using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dotend.MBTrade;
using Dotend.MBTrade.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration;
using MercadoBitcoin.ViewModels;
using MercadoBitcoin.Models;
using MercadoBitcoin.Helpers;

namespace MercadoBitcoin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TradePage : ContentPage
    {
        TradeViewModel viewModel;

        public TradePage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new TradeViewModel();

            Total3Buy.Text = "";
            Total7Buy.Text = "";
            Total3Sell.Text = "";
            Total7Sell.Text = "";
            bitTotal.Text = "0";
            realTotal.Text = "0";
            bitAvailable.Text = "0";
            realAvailablel.Text = "0";

            Device.StartTimer(TimeSpan.FromSeconds(5.0 / 60), OnTimerTick);

        }

        bool OnTimerTick()
        {
            if (TempStorage.Instance.Myfunds != null)
            {
                bitTotal.Text = TempStorage.Instance.Myfunds.balanceBTCTotal.ToString("0.00000");
                realTotal.Text = TempStorage.Instance.Myfunds.balanceBRLTotal.ToString("R$ 0.00000");
                bitAvailable.Text = TempStorage.Instance.Myfunds.balanceBTCAvaliable.ToString("0.00000");
                realAvailablel.Text = TempStorage.Instance.Myfunds.balanceBRLAvaliable.ToString("R$ 0.00000");
            }
            return true;
        }

        private void VolumeBuy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (VolumeBuy.Text != "" && ValueBuy.Text != "")
            {
                double _value;
                double _volume;
                double _valueFinal;

                if (double.TryParse(ValueBuy.Text, out _value) &&
                double.TryParse(VolumeBuy.Text, out _volume))
                {
                    _valueFinal = _value * _volume;

                    Total3Buy.Text = (_valueFinal - (_valueFinal * 0.003)).ToString("R$ 0.00000");
                    Total7Buy.Text = (_valueFinal - (_valueFinal * 0.007)).ToString("R$ 0.00000");
                }
            }
        }

        private void VolumeSell_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (VolumeBuy.Text != "" && ValueBuy.Text != "")
            {
                double _value;
                double _volume;
                double _valueFinal;

                if (double.TryParse(ValueSell.Text, out _value) && double.TryParse(VolumeSell.Text, out _volume))
                {
                    _valueFinal = _value * _volume;

                    Total3Sell.Text = (_valueFinal - (_valueFinal * 0.003)).ToString();
                    Total7Sell.Text = (_valueFinal - (_valueFinal * 0.007)).ToString();
                }
            }
        }

        private void Buy_Clicked(object sender, EventArgs e)
        {
            double _value;
            double _volume;
            DTOMBOrder _order = null;

            if (double.TryParse(ValueBuy.Text, out _value) && double.TryParse(VolumeBuy.Text, out _volume))
            {
                if((_volume * _value) > TempStorage.Instance.Myfunds.balanceBRLAvaliable)
                {
                    DisplayAlert("Buy Alert", "You have no balance for that trasaction.","Ok");
                    return;
                }

                _order = MBTAPI.Instance.setBitCoinTradeBuy(_volume, _value);
                ListMyOrders.BeginRefresh();
                viewModel.ExecuteLoadDataCommand();
                ValueBuy.Text = "";
                VolumeBuy.Text = "";
                ListMyOrders.EndRefresh();
            }
            else
            {
                DisplayAlert("Validation Alert", "Invalid data informed.", "Ok");
            }

        }

        private void Sell_Clicked(object sender, EventArgs e)
        {
            double _value;
            double _volume;
            DTOMBOrder _order = null;

            if (double.TryParse(ValueSell.Text, out _value) && double.TryParse(VolumeSell.Text, out _volume))
            {
                if (_volume > TempStorage.Instance.Myfunds.balanceBTCAvaliable)
                {
                    DisplayAlert("Sell Alert", "You have no balance for that trasaction.", "Ok");
                    return;
                }

                _order = MBTAPI.Instance.setBitCoinTradeSell(_volume, _value);
                ListMyOrders.BeginRefresh();
                viewModel.ExecuteLoadDataCommand();
                ValueSell.Text = "";
                VolumeSell.Text = "";
                ListMyOrders.EndRefresh();
            }
            else
            {
                DisplayAlert("Validation Alert", "Invalid data informed.", "Ok");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ListMyOrders == null || viewModel.ListMyOrders.Count == 0)
                viewModel.LoadDataCommad.Execute(null);
        }

        private void ListMyOrders_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DTOMBOrder _order;
            _order = (DTOMBOrder)e.Item;

            ShowExitDialog(_order.order_id);
        }

        private async void ShowExitDialog(long orderId)
        {
            var answer = await DisplayAlert("Cancel Order", 
                string.Format("Do you wan't to cancel your order number {0}?", orderId), "Yes", "No");
            if (answer)
            {
                MBTAPI.Instance.cancelOrderBit(orderId);
                ListMyOrders.BeginRefresh();
                viewModel.ExecuteLoadDataCommand();
                ListMyOrders.EndRefresh();

            }
        }

        async  void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }
    }
}