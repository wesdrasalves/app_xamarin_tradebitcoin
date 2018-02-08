using Dotend.MBTrade;
using Dotend.MBTrade.DTO;
using MercadoBitcoin.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MercadoBitcoin.ViewModels
{
    public class TradeViewModel : BaseViewModel
    {
        public Command LoadDataCommad;
        public ObservableRangeCollection<DTOMBOrder> ListMyOrders { get; set; }
        public string ValueBitTotal
        {
            get
            {
                if(TempStorage.Instance.Myfunds != null)
                {
                    return TempStorage.Instance.Myfunds.balanceBTCTotal.ToString();
                }

                return "0";
            }
        }

        public string ValueRealTotal
        {
            get
            {
                if (TempStorage.Instance.Myfunds != null)
                {
                    return "R$" + TempStorage.Instance.Myfunds.balanceBRLTotal.ToString();
                }

                return "0";
            }
        }


        public TradeViewModel()
        {
            Title = "Trades";
            ListMyOrders = new ObservableRangeCollection<DTOMBOrder>();
            LoadDataCommad = new Command(() => ExecuteLoadDataCommand());
        }

        public void ExecuteLoadDataCommand()
        {
            List<DTOMBOrder> _orders;

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                this.ListMyOrders.Clear();
                _orders = MBTAPI.Instance.getMyOpenOrders(MBEnumerables.CoinType.Bit);

                this.ListMyOrders.AddRange(_orders);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    ExecuteLoadDataCommand();
                });
            }
        }
    }
}
