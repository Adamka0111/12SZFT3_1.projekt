namespace ÉletjátékLib
{
    public class Fox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Hunger { get; set; } = 10;

        public Fox(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public void EatRabbit()
        {
            Hunger += 3;
        }
    }
}
