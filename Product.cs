using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace Xmas_tree_Json
{
    public class Product
    {
        [JsonProperty("full_name")]
        public string Name { get; set; }
        [JsonProperty("name_prefix")]
        public string Prefix { get; set; }
        [JsonProperty("prices")]
        public ProductPrices Prices { get; set; }

        //public string Description { get; set; }
        //public string Content { get; set; }
        [JsonProperty("html_url")]
        public string Url { get; set; }

        //public string urlToImage { get; set; }
    }
}
