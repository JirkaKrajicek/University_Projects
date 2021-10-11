using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Eshop_Krajicek.Model
{
    public class Order : ViewModel.ViewModelBase
    {
        private int orderID;
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; OnPropertyChanged(nameof(OrderID)); }
        }

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; OnPropertyChanged(nameof(CustomerID)); }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; OnPropertyChanged(nameof(State)); }
        }
        public DateTime Date { get; set; }

        public Order(int orderID, int customerID, double price, string state, DateTime date)
        {
            OrderID = orderID;
            CustomerID = customerID;
            Price = price;
            State = state;
            Date = date;
        }

        public override string ToString()
        {
            return $"Id: {OrderID}; ZakaznikId: {CustomerID}; Cena: {Price}; Datum: {Date}; Stav: {State}";
        }
    }
}
