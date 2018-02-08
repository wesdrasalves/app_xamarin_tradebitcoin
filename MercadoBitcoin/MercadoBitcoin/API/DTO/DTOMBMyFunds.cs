using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;

namespace Dotend.MBTrade.DTO
{
    public class DTOMBMyFunds : DTOMB
    {
        public double balanceBRLAvaliable { get; set; }
        public double balanceBRLTotal { get; set; }
        public double balanceBTCAvaliable { get; set; }
        public double balanceBTCTotal { get; set; }
        public double balanceLTCAvaliable { get; set; }
        public double balanceLTCTotal { get; set; }

        public DTOMBMyFunds(string pJsonData) : base(pJsonData)
        {
            this.convertJsonToObject(pJsonData);
        }

        private void castObject(Dictionary<string, object> pDicResponse)
        {
            DTOMBOrder _order = new DTOMBOrder();
            NumberFormatInfo _provider = new NumberFormatInfo();
            DateTime _dataBase = new DateTime(1970, 1, 1);
            _provider.NumberDecimalSeparator = ".";
            _provider.NumberGroupSeparator = ",";

            try
            {
                var _balance = (Dictionary<string, object>)pDicResponse["balance"];

                this.balanceBRLAvaliable = Convert.ToDouble(((Dictionary<string, object>)_balance["brl"])["available"], _provider);
                this.balanceBRLTotal = Convert.ToDouble(((Dictionary<string, object>)_balance["brl"])["total"], _provider);

                this.balanceBTCAvaliable = Convert.ToDouble(((Dictionary<string, object>)_balance["btc"])["available"], _provider);
                this.balanceBTCTotal = Convert.ToDouble(((Dictionary<string, object>)_balance["btc"])["total"], _provider);

                this.balanceLTCAvaliable = Convert.ToDouble(((Dictionary<string, object>)_balance["ltc"])["available"], _provider);
                this.balanceLTCTotal = Convert.ToDouble(((Dictionary<string, object>)_balance["ltc"])["total"], _provider);
            }
            catch
            {
                _order = null;
            }
        }

        public override IEnumerable convertJsonToObject(string pJsonData)
        {
            //var _data = new Dictionary<string, object>();// JavaScriptSerializer().DeserializeObject(pJsonData);

            //{ "response_data": { "balance": { "brl": { "available": "855.14133", "total": "855.14133"}, "btc": { "available": "0.00000684", "total": "0.00000684", "amount_open_orders": 0}, "ltc": { "available": "0.00000000", "total": "0.00000000", "amount_open_orders": 0}, "bch": { "available": "0.00000478", "total": "0.00000478", "amount_open_orders": 0}, "btg": { "available": "0.00000478", "total": "0.00000478", "amount_open_orders": 0} }, "withdrawal_limits": { "brl": { "available": "20000.00", "total": "20000.00"}, "btc": { "available": "25.00000000", "total": "25.00000000"}, "ltc": { "available": "500.00000000", "total": "500.00000000"}, "bch": { "available": "25.00000000", "total": "25.00000000"}, "btg": { "available": 0, "total": 0} } }, "status_code": 100, "server_unix_timestamp": "1518026362"}

            var _t = new {
                response_data = new
                {
                    balance = new {
                        brl = new {
                            available = "",
                            total = "",
                        },
                        btc = new
                        {
                            available = "",
                            total = "",
                            amount_open_orders = ""
                        },
                        ltc = new
                        {
                            available = "",
                            total = "",
                            amount_open_orders = ""
                        }
                    },
                    withdrawal_limits = new {
                        brl = new
                        {
                            available = "",
                            total = "",
                            amount_open_orders = ""
                        },
                        btc = new
                        {
                            available = "",
                            total = "",
                            amount_open_orders = ""
                        },
                        ltc = new
                        {
                            available = "",
                            total = "",
                            amount_open_orders = ""
                        }
                    }
                },
                status_code = 0,
                server_unix_timestamp =""
            };

            var _data = JsonConvert.DeserializeAnonymousType(pJsonData, _t);


            if (_data != null)
            {
                this.balanceBRLAvaliable = Convert.ToDouble(_data.response_data.balance.brl.available.Replace('.',','));
                this.balanceBRLTotal = Convert.ToDouble(_data.response_data.balance.brl.total.Replace('.', ','));

                this.balanceBTCAvaliable = Convert.ToDouble(_data.response_data.balance.btc.available.Replace('.', ','));
                this.balanceBTCTotal = Convert.ToDouble(_data.response_data.balance.btc.total.Replace('.', ','));

                this.balanceLTCAvaliable = Convert.ToDouble(_data.response_data.balance.ltc.available.Replace('.', ','));
                this.balanceLTCTotal = Convert.ToDouble(_data.response_data.balance.ltc.total.Replace('.', ','));
            }

            return base.convertJsonToObject(pJsonData);
        }
    }
}