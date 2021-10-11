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
    public partial class AdminUpdateOrderView : ContentPage
    {
        private Order selectedOrder = AdminAllOrdersView.selectedOrder;
        public AdminUpdateOrderView()
        {
            BindingContext = selectedOrder;
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Database db = Database.Instance();
            await db.UpdateOrderState(selectedOrder.OrderID, selectedOrder.State);
            await DisplayAlert("Success", $"Order {selectedOrder.OrderID} has been updated", "Ok");
            await Navigation.PushAsync(new AdminTabbedPageView());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (selectedOrder.State == "Confirmed")
            {
                Database db = Database.Instance();
                string state = "Confirmed and Send";
                await db.UpdateOrderState(selectedOrder.OrderID, state);
                await DisplayAlert("Success", $"Order {selectedOrder.OrderID} has been sent", "Ok");
                await Navigation.PushAsync(new AdminTabbedPageView());
            }
            else
            {
                await DisplayAlert("Error", "Order must be 'Confirmed' in order to be sent", "Ok");
            }
        }
    }
}