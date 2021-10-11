using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Eshop_Krajicek
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Database db = Database.Instance();
            db.CreateEshopDatabase();
            MainPage = new NavigationPage(new View.TabbedPage1());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
