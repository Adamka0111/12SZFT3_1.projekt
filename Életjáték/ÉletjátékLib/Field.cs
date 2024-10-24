namespace �letj�t�kLib
{
    public class Field
    {
        private const int Width = 15;
        private const int Height = 15;
        private Grass[,] grid;
        private List<Rabbit> rabbits;
        private List<Fox> foxes;
        private Random random;

        public Field(List<string> rabbitLocations, List<string> foxLocations)
        {
            grid = new Grass[Width, Height];
            rabbits = new List<Rabbit>();
            foxes = new List<Fox>();
            random = new Random();
            List<int[]> rabbitCoordinates = new List<int[]>();
            List<int[]> foxCoordinates = new List<int[]>();
            for (int i = 0; i < rabbitLocations.Count(); i++)
            {
                string[] location = rabbitLocations[i].Split(";");
                int[] coordinates = [int.Parse(location[1])-1, int.Parse(location[2])-1];
                rabbitCoordinates.Add(coordinates);
            }
            for (int i = 0; i < foxLocations.Count(); i++)
            {
                string[] location = foxLocations[i].Split(";");
                int[] coordinates = [int.Parse(location[1]) - 1, int.Parse(location[2]) - 1];
                foxCoordinates.Add(coordinates);
            }
            InitializeField(rabbitCoordinates, foxCoordinates);
        }

        private void InitializeField(List<int[]> rabbitCoordinates, List<int[]> foxCoordinates)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = new Grass();
                }
            }

            for (int i = 0; i < rabbitCoordinates.Count(); i++)
            {
                int x = rabbitCoordinates[i][0];
                int y = rabbitCoordinates[i][1];
                rabbits.Add(new Rabbit(x, y));
            }

            for (int i = 0; i < foxCoordinates.Count(); i++)
            {
                int x = foxCoordinates[i][0];
                int y = foxCoordinates[i][1];
                foxes.Add(new Fox(x, y));
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

            List<Rabbit> newRabbits = new List<Rabbit>();
            for (int i = rabbits.Count - 1; i >= 0; i--)
            {
                Rabbit rabbit = rabbits[i];
                rabbit.Hunger--;

                if (rabbit.Hunger <= 0)
                {
                    rabbits.RemoveAt(i);
                    continue;
                }

                Grass nearestGrass = FindNearestGrass(rabbit);
                if (nearestGrass != null)
                {
                    MoveTowards(rabbit, nearestGrass);
                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (grid[x, y] == nearestGrass)
                            {
                                if (rabbit.X == x && rabbit.Y == y)
                                {
                                    if (rabbit.EatGrass(nearestGrass))
                                    {
                                        grid[rabbit.X, rabbit.Y] = new Grass();
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (Rabbit otherRabbit in rabbits)
                {
                    if (otherRabbit != rabbit && IsAdjacent(rabbit, otherRabbit))
                    {
                        (int newX, int newY) = FindEmptyAdjacentTile(rabbit);
                        if (newX != -1 && newY != -1 && !IsFoxNearby(newX, newY))
                        {
                            newRabbits.Add(new Rabbit(newX, newY));
                        }
                    }
                }
            }

            rabbits.AddRange(newRabbits);

            List<Fox> newFoxes = new List<Fox>();
            for (int i = foxes.Count - 1; i >= 0; i--)
            {
                Fox fox = foxes[i];
                fox.Hunger--;

                if (fox.Hunger <= 0)
                {
                    foxes.RemoveAt(i);
                    continue;
                }

                Rabbit nearestRabbit = FindNearestRabbit(fox);
                if (nearestRabbit != null)
                {
                    if (IsAdjacent(fox, nearestRabbit))
                    {
                        fox.EatRabbit();
                        rabbits.Remove(nearestRabbit);
                    }
                    else
                    {
                        MoveTowards(fox, nearestRabbit);
                        MoveTowards(fox, nearestRabbit);
                    }
                }

                foreach (Fox otherFox in foxes)
                {
                    if (otherFox != fox && IsAdjacent(fox, otherFox))
                    {
                        (int newX, int newY) = FindEmptyAdjacentTile(fox);
                        if (newX != -1 && newY != -1 && !IsRabbitNearby(newX, newY))
                        {
                            newFoxes.Add(new Fox(newX, newY));
                        }
                    }
                }
            }

            foxes.AddRange(newFoxes);

            if (rabbits.Count == 0)
            {
                Console.WriteLine("A r�k�k megett�k az �sszes nyulat.");
                Environment.Exit(0);
            }
            else if (foxes.Count == 0)
            {
                Console.WriteLine("A nyulak t�l�lt�k a r�k�kat.");
                Environment.Exit(0);
            }
        }
		
        private bool IsRabbitNearby(int x, int y)
        {
            foreach (Rabbit rabbit in rabbits)
            {
                if (Math.Abs(rabbit.X - x) <= 1 && Math.Abs(rabbit.Y - y) <= 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsFoxNearby(int x, int y)
        {
            foreach (Fox fox in foxes)
            {
                if (Math.Abs(fox.X - x) <= 1 && Math.Abs(fox.Y - y) <= 1)
                {
                    return true;
                }
            }
            return false;
        }


        private Grass FindNearestGrass(Rabbit rabbit)
        {
            Grass nearestGrass = null;
            int minDistance = int.MaxValue;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int distance = Math.Abs(rabbit.X - x) + Math.Abs(rabbit.Y - y);
                    if (distance < minDistance && (grid[x, y].State == GrassState.Tender || grid[x, y].State == GrassState.Mature))
                    {
                        minDistance = distance;
                        nearestGrass = grid[x, y];
                    }
                }
            }

            return nearestGrass;
        }

        private Rabbit FindNearestRabbit(Fox fox)
        {
            Rabbit nearestRabbit = null;
            int minDistance = int.MaxValue;

            foreach (Rabbit rabbit in rabbits)
            {
                int distance = Math.Abs(fox.X - rabbit.X) + Math.Abs(fox.Y - rabbit.Y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestRabbit = rabbit;
                }
            }

            return nearestRabbit;
        }

        private void MoveTowards(Rabbit rabbit, Grass grass)
        {
            (int grassX, int grassY) = GetGrassCoordinates(grass);

            if (rabbit.X < grassX)
                rabbit.Move(rabbit.X + 1, rabbit.Y);
            else if (rabbit.X > grassX)
                rabbit.Move(rabbit.X - 1, rabbit.Y);
            else if (rabbit.Y < grassY)
                rabbit.Move(rabbit.X, rabbit.Y + 1);
            else if (rabbit.Y > grassY)
                rabbit.Move(rabbit.X, rabbit.Y - 1);
        }

        private (int, int) GetGrassCoordinates(Grass grass)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (grid[x, y] == grass)
                    {
                        return (x, y);
                    }
                }
            }
            return (-1, -1); 
        }


        private void MoveTowards(Fox fox, Rabbit rabbit)
        {
            if (fox.X < rabbit.X)
                fox.Move(fox.X + 1, fox.Y);
            else if (fox.X > rabbit.X)
                fox.Move(fox.X - 1, fox.Y);
            else if (fox.Y < rabbit.Y)
                fox.Move(fox.X, fox.Y + 1);
            else if (fox.Y > rabbit.Y)
                fox.Move(fox.X, fox.Y - 1);
        }

        private bool IsAdjacent(Rabbit rabbit1, Rabbit rabbit2)
        {
            return Math.Abs(rabbit1.X - rabbit2.X) <= 1 && Math.Abs(rabbit1.Y - rabbit2.Y) <= 1;
        }

        private bool IsAdjacent(Fox fox, Rabbit rabbit)
        {
            return Math.Abs(fox.X - rabbit.X) <= 1 && Math.Abs(fox.Y - rabbit.Y) <= 1;
        }

        private bool IsAdjacent(Fox fox1, Fox fox2)
        {
            return Math.Abs(fox1.X - fox2.X) <= 1 && Math.Abs(fox1.Y - fox2.Y) <= 1;
        }

        private (int, int) FindEmptyAdjacentTile(Rabbit rabbit)
        {
            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = rabbit.X + directions[i, 0];
                int newY = rabbit.Y + directions[i, 1];
                if (newX >= 0 && newX < Width && newY >= 0 && newY < Height && grid[newX, newY].State == GrassState.Seedling)
                {
                    return (newX, newY);
                }
            }
            return (-1, -1);
        }

        private (int, int) FindEmptyAdjacentTile(Fox fox)
        {
            int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = fox.X + directions[i, 0];
                int newY = fox.Y + directions[i, 1];
                if (newX >= 0 && newX < Width && newY >= 0 && newY < Height && grid[newX, newY].State == GrassState.Seedling)
                {
                    return (newX, newY);
                }
            }
            return (-1, -1);
        }


        public void Display()
        {
            Console.CursorVisible = false;
            char[,] displayGrid = new char[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    displayGrid[x, y] = grid[x, y].GetDisplayCharacter();
                }
            }

            foreach (Rabbit rabbit in rabbits)
            {
                displayGrid[rabbit.X, rabbit.Y] = 'N';
            }

            foreach (Fox fox in foxes)
            {
                displayGrid[fox.X, fox.Y] = 'R';
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (displayGrid[x, y] == 'N')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{displayGrid[x, y]} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (displayGrid[x, y] == 'R')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{displayGrid[x, y]} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (displayGrid[x, y] == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{displayGrid[x, y]} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{displayGrid[x, y]} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
