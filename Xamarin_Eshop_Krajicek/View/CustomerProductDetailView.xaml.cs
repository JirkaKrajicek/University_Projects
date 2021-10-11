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
    public partial class CustomerProductDetailView : ContentPage
    {
        private Product selectedProduct;

        public CustomerProductDetailView(Product product)
        {
            BindingContext = product;
            selectedProduct = product;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Item i = new Item(selectedProduct.ProductID, selectedProduct.Name, Convert.ToInt32(labelAmount.Text), selectedProduct.Price);
            ViewModel.CustomerCardViewModel.ItemsInCard.Add(i);            

            double price = Convert.ToDouble(labelAmount.Text) * selectedProduct.Price;

            await DisplayAlert("New item in card", $"Item {selectedProduct.Name} in amount of {labelAmount.Text} has been added in your card. Its price is {price}", "OK");
            await Navigation.PushAsync(new View.CustomerTabbedPage()); //store
        }

        private void stepper1_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            labelAmount.Text = Convert.ToString(stepper1.Value);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}