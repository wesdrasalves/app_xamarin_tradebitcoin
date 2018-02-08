using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;
using System.Reflection;
using System.Globalization;
using Newtonsoft.Json;

namespace Dotend.MBTrade.DTO
{

    public class DTOMBOrder : DTOMB
    {
        public long order_id { get; set; }//Identificador da Ordem
        public string coin_pair { get; set; } //Tipo de moeda BRLBTC ou BRLLTC
        public MBEnumerables.OperationType order_type { get; set; }
        public int status { get; set; } //2-Open, 3- Canceled, 4-Filled 
        public bool has_fills { get; set; } //Se teve alguma execução 
        public double quantity { get; set; } //Quantidade da moeda digital
        public double limit_price { get; set; } //Preço da moeda digital
        public double executed_quantity { get; set; } //Quantidade da moeda digital executada
        public double executed_price_avg { get; set; } //Preço unitário medio de execução
        public double fee { get; set; } //Comissão da ordem 
        public string created_timestamp { get; set; } //Data de Criação
        public string updated_timestamp { get; set; } //Date de Atualização
        public List<DTOMBOperation> operations { get; set; } //Lista de Operações já executadas
    
        public DTOMBOrder() : base("") { }
        public DTOMBOrder(string pJsonData) : base(pJsonData)
        {

            foreach (DTOMBOrder _item in this.convertJsonToObject(pJsonData))
            {
                foreach (PropertyInfo _prop in typeof(DTOMBOrder).GetProperties())
                {
                    if (_prop.Name != "operations") _prop.SetValue(this, _prop.GetValue(_item));
                }
                return;
            }
        }

        public override IEnumerable convertJsonToObject(string pJsonData)
        {
            //Criando Objeto anonymo para receber dados
            var resp = new
            {
                response_data = new
                {
                    orders = new DTOMBOrder[] { }
                },
                status_code = "",
                server_unix_timestamp = ""
            };


            //
            var json = JsonConvert.DeserializeAnonymousType(pJsonData,resp);

            if (json.response_data != null && 
                json.response_data.orders != null && 
                json.response_data.orders.Length > 0)
            {

            foreach (DTOMBOrder _order in json.response_data.orders)
                yield return _order;

                //if (_data != null)
                //{

                //    if (((Dictionary<string, object>)_data).ContainsKey("status_code") &&
                //        Convert.ToString(((Dictionary<string, object>)_data)["status_code"]) == "100")
                //    {
                //        Dictionary<string, object> _response = (Dictionary<string, object>)((Dictionary<string, object>)_data)["response_data"];

                //        if (_response.ContainsKey("order"))
                //        {
                //            Dictionary<string, object> _dicOrder = ((Dictionary<string, object>)_response["order"]);
                //            yield return getCastObject(_dicOrder);
                //        }
                //        else if (_response.ContainsKey("orders"))
                //        {
                //            foreach (object _dicObject in (object[])_response["orders"])
                //            {
                //                Dictionary<string, object> _dicOrder = ((Dictionary<string, object>)_dicObject);
                //                yield return getCastObject(_dicOrder);
                //            }
                //        }
                //    }
                //}
            }

            base.convertJsonToObject(pJsonData);
        }
    }
}