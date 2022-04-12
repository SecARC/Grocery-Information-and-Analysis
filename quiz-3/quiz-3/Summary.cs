using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz_3
{
    public class Summary
    {
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public string minPrice { get; set; }
        public string maxPrice { get; set; }
        public string averagePrice { get; set; }
        public Summary(string line)
        {
            string[] arr = line.Split('\n');
            if (arr.Length == 5)
            {
                ProductName = Convert.ToString(arr[0]);
                Unit = Convert.ToString(arr[1]);
                minPrice = Convert.ToString(arr[2]);
                maxPrice = Convert.ToString(arr[2]);
                averagePrice = Convert.ToString(arr[2]);
            }
            else
            {
                throw new FormatException("Line does not contains all summary information");
            }
        }
    }
}
