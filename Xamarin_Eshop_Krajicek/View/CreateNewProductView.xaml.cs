using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Eshop_Krajicek.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewProductView : ContentPage
    {
        public CreateNewProductView()
        {
            InitializeComponent();
        }

        private async void button1_Clicked(object sender, EventArgs e)
        {
            string name = textBoxNewProductName.Text;
            double price = Double.Parse(textBoxNewProductPrice.Text);
            string category = textBoxNewProductCategory.Text;
            string description = textBoxNewProductDescription.Text;

            Database db = Database.Instance();
            db.CreateNewProduct(price, 0, name, description, category);

            await DisplayAlert("Success", $"New product {name} added", "OK");
            await Navigation.PushAsync(new View.AllProductsView());
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}