namespace LightBulbsModule
{
    public enum Color
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        Cyan,
        Magenta,
        Gray, 
        White
    }

    public enum Position
    {
        Left,
        Middle,
        Right
    }

    public class Bulb
    {
        public Color Color { get; set; }
        public Position Position { get; set; }
    }
}