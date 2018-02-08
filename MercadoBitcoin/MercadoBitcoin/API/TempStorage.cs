using System;
using System.Collections.Generic;
using System.Text;
using Dotend.MBTrade.DTO;
using Dotend.MBTrade;

namespace Dotend.MBTrade
{
    public class TempStorage
    {
        private static TempStorage _instance;

        public List<DTOMBOrder> ListMyOrders
        {
            get;
            private set;
        }
        public List<DTOMBCoinData> ListSell
        {
            get;
            private set;
        }

        public List<DTOMBCoinData> ListBuy
        {
            get;
            private set;
        }

        public DTOMBMyFunds Myfunds
        {
            get;
            private set;
        }


        public static TempStorage Instance
        {
            get
            {
                if (TempStorage._instance == null)
                    TempStorage._instance = new TempStorage();

                return TempStorage._instance;
            }
        }


        public void ReloadDatas()
        {
            this.ListMyOrders = MBTAPI.Instance.getMyOpenOrders( MBEnumerables.CoinType.Bit);

            DTOMBOrderBook _orderBooks = MBTAPI.Instance.getLastOrders(); ;
            
            this.ListBuy = _orderBooks.bids;
            this.ListSell = _orderBooks.asks;

            this.Myfunds = MBTAPI.Instance.getMyInfoAccount();
        }
    }
}
