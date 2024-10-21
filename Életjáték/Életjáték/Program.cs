using ÉletjátékLib;

Field field = new Field();

    while (true)
    {
        Console.Clear();
        field.Display();  
        field.Update();   
        System.Threading.Thread.Sleep(1500); // Várakozás 1.5 másodpercig
    }
