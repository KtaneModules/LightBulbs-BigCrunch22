using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class LightBulbsScript : MonoBehaviour
{
	
	public KMAudio Audio;
    public KMBombInfo Bomb;
    public KMBombModule Module;
	
	public AudioClip[] SFX;
	
	public KMSelectable ButtonOne;
	public KMSelectable ButtonTwo;
	public KMSelectable ButtonThree;
	public KMSelectable Submit;
	
	public Material[] ColorBlocks;
	public Material[] CenterColors;
	public Material[] BlessedMaterial;
	public Renderer[] LED;
	
	public TextMesh QDiamond;
	public TextMesh[] QDiamondDays;
	
	public GameObject[] CirclesAndPluses;
	
	private int[] Inspector = {0, 0, 0};
	private int[] Determination = {0, 0, 0};
	private int[] TheComparison = {0, 0, 0};
	
	private bool Playable = false;
	
	//Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool ModuleSolved;
	
	void Awake()
	{
		moduleId = moduleIdCounter++;
		ButtonOne.OnInteract += delegate () { PressButtonOne(); return false; };
		ButtonTwo.OnInteract += delegate () { PressButtonTwo(); return false; };
		ButtonThree.OnInteract += delegate () { PressButtonThree(); return false; };
		Submit.OnInteract += delegate () { PressSubmit(); return false; };
		
	}
	
	void Start()
	{
		BulbColor();
		Gnome();
		Playable = true;
	}
	
	void BulbColor()
	{
		Inspector[0] = UnityEngine.Random.Range(0, ColorBlocks.Count());
		Inspector[1] = UnityEngine.Random.Range(0, CenterColors.Count());
		Inspector[2] = UnityEngine.Random.Range(0, ColorBlocks.Count());
		CirclesAndPluses[0].SetActive(false);
		CirclesAndPluses[2].SetActive(false);
		CirclesAndPluses[4].SetActive(false);
	}
	
	void Gnome()
	{
		if ((Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 5) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 2))
		{
			TheComparison[0] = 0;
			TheComparison[1] = 0;
			TheComparison[2] = 0;
		}
		
		else if ((Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 7) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 7) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 5))
		{
			TheComparison[0] = 0;
			TheComparison[1] = 0;
			TheComparison[2] = 1;
		}
		
		else if ((Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 5) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 5) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 3) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 3))
		{
			TheComparison[0] = 0;
			TheComparison[1] = 1;
			TheComparison[2] = 0;
		}
		
		else if ((Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 3) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 3) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 5) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 7))
		{
			TheComparison[0] = 0;
			TheComparison[1] = 1;
			TheComparison[2] = 1;
		}
		
		else if ((Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 3) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 3) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 5) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 4))
		{
			TheComparison[0] = 1;
			TheComparison[1] = 0;
			TheComparison[2] = 0;
		}
		
		else if ((Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 0 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 1 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 3) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 7) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 3 && Inspector[1] == 1 && Inspector[2] == 7) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 5) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 7))
		{
			TheComparison[0] = 1;
			TheComparison[1] = 0;
			TheComparison[2] = 1;
		}
		
		else if ((Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 7) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 1) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 6) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 2 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 6 && Inspector[1] == 1 && Inspector[2] == 3) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 1) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 7) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 7) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 3))
		{
			TheComparison[0] = 1;
			TheComparison[1] = 1;
			TheComparison[2] = 0;
		}
		
		else if ((Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 3 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 7 && Inspector[1] == 0 && Inspector[2] == 5) || (Inspector[0] == 4 && Inspector[1] == 0 && Inspector[2] == 4) || (Inspector[0] == 5 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 6 && Inspector[1] == 0 && Inspector[2] == 0) || (Inspector[0] == 2 && Inspector[1] == 0 && Inspector[2] == 2) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 0) || (Inspector[0] == 7 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 4 && Inspector[1] == 1 && Inspector[2] == 2) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 4) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 0 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 5 && Inspector[1] == 1 && Inspector[2] == 6) || (Inspector[0] == 1 && Inspector[1] == 1 && Inspector[2] == 5))
		{
			TheComparison[0] = 1;
			TheComparison[1] = 1;
			TheComparison[2] = 1;
		}
	}
	
	void TheTrueColor()
	{
		if (Determination[0] == 0)
		{
			LED[0].material = BlessedMaterial[0];
		}
		
		else if (Determination[0] == 1)
		{
			LED[0].material = ColorBlocks[Inspector[0]];
		}
		
		if (Determination[1] == 0)
		{
			LED[1].material = BlessedMaterial[0];
		}
		
		else if (Determination[1] == 1)
		{
			LED[1].material = CenterColors[Inspector[1]];
		}
		
		if (Determination[2] == 0)
		{
			LED[2].material = BlessedMaterial[0];
		}
		
		else if (Determination[2] == 1)
		{
			LED[2].material = ColorBlocks[Inspector[2]];
		}
	}
	
	void PressButtonOne()
	{
		ButtonOne.AddInteractionPunch(0.2f);
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		if (Playable == true)
		{
			if (Determination[0] == 0)
			{
				Determination[0] = 1;
				TheTrueColor();
				CirclesAndPluses[0].SetActive(true);
				CirclesAndPluses[1].SetActive(false);
			}
			
			else if (Determination[0] == 1)
			{
				Determination[0] = 0;
				TheTrueColor();
				CirclesAndPluses[0].SetActive(false);
				CirclesAndPluses[1].SetActive(true);
			}
		}
	}
	
	void PressButtonTwo()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		ButtonTwo.AddInteractionPunch(0.2f);
		if (Playable == true)
		{
			if (Determination[1] == 0)
			{
				Determination[1] = 1;
				TheTrueColor();
				CirclesAndPluses[2].SetActive(true);
				CirclesAndPluses[3].SetActive(false);
			}
			
			else if (Determination[1] == 1)
			{
				Determination[1] = 0;
				TheTrueColor();
				CirclesAndPluses[2].SetActive(false);
				CirclesAndPluses[3].SetActive(true);
			}
		}
	}
	
	void PressButtonThree()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		ButtonTwo.AddInteractionPunch(0.2f);
		if (Playable == true)
		{
			if (Determination[2] == 0)
			{
				Determination[2] = 1;
				TheTrueColor();
				CirclesAndPluses[4].SetActive(true);
				CirclesAndPluses[5].SetActive(false);
			}
			
			else if (Determination[2] == 1)
			{
				Determination[2] = 0;
				TheTrueColor();
				CirclesAndPluses[4].SetActive(false);
				CirclesAndPluses[5].SetActive(true);
			}
		}
	}
	
	void PressSubmit()
	{
		Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.BigButtonPress, transform);
		Submit.AddInteractionPunch(0.2f);
		if (Playable == true)
		{
			if (Determination[0] == TheComparison[0] && Determination[1] == TheComparison[1] && Determination[2] == TheComparison[2])
			{
				StartCoroutine(FlickerVictory());
				Playable = false;
				
			}
			
			else
			{
				StartCoroutine(FlickerFail());
				Playable = false;
			}
		}
	}
	
	IEnumerator FlickerFail()
	{
		QDiamond.text = "";
		LED[0].material = BlessedMaterial[0];
		LED[1].material = BlessedMaterial[0];
		LED[2].material = BlessedMaterial[0];
		CirclesAndPluses[0].SetActive(false);
		CirclesAndPluses[1].SetActive(false);
		CirclesAndPluses[2].SetActive(false);
		CirclesAndPluses[3].SetActive(false);
		CirclesAndPluses[4].SetActive(false);
		CirclesAndPluses[5].SetActive(false);
		yield return new WaitForSecondsRealtime(1f);
		LED[0].material = BlessedMaterial[1];
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSecondsRealtime(1f);
		LED[1].material = BlessedMaterial[1];
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSecondsRealtime(1f);
		LED[2].material = BlessedMaterial[1];
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSecondsRealtime(1f);
		LED[0].material = ColorBlocks[0];
		LED[1].material = ColorBlocks[0];
		LED[2].material = ColorBlocks[0];
		QDiamondDays[0].text = "N";
		QDiamondDays[1].text = "O";
		QDiamondDays[2].text = "!";
		Audio.PlaySoundAtTransform(SFX[2].name, transform);
		yield return new WaitForSecondsRealtime(1.75f);
		Module.HandleStrike();
		Reset();
		BulbColor();
		Gnome();
		Playable = true;
	}
	
	IEnumerator FlickerVictory()
	{
		QDiamond.text = "";
		LED[0].material = BlessedMaterial[0];
		LED[1].material = BlessedMaterial[0];
		LED[2].material = BlessedMaterial[0];
		CirclesAndPluses[0].SetActive(false);
		CirclesAndPluses[1].SetActive(false);
		CirclesAndPluses[2].SetActive(false);
		CirclesAndPluses[3].SetActive(false);
		CirclesAndPluses[4].SetActive(false);
		CirclesAndPluses[5].SetActive(false);
		yield return new WaitForSecondsRealtime(1f);
		LED[0].material = BlessedMaterial[1];
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSecondsRealtime(1f);
		LED[1].material = BlessedMaterial[1];
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSecondsRealtime(1f);
		LED[2].material = BlessedMaterial[1];
		Audio.PlaySoundAtTransform(SFX[1].name, transform);
		yield return new WaitForSecondsRealtime(1f);
		Module.HandlePass();
		LED[0].material = ColorBlocks[3];
		LED[1].material = ColorBlocks[3];
		LED[2].material = ColorBlocks[3];
		Audio.PlaySoundAtTransform(SFX[0].name, transform);
		QDiamondDays[0].text = "G";
		QDiamondDays[1].text = "G";
		QDiamondDays[2].text = "!";
	}
	
	void Reset()
	{
		LED[0].material = BlessedMaterial[0];
		LED[1].material = BlessedMaterial[0];
		LED[2].material = BlessedMaterial[0];
		Determination[0] = 0;
		Determination[1] = 0;
		Determination[2] = 0;
		CirclesAndPluses[1].SetActive(true);
		CirclesAndPluses[3].SetActive(true);
		CirclesAndPluses[5].SetActive(true);
		QDiamond.text = "SUBMIT";
		QDiamondDays[0].text = "";
		QDiamondDays[1].text = "";
		QDiamondDays[2].text = "";
	}
}
