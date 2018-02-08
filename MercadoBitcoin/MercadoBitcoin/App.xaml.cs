using MercadoBitcoin.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MercadoBitcoin
{
	public partial class App : Application
	{
        public App()
		{
			InitializeComponent();

			SetMainPage();
		}

		public static void SetMainPage()
		{

            Current.MainPage = new Main();
                /*TabbedPage
                {
                Children =
                {
                    new NavigationPage(new BuyOrdersPage())
                    {
                        Title = "Orders Buy",
                        Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    },
                    new NavigationPage(new SellOrdersPage())
                    {
                        Title = "Orders Sell",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                    new NavigationPage(new TradePage())
                    {
                        Title = "Buy & Sell",
                        Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    },
                }
            };*/
        }
	}
}
