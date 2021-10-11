using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Eshop_Krajicek.Model
{
    public class Product : ViewModel.ViewModelBase
    {
        public int ProductID { get; set; }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        private double sale;
        public double Sale
        {
            get { return sale; }
            set { sale = value; OnPropertyChanged(nameof(Sale)); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; OnPropertyChanged(nameof(Category)); }
        }



        public Product(int id, double price, double sale, string name, string description, string category)
        {
            ProductID = id;
            Price = price;
            Sale = sale;
            Name = name;
            Description = description;
            Category = category;
        }

        public override string ToString()
        {
            return $"Id: {ProductID}; Nazev: {Name}; Cena: {Price}; Sleva: {Sale}; Kategorie: {Category}; Popis: {Description}";
        }
    }
}
