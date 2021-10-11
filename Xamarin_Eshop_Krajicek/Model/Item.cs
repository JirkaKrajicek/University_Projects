using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Eshop_Krajicek.Model
{
    public class Item
    {
        public readonly int ItemID;
        public int OrderID { get; set; }
        public int ProductID { get; set; }

        public string Name { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

        public double PriceCount { get; set; }

        public Item(int productID, string name, int count, double price)
        {
            //OrderID = orderID;
            ProductID = productID;
            Name = name;
            Count = count;
            Price = price;
            PriceCount = count * price;
        }

        public override string ToString()
        {
            return $"Id: {ItemID}; ObjednavkaId: {OrderID}; ProduktId: {ProductID}; Nazev: {Name}; Mnozstvi: {Count}; Cena: {Price}";
        }
    }
}
