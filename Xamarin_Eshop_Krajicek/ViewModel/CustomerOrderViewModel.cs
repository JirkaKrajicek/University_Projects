using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek.ViewModel
{
    public class CustomerOrderViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public CustomerOrderViewModel()
        {
            Orders = ShowOrders();
        }

        private ObservableCollection<Order> ShowOrders()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            Database db = Database.Instance();

            List<Order> listOrders = db.GetCustomersOrders(View.CustomerLoginPage.customerID);
            foreach (Order o in listOrders)
            {
                orders.Add(o);
            }
            return orders;

        }
    }
}
