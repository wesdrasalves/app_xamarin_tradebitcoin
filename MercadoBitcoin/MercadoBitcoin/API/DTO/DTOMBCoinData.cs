using System;
using System.Collections.Generic;
using System.Text;

namespace Dotend.MBTrade.DTO
{
    public class DTOMBCoinData 
    {
        double _volume = 0;
        double _value = 0;

        public double Volume
        {
            get { return this._volume; }
            set { this._volume = value; }
        }

        public double Value
        {
            get { return this._value; }
            set { this._value = value; }
        }


    }
}
