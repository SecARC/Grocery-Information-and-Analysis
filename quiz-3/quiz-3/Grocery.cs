using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz_3
{
    public class Grocery
    {
        public string productName { get; set; }
        public string unit { get; set; }
        public string price { get; set; }

        public Grocery(string line)
        {
            string[] arr = line.Split('\n');
            if (arr.Length == 3)
            {
                productName = Convert.ToString(arr[0]);
                unit = Convert.ToString(arr[1]);
                price = Convert.ToString(arr[2]);
            }
            else
            {
                throw new FormatException("Line does not contains all grocery information");
            }
        }
    }
}
