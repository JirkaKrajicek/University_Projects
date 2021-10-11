using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek.ViewModel
{
    class CustomerCardViewModel : ViewModelBase
    {
        public static List<Item> ItemsInCard = new List<Item>();
        public ObservableCollection<Item> items { get; set; }

        public CustomerCardViewModel()
        {
            items = new ObservableCollection<Item>(ItemsInCard);
        }

    }
}
