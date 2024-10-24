using ÉletjátékLib;

string input = " ";
bool done = false;
List<string> rabbitLocations = new List<string>();
List<string> foxLocations = new List<string>();
int animalNumber = 0;
while (done == false && animalNumber < 11)
{
    Console.WriteLine("Adjon meg 10 állat koordinátáját. Formálás: állat typusa, X koordináta, Y koordináta.\nÁllat típusa: N - nyúl, R - róka\nX, Y koordináta: 1 és 15 közötti egész szám.");
    Console.WriteLine("Ha kész van az adatok megadásával, írja be hogy \"exit\"");
    input = Console.ReadLine();
    if (input[0] == 'N')
    {
        rabbitLocations.Add(input);
        animalNumber += 1;
    }
    else if (input[0] == 'R')
    {
        foxLocations.Add(input);
        animalNumber += 1;
    }
    else if (input == "exit")
    {
        done = true;
    }
    else
    {
        Console.WriteLine("Helytelen adatot adott meg.");
    }
    Console.Clear();
}


Field field = new Field(rabbitLocations, foxLocations);

    while (true)
    {
        Console.Clear();
        field.Display();  
        field.Update();   
        System.Threading.Thread.Sleep(1500); // Várakozás 1.5 másodpercig
    }
