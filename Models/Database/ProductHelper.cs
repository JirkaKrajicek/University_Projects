using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class ProductHelper
    {
        public static IList<Product> GenerateProducts()
        {
            DateTime date = DateTime.Now;
            IList<Product> products = new List<Product>
            {
                new Product()
                {
                    
                    ProductName = "Dodge-Challenger",
                    ImageSrc = "/images/Products/car1.jpg",
                    ImageAlt = "dodge_challenger_coupe",
                    Price = 400000,
                    DateCreated = date,
                },

                new Product()
                {
                    
                    ProductName = "Lamborghini",
                    ImageSrc = "/images/Products/car2.jpg",
                    ImageAlt = "black_lamborghini",
                    Price = 550000,
                     DateCreated = date,
                },

                new Product()
                {
                    
                    ProductName = "Ford-Mustang",
                    ImageSrc = "/images/Products/car3.jpg",
                    ImageAlt = "for_mustang_model_1967",
                    Price = 1200000,
                     DateCreated = date,
                },

            };
            return products;
        }
    }
}
