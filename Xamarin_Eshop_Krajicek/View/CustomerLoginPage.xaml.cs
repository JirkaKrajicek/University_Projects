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
    public partial class CustomerLoginPage : ContentPage
    {
        public static int customerID { get; set; }
        public CustomerLoginPage()
        {
            InitializeComponent();
        }
        private async void button1_Clicked_1(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string pswrd = textBoxPassword.Text;

            //await DisplayAlert("Ouch!", "Prvni vlastovky", "Lets move on");
            Database db = Database.Instance();
            List<Customer> custList = db.GetAllCustomers();
            bool bull = false;

            foreach (Customer c in custList)
            {
                if (name == c.Name && pswrd == c.Address)
                {
                    bull = true;
                    customerID = c.CustomerID;
                    await DisplayAlert("Welcome", "Acces granted", "Continue");
                    await Navigation.PushAsync(new View.CustomerTabbedPage());
                    break;
                }
            }

            if (bull == false)
            {
                await DisplayAlert("Error", "Acces denied", "Try again");
            }

            textBoxName.Text = String.Empty;
            textBoxPassword.Text = String.Empty;

        }

        private async void button2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new View.RegisterNewCustomerView());
        }
    }
}