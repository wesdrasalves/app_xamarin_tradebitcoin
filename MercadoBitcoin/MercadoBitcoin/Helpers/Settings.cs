using System;
using System.Collections.Generic;
using System.Text;

namespace MercadoBitcoin.Helpers
{
    public static class Settings
    {
        public const string PublicKey =  "f5a144afd52dab762047fcd7607b5445";
        public const string PrivateKey = "1ee74121d6205609cb6b42d1f56fc1ea82f8aac6b838431794d20a5d13dc00ce";

        public const double VALUE_BIG_UP = 1500; 
        public const double VALUE_BIG_DOWN = 1500;
        public const double SECONDS_CHECK_UPDATE = 15;
        public const double FILTER_BIG_ORDERS = 3;
    }
}
