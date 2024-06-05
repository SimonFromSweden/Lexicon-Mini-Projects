using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MiniProjectW3;

// Create a list of Asset objects (including phone and computer)
//List<Asset> assets = new List<Asset>();
List<Asset> assets = new List<Asset>
{
new Phone ("IPhone", "8", new DateTime(2022,11,08), 111 , "USA"),
new Phone ("Samsung", "Galaxy 4", new DateTime(2021,06,14), 123 , "USA"),
new Computer ("Acer", "C775", new DateTime(2022,01,13), 523 , "USA"),
new Computer("Asus", "Elitebook 4", new DateTime(2022,02,04), 234, "Sweden"),
new Computer("HP", "Elitebook", new DateTime(2020,08,10), 345, "Sweden"),
new Phone("IPhone", "15", new DateTime(2023,09,10), 745, "Sweden"),
new Computer("Lenovo", "Yoga", new DateTime(2022,08,26), 445, "Spain"),
new Phone("Samsung", "Galaxy S22", new DateTime(2021,09,10), 645, "Spain"),
new Phone("Motorola", "G37", new DateTime(2022,08,07), 845, "Spain"),
};
// Start of the program calls the UserInterface-function allowing the user to make choices and input data
UserInterface();

void UserInterface()
{
    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Welcome to AssetTracker666!"); Console.ResetColor();
    Console.WriteLine("---------------------------");
    Console.WriteLine("Here are your choices:");
    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("*Press 1 to ADD ASSETS \n*Press 2 to SHOW ASSETS"); Console.ResetColor();
    Console.WriteLine("---------------------------");
    Console.Write("Select your choice and press enter: ");
    int choice = Convert.ToInt32(Console.ReadLine());
    switch (choice)
    {
        case 1:
            Console.Clear();
            AddAssets();
            break;
        case 2:
            Console.Clear();
            ShowAssets();
            break;
        default:
            Console.WriteLine("Wrong input, please try again!");
            UserInterface();
            break;
    }   
}

void AddAssets()
{
    bool isPhone = false; string asset = "asset"; string office = "";
    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("ADD ASSET"); Console.ResetColor(); Console.WriteLine("-----------------------------------");
    Console.WriteLine("Is the asset a phone or a computer?");
    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("*Press 1 for phone \n*Press 2 for computer\n*Press 3 to return to main menu");Console.ResetColor();
    Console.WriteLine("-----------------------------------");
    Console.Write("Select your choice and press enter: ");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            isPhone = true;
            asset = "phone";
            break;
        case "2":
            isPhone = false;
            asset = "computer";
            break;
        case "3":
            Console.Clear();
            UserInterface();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Incorrect input, please try again!"); Console.ResetColor();
            AddAssets();
            break;
    }
    Console.Write($"\nWhat is the brand of the {asset}? ");
    string brand = Console.ReadLine();
    Console.Write($"\nWhat is the model of the {asset}? ");
    string model = Console.ReadLine();
    Console.Write($"\nWhat is the purchase date of the {asset} (yyyy-MM-dd)?: ");
    string date = Console.ReadLine();
    //DateTime dt1 = Convert.ToDateTime(date);
    // https://stackoverflow.com/questions/371987/how-to-validate-a-datetime-in-c
    bool isValidDate;
    if (DateTime.TryParse(date, out DateTime validDate))
    {
        // Yay :)
        isValidDate = true;
    }
    else
    {
        isValidDate = false;
        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Incorrect date, please try again!"); Console.ResetColor();
        AddAssets();
    }

    Console.Write($"\nWhat is the price of the {asset} (in USD)? ");
    bool priceIsInt = int.TryParse(Console.ReadLine(), out int price);
    Console.WriteLine($"\nWhat office does the {asset} belong to?");
    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("*Press 1 for USA \n*Press 2 for Sweden\n*Press 3 for Spain"); Console.ResetColor();
    Console.WriteLine("-------------------------------------");
    Console.Write("Select your choice and press enter: ");
    int choice2 = Convert.ToInt32(Console.ReadLine());
    switch (choice2)
    {
        case 1:
            office = "USA";
            break;
        case 2:
            office = "Sweden";
            break;
        case 3:
            office = "Spain";
            break;
        default:
            Console.WriteLine("Incorrect input, please try again!");
            AddAssets();
            break;
    }

    if (isPhone && brand != "" && model != "" && isValidDate && priceIsInt)
    {
        assets.Add(new Phone(brand, model, validDate, price, office));
    }
    else if(!isPhone && brand != "" && model != "" && isValidDate && priceIsInt)
    {
        assets.Add(new Computer(brand, model, validDate, price, office));
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid inputs, please try again");
        Console.ResetColor();
        AddAssets();
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Asset successfully added!");
    Console.ResetColor();
    Console.WriteLine("-------------------------------------");


    Console.WriteLine("Here are your choices: ");
    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("*Press 1 to add another asset \n*Press 2 to return to main menu"); Console.ResetColor();
    Console.WriteLine("-----------------------------------");
    Console.Write("Select your choice and press enter: ");
    int choice3 = Convert.ToInt32(Console.ReadLine());
    switch (choice3)
    {
        case 1:
            Console.Clear();
            AddAssets();
            break;
        case 2:
            Console.Clear();
            UserInterface();
            break;
    }
}

void ShowAssets()
{
    Console.ForegroundColor = ConsoleColor.Green; Console.Write("SHOW ASSETS\n"); Console.ResetColor();
    Console.Write("Color Coding: ");
    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Less than 6 months from EOL".PadRight(30)); Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Red; Console.Write("Less than 3 months from EOL".PadRight(30)); Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("EOL has passed\n"); Console.ResetColor();
    Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("Type".PadRight(12) + "Brand".PadRight(12) + "Model".PadRight(12) + "Office".PadRight(12) + "Purchase Date".PadRight(15) + "Price in USD".PadRight(15)+ "Currency".PadRight(15) + "Local price today"); Console.ResetColor();
    Console.WriteLine("----".PadRight(12) + "-----".PadRight(12) + "-----".PadRight(12) + "------".PadRight(12) + "-------------".PadRight(15) + "------------".PadRight(15) + "---------".PadRight(15) + "-----------------");
    int maxTime = 3 * 365;

    // Sorting the assets-array
    List<Asset> sortedAssets = assets.OrderBy(Asset => Asset.Office).ThenBy(Asset => Asset.PurchaseDate).ToList(); //https://stackoverflow.com/questions/298725/multiple-order-by-in-linq


    // looping through the assets array and printing its contents
    foreach (Asset asset in sortedAssets)
    {
        int price = asset.Price;
        // Code for calculating the time between purchase date and now
        DateTime pd = asset.PurchaseDate;
        DateTime dtNow = DateTime.Now;
        TimeSpan diff = dtNow.Subtract(pd);
        int daysFromPurchase = diff.Days;
        int daysToEndOfLife = maxTime- daysFromPurchase;
        if (daysToEndOfLife < 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        else if (daysToEndOfLife < 92)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (daysToEndOfLife < 183)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.WriteLine(asset.Type.PadRight(12) + asset.Brand.PadRight(12) + asset.Model.PadRight(12) + asset.Office.PadRight(12) + asset.PurchaseDate.ToString("yyyy-MM-dd").PadRight(15) + price.ToString().PadRight(15) + OfficeToCurrency(asset.Office).PadRight(15) + (price*OfficeToCurrencyFactor(asset.Office)).ToString("#.##"));
        Console.ResetColor();
    }
    Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
    Console.WriteLine("Here are your choices: ");
    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("*Press 1 to add another asset \n*Press 2 to return to main menu"); Console.ResetColor();
    Console.WriteLine("-----------------------------------");
    Console.Write("Select your choice and press enter: ");
    int choice3 = Convert.ToInt32(Console.ReadLine());
    switch (choice3)
    {
        case 1:
            Console.Clear();
            AddAssets();
            break;
        case 2:
            Console.Clear();
            UserInterface();
            break;
    }
}



string OfficeToCurrency(string office)
{
    if (office == "USA")
    {
        return "USD";
    }
    else if (office == "Sweden")
    {
        return "SEK";
    }
    else if (office == "Spain")
    {
        return "EUR";
    }
    else
    {
        return "Unknown";
    }
}

double OfficeToCurrencyFactor(string office)
{
    if (office == "USA")
    {
        return 1.0;
    }
    else if (office == "Sweden")
    {
        return 10.42;
    }
    else if (office == "Spain")
    {
        return 0.92;
    }
    else
    {
        return 0.0;
    }
}
