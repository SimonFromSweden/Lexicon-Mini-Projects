using System.ComponentModel;
// Skapa en lista med Product-object
List<Product> products = new List<Product>();

EnterProduct();

// Funktionsdeklarationer
void EnterProduct()
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("To enter a new product - follow the steps | To quit - enter: \"Q\"");
    Console.ResetColor();
    Console.WriteLine("----------------------------------------------------------------");

    Console.Write("Enter a category: ");
    string category = Console.ReadLine();
    Console.WriteLine("");
    if (category == "q" || category == "Q")
    {
        ShowProductList();
        return;
    }
    
    Console.Write("Enter a product name: ");
    string productName = Console.ReadLine();
    Console.WriteLine("");
    if (category == "q" || category == "Q")
    {
        ShowProductList();
        return;
    }

    Console.Write("Enter a price: ");
    bool outcome = Int32.TryParse(Console.ReadLine(), out int price);
    if (!outcome)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nGiven value is not a number, please try again!\n");
        EnterProduct();
    }

    Console.WriteLine("");
    if (category == "q" || category == "Q")
    {
        ShowProductList();
        return;
    }

    // Add Product to the list products
    if (category != "" && productName != "" && outcome)
    {
        products.Add(new Product(category, productName, price));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The product was successfully added!");
        Console.ResetColor();
        Console.WriteLine("----------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        EnterProduct();
    }
}

void ShowProductList()
{
    Console.WriteLine("----------------------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
    Console.ResetColor();
    int sum = products.Sum(x => x.Price); // Calculation of sum
    List<Product> sortedProducts = products.OrderBy(Product => Product.Price).ToList(); //uses Product object as parameter, and orders by Price attribute using LINQ
    foreach (Product product in sortedProducts)
    {
        Console.WriteLine(product.Category.PadRight(20) + product.ProdName.PadRight(20) + product.Price);
    }
    Console.WriteLine("\n" + " ".PadRight(20) + "Total amount:".PadRight(20) + sum.ToString().PadRight(20));
    Console.WriteLine("----------------------------------------------------------------");
    ShowChoices();
}

void ShowChoices()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("To enter a new product - enter: \"P\" | To search for a product - enter: \"S\" | To quit - enter: \"Q\"");
    Console.ResetColor();
    string choice = Console.ReadLine();
    if (choice == "P" ||  choice == "p")
    {
        EnterProduct();
    }
    else if (choice == "S" || choice == "s")
    {
        Search();
    }
    else if (choice == "Q" ||  choice == "q")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nThank you for using this program!");
        Console.ResetColor();
        return;
    } else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Incorrect option, please try again!");
        Console.ResetColor();
        ShowChoices();
    }
}

void Search()
{
    Console.Write("Enter a product name: ");
    string searchTerm = Console.ReadLine();
    Console.WriteLine("----------------------------------------------------------------");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
    Console.ResetColor();
    List<Product> sortedProducts = products.OrderBy(Product => Product.Price).ToList(); //uses Product object as parameter, and orders by Price attribute using LINQ
    foreach (Product product in sortedProducts)
    {
        if (product.ProdName.ToLower() == searchTerm.Trim().ToLower())
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(product.Category.PadRight(20) + product.ProdName.PadRight(20) + product.Price);
            Console.ResetColor();
        }    
        else { 
        Console.WriteLine(product.Category.PadRight(20) + product.ProdName.PadRight(20) + product.Price);
        }
    }
    Console.WriteLine("----------------------------------------------------------------");
    ShowChoices();

}

// Klass-deklaration

class Product
{
    public Product(string category, string prodName, int price)
    {
        Category = category;
        ProdName = prodName;
        Price = price;
    }

    public string Category { get; set; }
    public string ProdName { get; set; }
    public int Price { get; set; }
}
