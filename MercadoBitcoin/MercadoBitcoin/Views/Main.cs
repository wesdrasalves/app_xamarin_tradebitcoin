using Dotend.MBTrade;
using MercadoBitcoin.Helpers;
using System;
using Xamarin.Forms;

namespace MercadoBitcoin.Views
{
    public class Main : TabbedPage
    {
        public Main()
        {
            //LoadData();
            Children.Add(new NavigationPage(new BuyOrdersPage()) { Title = "Orders Buy" });
            Children.Add(new NavigationPage(new SellOrdersPage()) { Title = "Orders Sell" });
            Children.Add(new NavigationPage(new TradePage()) { Title = "Buy & Sell" });
        }

         private async  void LoadData()
        {
            string _data = await PCLHelper.ReadAllTextAsync(Settings.FILE_STORE);

            string[] _arrData = _data.Split(new string[] { Settings.SEPARATOR_DATA }, StringSplitOptions.RemoveEmptyEntries);
            MBTAPI.RenewInstance(_arrData[1], _arrData[0], "5992");

        }
    }
}