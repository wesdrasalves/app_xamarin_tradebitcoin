using System;
using System.Collections.Generic;
using System.Text;

namespace MercadoBitcoin.Models
{
    public class CoinData : BaseDataObject
    {
        decimal _volume = 0;
        decimal _value = 0;

        public decimal Volume
        {
            get { return this._volume; }
            set { SetProperty(ref _volume, value);  }
        }

        public decimal Value
        {
            get { return this._value; }
            set { SetProperty(ref _value, value);  }
        }
    }
}
