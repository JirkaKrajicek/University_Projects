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
    public partial class UpdateProductView : ContentPage
    {
        private Product productToUpdate;
        public UpdateProductView(Product product)
        {
            BindingContext = product;
            productToUpdate = product;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Database db = Database.Instance();
            db.UpdateProduct(productToUpdate.ProductID, productToUpdate.Name, productToUpdate.Category, productToUpdate.Price, productToUpdate.Sale, productToUpdate.Description);
            await Navigation.PushAsync(new AllProductsView());
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}