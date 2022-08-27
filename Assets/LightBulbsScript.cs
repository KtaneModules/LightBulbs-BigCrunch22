using LightBulbsModule;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;

using Color = LightBulbsModule.Color;
using rnd = UnityEngine.Random;

public class LightBulbsScript : MonoBehaviour
{
	public KMBombModule Module;
	public KMAudio Audio;
	public KMBombInfo Info;
    public KMColorblindMode Colorblind;
	public KMRuleSeedable RuleSeed;

	public Mesh[] ButtonMeshes;
	public MeshFilter[] ButtonMeshFilters;
	public MeshRenderer[] BulbRenderer;
	public KMSelectable[] Buttons;

	public AudioClip[] Sfx;
	public TextMesh[] ButtonTexts;
    public TextMesh[] ColorblindTexts;
	public Material[] BulbMaterials;
	public Material[] MiddleBulbMaterials;
	public Material DefaultMaterial;

	private List<Bulb> Bulbs = new List<Bulb>();

	private string[][] Table = new string[][]
	{
		new[]{"000", "00-", "0--", "00-", "000", "-0-", "000", "00-"},
		new[]{"-00", "-0-", "0-0", "-0-", "-00", "000", "000", "0-0"},
		new[]{"-0-", "0--", "---", "0--", "-00", "0--", "00-", "-00"},
		new[]{"--0", "-00", "---", "-00", "0--", "-00", "0--", "0-0"},
		new[]{"0-0", "00-", "000", "0--", "---", "0-0",  "0-0", "--0"},
		new[]{"---", "--0", "---", "-00", "0--", "-0-", "000", "--0"},
		new[]{"---", "-0-", "00-", "00-", "0-0", "--0", "--0", "0-0"},
		new[]{"---", "--0", "000", "-0-", "--0", "---", "-0-", "00-"}
	};
	private bool[] ButtonStates = new bool[] { false, false, false };

	private static int _moduleIdCounter = 1;
	private int _moduleId = 0;
	private bool isSolved = false;
	private bool interactable = true;
    private bool colorblindActive = false;

	private List<char> CorrectAnswer = new List<char>();
	private readonly static Regex TwitchPlaysRegex = new Regex("^submit ([o-][o-][o-])$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);


	// Use this for initialization
	void Start()
	{
		_moduleId = _moduleIdCounter++;
        if (Colorblind.ColorblindModeActive)
            colorblindActive = true;
		string[] RuleseedTable = {"000", "00-", "0--", "00-", "000", "-0-", "000", "00-", "-00", "-0-", "0-0", "-0-", "-00", "000", "000", "0-0", "-0-", "0--", "---", "0--", "-00", "0--", "00-", "-00", "--0", "-00", "---", "-00", "0--", "-00", "0--", "0-0", "0-0", "00-", "000", "0--", "---", "0-0",  "0-0", "--0", "---", "--0", "---", "-00", "0--", "-0-", "000", "--0", "---", "-0-", "00-", "00-", "0-0", "--0", "--0", "0-0", "---", "--0", "000", "-0-", "--0", "---", "-0-", "00-"};
		var RuleSeedRNG = RuleSeed.GetRNG();
		Debug.LogFormat("[Light Bulbs #{0}] Table for Ruleseed Number {1}:", _moduleId, RuleSeedRNG.Seed.ToString());
		if (RuleSeedRNG.Seed != 1)
		{
			RuleSeedRNG.ShuffleFisherYates(RuleseedTable);
		}
		string Log = "";
		for (int x = 0; x < RuleseedTable.Length; x++)
		{
			Table[x/8][x%8] = RuleseedTable[x];
			Log = Log + RuleseedTable[x] + (x%8 < 7 ? ", " : "");
			if (x%8 == 7)
			{
				Debug.LogFormat("[Light Bulbs #{0}] {1}", _moduleId, Log);
				Log = "";
			}
		}
		Debug.LogFormat("[Light Bulbs #{0}] ----------------------------------------------------", _moduleId);
		Init();
		Module.OnActivate += Activate;		
	}

	private void Init()
	{
		InitBulbs();
		CorrectAnswer = GetCorrectAnswer();
		Debug.LogFormat("[Light Bulbs #{0}] The bulbs are: {1}", _moduleId, string.Join(", ", Bulbs.Select(x => x.Color.ToString()).ToArray()));
		Debug.LogFormat("[Light Bulbs #{0}] Solution is: {1}", _moduleId, string.Join("", CorrectAnswer.Select(x => x.ToString()).ToArray()));
	}

	private void Reset()
	{
		ButtonTexts[3].text = "SUBMIT";
		ButtonStates = new bool[3] { false, false, false };
		Bulbs = new List<Bulb>();
		for (int i = 0; i < 3; ++i)
		{
			ButtonMeshFilters[i].mesh = null;
			ButtonTexts[i].text = "";
			ButtonMeshFilters[i].mesh = ButtonMeshes[0];
			BulbRenderer[i].material = DefaultMaterial;
		}
		Init();
	}

	void Activate()
	{
		for (int i = 0; i < 4; ++i)
		{
			int index = i;
			Buttons[index].OnInteract += delegate
			{
				if (!interactable || isSolved)
				{
					return false;
				}
				HandlePress(index);
				return false;
			};
		}
	}

	private void HandlePress(int index)
	{
		Buttons[index].AddInteractionPunch(.2f);
		if (index == 3)
		{
			Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, Buttons[3].transform);			
			CheckAnswer();
		}
		else
		{
			ButtonMeshFilters[index].mesh = ButtonMeshes[(!ButtonStates[index]) ? 1 : 0];
			ButtonStates[index] = !ButtonStates[index];
			if (index != 1)
			{
				BulbRenderer[index].material = ((ButtonStates[index]) ? BulbMaterials[(int)Bulbs[index].Color] : DefaultMaterial);
                if (colorblindActive)
                    ColorblindTexts[index].text = ((ButtonStates[index]) ? BulbMaterials[(int)Bulbs[index].Color].name[0].ToString().ToUpper() : "");
            }
			else
			{
				BulbRenderer[1].material = ((ButtonStates[index]) ? MiddleBulbMaterials[(int)Bulbs[1].Color - 8] : DefaultMaterial);
                if (colorblindActive)
                {
                    if (ButtonStates[index])
                    {
                        if (MiddleBulbMaterials[(int)Bulbs[1].Color - 8].name.Equals("grey"))
                            ColorblindTexts[index].text = "A";
                        else
                            ColorblindTexts[index].text = "W";
                    }
                    else
                        ColorblindTexts[index].text = "";
                }
                    
            }

            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, Buttons[index].transform);
		}
	}
	private Color GetColor(int color)
	{
		switch (color)
		{
			case 0:
				return Color.Red;
			case 1:
				return Color.Orange;
			case 2:
				return Color.Yellow;
			case 3:
				return Color.Green;
			case 4:
				return Color.Blue;
			case 5:
				return Color.Purple;
			case 6:
				return Color.Cyan;
			case 7:
				return Color.Magenta;
			default:
				throw new InvalidOperationException("Invalid color detected, please contact the author of the mod!");
		}
	}

	private void InitBulbs()
	{
		Bulbs.Add(new Bulb { Color = GetColor(rnd.Range(0, 8)), Position = Position.Left });
		Bulbs.Add(new Bulb { Color = (rnd.Range(0, 2) == 1) ? Color.White : Color.Gray, Position = Position.Middle });
		Bulbs.Add(new Bulb { Color = GetColor(rnd.Range(0, 8)), Position = Position.Right });
	}

	private List<char> GetCorrectAnswer()
	{
		return InvertTable(Table[(int)Bulbs[0].Color][(int)Bulbs[2].Color].ToCharArray().ToList());
	}

	private void CheckAnswer()
	{
		var buttonValues = ConvertButtonToValue(ButtonStates);
        if (colorblindActive)
        {
            for (int i = 0; i < 3; i++)
                ColorblindTexts[i].text = "";
        }
		if (string.Join("", CorrectAnswer.Select(x => x.ToString()).ToArray()) == string.Join("", buttonValues.Select(x => x.ToString()).ToArray()))
		{
			isSolved = true;
			StartCoroutine(SolveAnimation(true));
			Debug.LogFormat("[Light Bulbs #{0}] You entered: {1}. That is correct, module solved.", _moduleId, string.Join("", buttonValues.Select(x => x.ToString()).ToArray()));
		}
		else
		{
			StartCoroutine(SolveAnimation(false));
			Debug.LogFormat("[Light Bulbs #{0}] You entered: {1}. That is incorrect, expected: {2}. Strike!", _moduleId, string.Join("", buttonValues.Select(x => x.ToString()).ToArray()), string.Join("", CorrectAnswer.Select(x => x.ToString()).ToArray()));
		}
	}

	private List<char> InvertTable(List<char> list)
	{
		var newList = new List<char>();
		if (Bulbs[1].Color == Color.Gray)
		{
			for (int i = 0; i < 3; ++i)
			{
				newList.Add((list[i] == '0') ? '-' : '0');
			}
			return newList;
		}
		return list;
	}

	private List<char> ConvertButtonToValue(bool[] buttons)
	{
		var newList = new List<char>();
		for (int i = 0; i < 3; ++i)
		{
			newList.Add((buttons[i]) ? '0' : '-');
		}
		return newList;
	}

	IEnumerator SolveAnimation(bool IsSolved)
	{
		interactable = false;
		ButtonTexts[3].text = "";
		for (int i = 0; i < 3; ++i)
		{
			ButtonMeshFilters[i].mesh = null;
			BulbRenderer[i].material = DefaultMaterial;
		}
		yield return new WaitForSecondsRealtime(1f);
		for (int i = 0; i < 3; ++i)
		{
			BulbRenderer[i].material = MiddleBulbMaterials[1];
			Audio.PlaySoundAtTransform(Sfx[0].name, transform);
			yield return new WaitForSecondsRealtime(1f);
		}
		if (IsSolved)
		{
			for (int i = 0; i < 3; ++i)
			{
				ButtonMeshFilters[i].mesh = null;
			}
			ButtonTexts[0].text = "G";
			ButtonTexts[1].text = "G";
			ButtonTexts[2].text = "!";
			for (int i = 0; i < 3; ++i)
			{
				BulbRenderer[i].material = BulbMaterials[3];
			}
			Audio.PlaySoundAtTransform(Sfx[2].name, transform);
			Module.HandlePass();
		}
		else
		{
			for (int i = 0; i < 3; ++i)
			{
				ButtonMeshFilters[i].mesh = null;
			}
			ButtonTexts[0].text = "N";
			ButtonTexts[1].text = "O";
			ButtonTexts[2].text = "!";
			for (int i = 0; i < 3; ++i)
			{
				BulbRenderer[i].material = BulbMaterials[0];
			}
			Audio.PlaySoundAtTransform(Sfx[1].name, transform);
			yield return new WaitForSecondsRealtime(1.75f);
            Module.HandleStrike();
            Reset();
			interactable = true;
		}	
	}

#pragma warning disable 414
	private readonly string TwitchHelpMessage = "Cycle the bulbs by entering: !{0} cycle | Submit your answer by entering: !{0} submit -o- | Toggle colorblind mode by entering: !{0} colorblind";
#pragma warning restore 414

	public IEnumerator ProcessTwitchCommand(string command)
	{
		command = command.ToLowerInvariant().Trim();
		Match m = TwitchPlaysRegex.Match(command);
        if (command.Equals("colorblind", StringComparison.InvariantCultureIgnoreCase))
        {
            yield return null;
            if (colorblindActive)
            {
                colorblindActive = false;
                for (int i = 0; i < 3; i++)
                    ColorblindTexts[i].text = "";
            }
            else
            {
                colorblindActive = true;
                for (int i = 0; i < 3; i++)
                {
                    if (ButtonStates[i])
                    {
                        if (i != 1)
                            ColorblindTexts[i].text = BulbMaterials[(int)Bulbs[i].Color].name[0].ToString().ToUpper();
                        else
                        {
                            if (MiddleBulbMaterials[(int)Bulbs[1].Color - 8].name.Equals("grey"))
                                ColorblindTexts[i].text = "A";
                            else
                                ColorblindTexts[i].text = "W";
                        }
                    }
                }
            }
            yield break;
        }
        if (command.Equals("cycle", StringComparison.InvariantCultureIgnoreCase))
		{
			for (int i = 0; i < 3; ++i)
			{
				yield return new WaitForSecondsRealtime(1f);
                Buttons[i].OnInteract();
				yield return new WaitForSecondsRealtime(1f);
			}
			for (int i = 0; i < 3; ++i)
			{
                Buttons[i].OnInteract();
                yield return new WaitForSecondsRealtime(.1f);
			}
			yield break;
		}
		if (m.Success)
		{
			yield return null;
			var input = m.Groups[1].ToString().ToCharArray();
			for (int i = 0; i < 3; ++i)
			{
				if (input[i].Equals('-'))
				{
					if (ButtonStates[i])
					{
                        Buttons[i].OnInteract();
                        yield return new WaitForSecondsRealtime(.1f);
					}
				}
				else
				{
					if (!ButtonStates[i])
					{
                        Buttons[i].OnInteract();
                        yield return new WaitForSecondsRealtime(.1f);
					}
				}				
			}
            Buttons[3].OnInteract();
            if (isSolved)
			{
				yield return "solve";
			}
			else
			{
				yield return "strike";
			}
			yield break;
		}
		yield break;
	}

    public IEnumerator TwitchHandleForcedSolve()
    {
        if (!isSolved)
        {
            if (!interactable)
            {
                StopAllCoroutines();
                for (int i = 0; i < 3; ++i)
                {
                    ButtonMeshFilters[i].mesh = null;
                }
                ButtonTexts[0].text = "G";
                ButtonTexts[1].text = "G";
                ButtonTexts[2].text = "!";
                for (int i = 0; i < 3; ++i)
                {
                    BulbRenderer[i].material = BulbMaterials[3];
                }
                Audio.PlaySoundAtTransform(Sfx[2].name, transform);
                Module.HandlePass();
            }
            else
            {
                List<char> states = ConvertButtonToValue(ButtonStates);
                for (int i = 0; i < 3; i++)
                {
                    if (states[i] != CorrectAnswer[i])
                    {
                        Buttons[i].OnInteract();
                        yield return new WaitForSecondsRealtime(.1f);
                    }
                }
                Buttons[3].OnInteract();
            }
        }
        while (!interactable && ButtonTexts[2].text != "!") yield return true;
    } 
}
