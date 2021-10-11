using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Eshop_Krajicek.Model
{
    class Customer
    {
        public readonly int CustomerID;
        public string Name { get; set; }
        public string Address { get; set; }

        public Customer(int id, string name, string address)
        {
            CustomerID = id;
            Name = name;
            Address = address;
        }
    }
}
