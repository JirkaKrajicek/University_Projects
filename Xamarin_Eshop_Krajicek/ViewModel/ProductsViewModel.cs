using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek.ViewModel
{
    class ProductsViewModel : ViewModelBase
    {
        public ObservableCollection<Product> Products { get; set; }

        public ProductsViewModel()
        {
            Products = ShowProducts();
        }

        public ObservableCollection<Product> ShowProducts()
        {
            //Products.Clear();
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            Database db = Database.Instance();
            List<Product> listProducts = db.GetAllProducts();

            foreach (Product p in listProducts)
            {
                products.Add(p);
            }
            return products;

        }

    }
}
