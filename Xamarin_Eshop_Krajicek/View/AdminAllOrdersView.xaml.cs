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
    public partial class AdminAllOrdersView : ContentPage
    {
        public static Order selectedOrder { get; set; }
        public AdminAllOrdersView()
        {
            BindingContext = new ViewModel.AllOrdersViewModel();
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Order order)
            {
                selectedOrder = order;
                Navigation.PushAsync(new AdminOrderDetailTabbedPage());
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}