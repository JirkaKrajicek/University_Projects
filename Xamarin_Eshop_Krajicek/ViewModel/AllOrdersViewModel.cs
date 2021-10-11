using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek.ViewModel
{
    public class AllOrdersViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public AllOrdersViewModel()
        {
            Orders = ShowAllOrders();
        }
        private ObservableCollection<Order> ShowAllOrders()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            Database db = Database.Instance();
            List<Order> listOrder = db.GetAllOrders();

            foreach (Order o in listOrder)
            {
                orders.Add(o);
            }
            return orders;
        }
    }
}
