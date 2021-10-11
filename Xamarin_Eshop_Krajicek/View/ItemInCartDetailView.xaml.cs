using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_Eshop_Krajicek.Model;

namespace Xamarin_Eshop_Krajicek.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemInCartDetailView : ContentPage
    {
        private Item selectedItem { get; set; }
        public ItemInCartDetailView(Item item)
        {
            BindingContext = item;
            selectedItem = item;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Remove item from card?", "Cancel", "Yes");
            if (action == "Yes")
            {
                foreach (Item item in ViewModel.CustomerCardViewModel.ItemsInCard)
                {
                    if (item == selectedItem)
                    {
                        ViewModel.CustomerCardViewModel.ItemsInCard.Remove(item);
                        await DisplayAlert("Item removed", $"Item {selectedItem.Name} has been removed from your card", "Ok");
                        await Navigation.PushAsync(new CustomerTabbedPage());
                        break;
                    }
                }
            }

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}