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
    public partial class CustomerCartView : ContentPage
    {
        public CustomerCartView()
        {
            BindingContext = new ViewModel.CustomerCardViewModel();
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (ViewModel.CustomerCardViewModel.ItemsInCard.Count != 0)
            {
                DateTime date = DateTime.Now;
                int thisYear = date.Year;
                DateTime DTspring = new DateTime(thisYear, 3, 20);
                DateTime DTsummer = new DateTime(thisYear, 6, 21);
                DateTime DTautumn = new DateTime(thisYear, 9, 22);
                DateTime DTwinter = new DateTime(thisYear, 12, 21);


                Database db = Database.Instance();
                string state = "New order";
                db.CreateNewOrder(View.CustomerLoginPage.customerID, 0, state, date);
                int orderID = db.GetOrderID(View.CustomerLoginPage.customerID, date);

                double orderPrice = 0;
                int howManyItems = 0; //kvuli slevam

                foreach (Item item in ViewModel.CustomerCardViewModel.ItemsInCard)
                {
                    double itemPrice = item.Price;

                    if (date >= DTautumn && date < DTwinter) //autumn sale
                    {
                        double sale = db.GetProductSale(item.ProductID);
                        itemPrice = itemPrice * ((100 - sale) / 100);
                    }

                    db.CreateNewItem(orderID, item.Name, item.ProductID, itemPrice, item.Count);
                    orderPrice = orderPrice + (item.Price * item.Count);
                    howManyItems = howManyItems + item.Count;
                }

                //aplikace slev******************************************************************************************                

                if (date >= DTspring && date < DTsummer) //spring sale
                {
                    Model.EventSale sale = new Model.SpringSale();
                    double price = sale.GetSale(orderPrice, howManyItems, 0);

                    await db.UpdateOrderPrice(orderID, price);
                }
                else if (date >= DTsummer && date < DTautumn) //summer sale
                {
                    List<Order> listCustomerOrder = db.GetCustomersOrders(View.CustomerLoginPage.customerID);
                    int orderCount = listCustomerOrder.Count;

                    Model.EventSale sale = new Model.SummerSale();
                    double price = sale.GetSale(orderPrice, 0, orderCount);

                    await db.UpdateOrderPrice(orderID, price);
                }
                else if (date >= DTwinter || date < DTspring) //winter sale
                {
                    Model.EventSale sale = new Model.WinterSale();
                    double price = sale.GetSale(orderPrice, howManyItems, 0);

                    await db.UpdateOrderPrice(orderID, price);
                }
                else
                {
                    await db.UpdateOrderPrice(orderID, orderPrice);
                }
                await DisplayAlert("Success", "New order has been created", "OK");
                ViewModel.CustomerCardViewModel.ItemsInCard.Clear();
                await Navigation.PushAsync(new CustomerTabbedPage());
            }
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Item item)
            {
                Navigation.PushAsync(new ItemInCartDetailView(item));
            }
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TabbedPage1());
        }
    }
}