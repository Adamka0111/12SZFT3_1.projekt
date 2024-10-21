namespace ÉletjátékLib
{
    internal class Rabbit
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Hunger { get; set; } = 5;

        public Rabbit(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }

        public bool EatGrass(Grass grass)
        {
            if (grass.State == GrassState.Tender || grass.State == GrassState.Mature)
            {
                Hunger += (int)grass.State - 1;
                return true;
            }
            return false;
        }
    }
}
