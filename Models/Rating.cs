using eshop.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace eshop.Models
{
    [Table(nameof(Rating))]
    public class Rating : Entity
    {
        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public decimal Stars { get; set; }

        public bool Rated { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
