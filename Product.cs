using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Xmas_tree_Json
{
    public class Product
    {
        public string full_name { get; set; }
        public string name_prefix { get; set; }
        public ProductPrices prices { get; set; }

        //public string Description { get; set; }
        //public string Content { get; set; }
        //public string Url { get; set; }

        //public string urlToImage { get; set; }
    }
}
