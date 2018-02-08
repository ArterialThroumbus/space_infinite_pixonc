namespace Assets.Scripts.Models
{
    public struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coordinate operator +(Coordinate first, Coordinate second)
        {
            first.X += second.X;
            first.Y += second.Y;

            return first;
        }

        public static Coordinate operator -(Coordinate first, Coordinate second)
        {
            first.X -= second.X;
            first.Y -= second.Y;

            return first;
        }
    }
}
