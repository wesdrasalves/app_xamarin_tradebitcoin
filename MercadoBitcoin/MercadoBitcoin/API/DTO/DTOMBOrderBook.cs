using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Dotend.MBTrade.DTO
{
    public class DTOMBOrderBook : DTOMB
    {
        public List<DTOMBCoinData> asks { get; set; }
        public List<DTOMBCoinData> bids { get; set; }

        public DTOMBOrderBook(string pJsonData) : base(pJsonData)
        {
            this.convertJsonToObject(pJsonData);
        }

        public override IEnumerable convertJsonToObject(string pJsonData)
        {
            //Template
            var _template = new {
                asks = (new List<double[]>() { }),
                bids = (new List<double[]>() { })
            };

            //Convert json to Object
            var _data = JsonConvert.DeserializeAnonymousType(pJsonData, _template);

            //Convert Array double to CoinData
            this.asks = (from _item in _data.asks select new DTOMBCoinData { Volume = _item[1], Value = _item[0] }).ToList();
            this.bids = (from _item in _data.bids select new DTOMBCoinData { Volume = _item[1], Value = _item[0] }).ToList();

            return base.convertJsonToObject(pJsonData);
        }
    }
}