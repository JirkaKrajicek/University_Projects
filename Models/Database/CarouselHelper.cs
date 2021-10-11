using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()
        {
            DateTime date = DateTime.Now;
            IList<Carousel> carousels = new List<Carousel>
            {

                new Carousel()
                {

                    DataTarget = "#myCarousel",
                    ImageSrc = "/images/Carousels/car_banner1.jpg",
                    ImageAlt = "MultipleCars",
                    CarouselContent = "All models customazible.\n",
                    DateCreated = date,
                },
                new Carousel()
                {

                    DataTarget = "#myCarousel",
                    ImageSrc = "/images/Carousels/car_banner2.jpg",
                    ImageAlt = "RustyCar",
                    CarouselContent = "Our technicians will never let you down\n",
                    DateCreated = date,

                },
            };
            return carousels;
        }
    }
}
