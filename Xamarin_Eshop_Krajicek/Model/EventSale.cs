using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Eshop_Krajicek.Model
{
    abstract public class EventSale
    {
        public abstract double GetSale(double price, int productCount, int orderCount);
    }
    public class SpringSale : EventSale
    {
        public override double GetSale(double price, int productCount, int orderCount)
        {
            if ((productCount >= 3) && (productCount < 8))
            {
                price = price * 0.8;
            }
            else if ((productCount >= 8) && (productCount < 12))
            {
                price = price * 0.7;
            }
            else if (productCount >= 12)
            {
                price = price * 0.6;
            }

            return price;
        }

    }

    public class SummerSale : EventSale
    {
        public override double GetSale(double price, int productCount, int orderCount)
        {
            if ((orderCount >= 5) && (orderCount < 10))
            {
                price = price * 0.9;
            }
            else if (orderCount >= 10)
            {
                price = price * 0.8;
            }

            return price;
        }
    }

    public class WinterSale : EventSale //pri koupi 3/6/9 produktu s cenou 100 a vic/250 a vic/450 a vic sleva na objednavku 10%/15%/20%
    {
        public override double GetSale(double price, int productCount, int orderCount)
        {
            if ((price >= 100) && (productCount) >= 3 && (productCount <6))
            {
                price = price * 0.9;
            }
            else if ((price >= 250) && (productCount) >= 6 && (productCount < 9))
            {
                price = price * 0.85;
            }
            else if ((price >= 450) && (productCount) >= 9)
            {
                price = price * 0.8;
            }

            return price;
        }
    }
}
