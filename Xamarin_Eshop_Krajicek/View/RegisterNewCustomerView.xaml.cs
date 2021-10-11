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
    public partial class RegisterNewCustomerView : ContentPage
    {
        public RegisterNewCustomerView()
        {
            InitializeComponent();
        }

        private async void button1_Clicked(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string address = textBoxAddress.Text;

            Database db = Database.Instance();
            db.CreateNewCustomer(name, address);

            await DisplayAlert("Welcome", $"Account name {name} with address {address} has been created", "Ok");
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}