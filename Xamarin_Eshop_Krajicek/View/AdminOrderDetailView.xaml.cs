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
    public partial class AdminOrderDetailView : ContentPage
    {
        private Order selectedOrder = AdminAllOrdersView.selectedOrder;
        public AdminOrderDetailView()
        {
            BindingContext = new ViewModel.OrderDetailViewModel(selectedOrder);
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}