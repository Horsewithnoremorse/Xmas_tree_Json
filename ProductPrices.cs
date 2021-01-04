using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace Xmas_tree_Json
{
    public class ProductPrices
    {
        [JsonProperty("price_min")]
        public Price Price_min { get; set; }
    }
}
