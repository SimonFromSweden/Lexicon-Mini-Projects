using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectW3
{
    internal class Computer : Asset
    {
        public Computer(string brand, string model, DateTime purchasedate, int price, string office)
        {
            Brand = brand;
            Model = model;
            PurchaseDate = purchasedate;
            Price = price;
            Office = office;
            Type = "Computer";
        }
    }
}
