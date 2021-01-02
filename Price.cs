using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;


namespace Xmas_tree_Json
{
    public class Price
    {
        public string amount { get; set; }

        public double amountD { get; set; }

        public double MyConvertToDouble()
        {
            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };
            double amountDouble = double.Parse(amount, formatter);
            return amountDouble;
        }
    }   
}
