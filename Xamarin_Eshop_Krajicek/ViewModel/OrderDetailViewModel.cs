using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek.ViewModel
{
    public class OrderDetailViewModel : ViewModelBase
    {
        public Order Order { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public OrderDetailViewModel(Order order)
        {
            Order = order;
            Items = ShowAllItems();
        }

        private ObservableCollection<Item> ShowAllItems()
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>();
            Database db = Database.Instance();
            List<Item> itemsList = db.GetOrderItems(Order.OrderID);
            foreach (Item item in itemsList)
            {
                items.Add(item);
            }

            return items;
        }
    }
}
