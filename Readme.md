#               Nyulak és Rókák

## Tartalomjegyzék
1 Életjáték
- [Program.cs](#programcs)

2 ÉletjátékLib
- [Field.cs](#fieldcs)
- [Fox.cs](#foxcs)
- [Grass.cs](#grasscs)
- [Rabbit.cs](#rabbitcs)


# Életjáték
## Program.cs
A Program.cs felel a program futásáért.  
A `Field field = new Field();` meghívja az ÉletjátékLib-ből a Field osztályt;
A `While` ciklusban fut a kód:  
1 `Console.Clear();`: törli az előző mezőt  
2 `field.Display();`: kiírja a jelenlegi mezőt  
3 `System.Threading.Thread.Sleep(1500);`: késlelteti az ismétlést 1,5 másodperccel  


# ÉletjátékLib
## Field.cs

A Szimuláció területének létrehozásáért és megjelenítéséért felelő osztály  

1 `private const int Width`: A mező szélességét tartalmazó konstans.  
2 `private const int Height`: A mező magasságát tartalmazó konstans.  
3 `private Grass[,] grid`: A füvet tároló mátrix.  
4 `private List<Rabbit> rabbits`: A nyulakat tároló lista.  
5 `private List<Fox> foxes`: A rókákat tároló lista.  
6 `private Random random`: A random számok generálásához szükséges tulajdonság.  
7 `public Field`: A `Field` osztály konstructora  
8 `private void InitializeField`: egy metódus ami feltölti a `grid[,]` mátrixot és random koordinátákra helyezi a nyulakat és a rókákat.  
9 `public void Update`: A mező állapotát frissítő metódus.  
10 `private bool IsRabbitNearby(int x, int y)`: Ellenőrzi hogy a megadott koordinátákon tartózkodik-e nyúl.  
11 `private bool IsFoxNearby(int x, int y)`: Ellenőrzi hogy a megadott koordinátákon tartózkodik-e róka.  
12 `private Grass FindNearestGrass(Rabbit rabbit)`: Megkeresi a megadott nyúlhoz legközelebb lévő fű csomót.  
13 `private Rabbit FindNearestRabbit(Fox fox)`: Megkeresi a megadott rókához legközelebb lévő nyulat.  
14 `private void MoveTowards(Rabbit rabbit, Grass grass)`: A megadott nyulat a megadott fű csomó felé mozdítja.  
15 `private (int, int) GetGrassCoordinates(Grass grass)`: Megkeresi a megadott fű csomó koordinátáit.  
16 `private void MoveTowards(Fox fox, Rabbit rabbit)`: A megadott rókát a megadott nyúl felé mozdítja.  
17 `private (int, int) FindEmptyAdjacentTile(Rabbit rabbit)`: Megkeresi a megadott nyúlhoz legközelebb lévő "üres" cellát. (üres cella = fűkezdemény)  
18 `private (int, int) FindEmptyAdjacentTile(Fox fox)`: Megkeresi a megadott rókához legközelebb lévő "üres" cellát. (üres cella = fűkezdemény)  
19 `public void Display()`: A mező és a rajta lévő rókák és nyulak megjelenítéséért felelő metódus.  
20 `private bool IsAdjacent`:
   - `(Rabbit rabbit1, Rabbit rabbit2)`: Megnézi hogy a megadott nyúl mellett van-e másik nyúl.  
   - `(Fox fox, Rabbit rabbit)`: Megnézi hogy a megadott róka mellett van-e nyúl.
   - `(Fox fox1, Fox fox2)`: Megnézi hogy a megadott róka mellett van-e másik róka.

## Fox.cs

A `Fox` osztály tárolja a rókák adatait (X és Y koordináta + Jóllakottság) és felel a rókáknak a mozgásáért és evésért.  
1 `public int X`: A rókák X koordinátáját tároló tulajdonság.  
2 `public int Y`: A rókák Y koordinátáját tároló tulajdonság.  
3 `public int Hunger`: A rókák Jóllakottsági szintjét tároló tulajdonság (kezdőértek az 10).  
4 `public Fox(int x, int y)`: A `Fox` Osztály konstruktora ami beviszi az X és Y koordinátákat.  
5 `public void Move(int newX, int newY)`: A mozgásért felelő methódus.  
6 `public void EatRabbit()`: a nyulak megevéséért felelő metódus.  

## Grass.cs

A `Grass` Osztály felel a fű növekedéséért.
1 `public enum GrassState`: A fű növekedési státuszait tároló lista.  
2 `public GrassState State`: A fű státuszát tároló tulajdonság.  
3 `public Grass()`: A `Fű` osztály konstructora ami random növekedési stádiummal tárolja el a fű csomókat.  
4 `public void Grow()`: A fű növekedéséért felelő metódus.  
5 `public char GetDisplayCharacter()`: egy metódus ami fű csomók növekedési stádiumától függő megjelenítési karakterét adaja vissza.  


## Rabbit.cs

A `Rabbit` osztály tárolja a nyulak adatait (X és Y koordináta + Jóllakottság) és felel a nyulaknak a mozgásáért és evésért.  
1 `public int X`: A nyulak X koordinátáját tároló tulajdonság.  
2 `public int Y`: A nyulak Y koordinátáját tároló tulajdonság.  
3 `public int Hunger`: A nyulak Jóllakottsági szintjét tároló tulajdonság (kezdőértek az 5).  
4 `public Rabbit(int x, int y)`: A `Rabbit` Osztály konstruktora ami beviszi az X és Y koordinátákat.  
5 `public void Move(int newX, int newY)`: A mozgásért felelő methódus.  
6 `public bool EatGrass(Grass grass)`: A nyulak evéséért felelő metódus:  
- `if (grass.State == GrassState.Tender || grass.State == GrassState.Mature)`: Ellenőrzi hogy a nyúl éppen zsenge füvön vagy kifejlett főcsomón áll:
   - ha igen akkor megeszi és nő a jóllakottság,
   - ha nem akkor csökken 1-el.  