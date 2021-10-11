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
    public partial class ProductDetailView : ContentPage
    {
        private Product productToUpdate;
        public ProductDetailView(Product product)
        {
            BindingContext = product;
            productToUpdate = product;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.UpdateProductView(productToUpdate));
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet($"Delete product {productToUpdate.Name}?", "CANCEL", "ACCEPT");
            if (action == "ACCEPT")
            {
                Database db = Database.Instance();
                db.DeleteProduct(productToUpdate.ProductID);

                await DisplayAlert("Product deleted", $"Product {productToUpdate.Name} has been deleted successfully", "OK");
                await Navigation.PushAsync(new View.AllProductsView());
            }

        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}