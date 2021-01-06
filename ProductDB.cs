using System;
using System.Collections.Generic;
using System.Text;

namespace Xmas_tree_Json
{
    public class ProductDB : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Url { get; set; }
    }
}
