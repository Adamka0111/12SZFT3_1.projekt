namespace Életjáték
{
    public class Field
    {
        private const int Width = 10;  
        private const int Height = 10; 
        private Grass[,] grid;         

        public Field()
        {
            grid = new Grass[Width, Height];
            InitializeField();
        }

        private void InitializeField()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = new Grass();
                }
            }
        }

        public void Update()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y].Grow();
                }
            }
        }

        public void Display()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Console.Write(grid[x, y].GetDisplayCharacter() + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}