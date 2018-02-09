using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using MercadoBitcoin.Models;
using Dotend.MBTrade;
using Dotend.MBTrade.DTO;
using MercadoBitcoin.Helpers;
using System.Linq;

namespace MercadoBitcoin.ViewModels
{

    class OrdersViewModel : BaseViewModel
    {
        public Command LoadDataCommad;
        public ObservableRangeCollection<DTOMBCoinData> ListBuyCoin { get; set; }
        public ObservableRangeCollection<DTOMBCoinData> ListSellCoin { get; set; }
        private bool _isOnFilter = false;

        public bool isOnFilter {
            get { return this._isOnFilter; }
            set {
                
                if(this._isOnFilter != value)
                {
                    this._isOnFilter = value;
                    this.ExecuteLoadDataCommand();
                }
            }
        }

        private static OrdersViewModel _instance;

        public static OrdersViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OrdersViewModel();

                return _instance;
            }
        }


        private OrdersViewModel()
        {
            Title = "Public Orders";
            ListBuyCoin = new ObservableRangeCollection<DTOMBCoinData>();
            ListSellCoin = new ObservableRangeCollection<DTOMBCoinData>();
            isOnFilter = false;
            LoadDataCommad = new Command( () =>  ExecuteLoadDataCommand());
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command( () =>
                {
                    ExecuteLoadDataCommand();
                });
            }
        }


         void  ExecuteLoadDataCommand()
        {
            DTOMBOrderBook _orderBooks;

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _orderBooks = MBTAPI.Instance.getLastOrders();

                this.ListSellCoin.Clear();
                this.ListBuyCoin.Clear();

                if (_orderBooks != null && 
                    _orderBooks.asks != null &&
                    _orderBooks.bids != null)
                    if(this.isOnFilter)
                    {

                        this.ListSellCoin.AddRange((from _o in _orderBooks.asks
                                                    where _o.Volume >= Settings.FILTER_BIG_ORDERS
                                                    select _o).ToList());
                        this.ListBuyCoin.AddRange((from _o in _orderBooks.bids
                                                   where _o.Volume >= Settings.FILTER_BIG_ORDERS
                                                   select _o).ToList());
                    }
                    else
                    {
                        this.ListSellCoin.AddRange(_orderBooks.asks);
                        this.ListBuyCoin.AddRange(_orderBooks.bids);
                    }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
