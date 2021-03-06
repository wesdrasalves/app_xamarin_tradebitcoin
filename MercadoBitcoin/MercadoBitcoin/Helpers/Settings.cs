﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MercadoBitcoin.Helpers
{
    public static class Settings
    {
        public const string FILE_STORE = "MBData.wa";
        public const string SEPARATOR_DATA = "#$|$#";

        public const double VALUE_BIG_UP = 1500; 
        public const double VALUE_BIG_DOWN = 1500;
        public const double SECONDS_CHECK_UPDATE = 15;
        public const double FILTER_BIG_ORDERS = 3;
    }
}
