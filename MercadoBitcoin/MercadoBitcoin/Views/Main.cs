using Xamarin.Forms;

namespace MercadoBitcoin.Views
{
    public class Main : TabbedPage
    {
        public Main()
        {
            Children.Add(new NavigationPage(new BuyOrdersPage()) { Title = "Orders Buy" });
            Children.Add(new NavigationPage(new SellOrdersPage()) { Title = "Orders Sell" });
            Children.Add(new NavigationPage(new TradePage()) { Title = "Buy & Sell" });
        }
    }
}