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
    public partial class AdminLoginPage : ContentPage
    {
        public AdminLoginPage()
        {
            InitializeComponent();
        }

        private async void button1_Clicked(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            string pswrd = textBoxPassword.Text;

            if (name == "admin" && pswrd == "1234")
            {
                await DisplayAlert("Correct", "Acces granted", "Continue");

                await Navigation.PushAsync(new View.AdminTabbedPageView());
            }
            textBoxName.Text = String.Empty;
            textBoxPassword.Text = String.Empty;
        }
    }
}