using eshop.Models.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table("Carousel")]
    public class Carousel : Entity
    {
        [Required]
        public string DataTarget { get; set; }
        [Required]
        [StringLength(225)]
        public string ImageSrc { get; set; }
        [Required]
        [StringLength(50)]
        public string ImageAlt { get; set; }
        [Required]
        public string CarouselContent { get; set; }
        [NotMapped]
        [FileContentType("image")]
        public IFormFile Image { get; set; }
    }
}
