using MercadoBitcoin.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotend.MBTrade
{
    public class MBUtils
    {
        //private double _oldValueBuy = 0;
        //private double _oldVolumeBuy = 0;
        //private double _oldValueSell = 0;
        //private double _oldVolumeSell = 0;
        //private double _oldValueReal = 0;
        //private double _oldValueRealTotal = 0;
        //private double _oldValueBit = 0;
        //private double _oldValueBitTotal = 0;
        private DateTime _lastDateTimeUpdate = DateTime.MinValue;

        public double OldValueBuy{ get; private set;}
        public double OldVolumeBuy { get; private set;}
        public double OldValueSell { get; private set;}
        public double OldVolumeSell { get; private set;}
        public double OldValueReal { get; private set;}
        public double OldValueRealTotal { get; private set;}
        public double OldValueBit { get; private set; }
        public double OldValueBitTotal { get; private set; }


        private static MBUtils _instance = null;

        public static MBUtils Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new MBUtils();
                    _instance.Restart();
                }

                return _instance;
            }
        }

        public void Restart()
        {
            _instance._lastDateTimeUpdate = DateTime.Now.AddSeconds(-(Settings.SECONDS_CHECK_UPDATE + 1));
            _instance.UpdateAllValues();
        }

        private void UpdateAllValues()
        {

            this.UpdateStore();

            if (TempStorage.Instance.Myfunds != null)
            {
                this.OldValueReal = TempStorage.Instance.Myfunds.balanceBRLAvaliable;
                this.OldValueRealTotal = TempStorage.Instance.Myfunds.balanceBRLTotal;
                this.OldValueBit = TempStorage.Instance.Myfunds.balanceBTCAvaliable;
                this.OldValueBitTotal = TempStorage.Instance.Myfunds.balanceBTCTotal;
            }

            if (TempStorage.Instance.ListSell != null && TempStorage.Instance.ListSell.Count > 0)
            {
                this.OldValueSell = TempStorage.Instance.ListSell[0].Value;
                this.OldVolumeSell = TempStorage.Instance.ListSell[0].Volume;
            }

            if (TempStorage.Instance.ListBuy != null && TempStorage.Instance.ListBuy.Count > 0)
            {
                this.OldValueBuy = TempStorage.Instance.ListBuy[0].Value;
                this.OldVolumeBuy = TempStorage.Instance.ListBuy[0].Value;
            }
        }

        private void UpdateStore()
        {
            if (DateTime.Now.AddSeconds(-Settings.SECONDS_CHECK_UPDATE) > this._lastDateTimeUpdate)
            {
                this._lastDateTimeUpdate = DateTime.Now;
                TempStorage.Instance.ReloadDatas();
            }

        }

        public bool CheckBuy()
        {
            this.UpdateStore();

            if(TempStorage.Instance.Myfunds != null)
                if (TempStorage.Instance.Myfunds.balanceBTCAvaliable != OldValueBit)
                {
                    OldValueBit = TempStorage.Instance.Myfunds.balanceBTCAvaliable;
                    return true;
                }

            return false;
        }

        public bool CheckSell()
        {
            this.UpdateStore();

            if(TempStorage.Instance.Myfunds != null)
                if (TempStorage.Instance.Myfunds.balanceBRLAvaliable != OldValueReal)
                {
                    OldValueReal = TempStorage.Instance.Myfunds.balanceBRLAvaliable;
                    return true;
                }

            return false;
        }

        public bool CheckBigUp()
        {
            this.UpdateStore();

            double _value;

            if(TempStorage.Instance.ListSell != null && TempStorage.Instance.ListSell.Count > 0)
            {
                _value = TempStorage.Instance.ListSell[0].Value;

                if((_value - this.OldValueSell) >= Settings.VALUE_BIG_UP)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckBigDown()
        {
            this.UpdateStore();

            double _value;

            if (TempStorage.Instance.ListBuy != null && TempStorage.Instance.ListBuy.Count > 0)
            {
                _value = TempStorage.Instance.ListBuy[0].Value;

                if ((this.OldValueBuy - _value) >= Settings.VALUE_BIG_DOWN)
                {
                    return true;
                }
            }
            return false;
        }



    }
}
