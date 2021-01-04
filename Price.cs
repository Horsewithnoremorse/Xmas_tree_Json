using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Xmas_tree_Json
{
    public class Price
    {
        [JsonProperty("amount")]
        public string CashInString { get; set; }

        public double CashInDouble { get; set; }

        public double MyConvertToDouble()
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            double amountDouble = double.Parse(CashInString, formatter);
            return amountDouble;
        }
    }   
}
