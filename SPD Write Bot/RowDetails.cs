using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPD_Write_Bot
{
    public class RowDetails
    {
        public Dictionary<int, (string column2Value, string column3Value)> ramvalue { get; set; }
        public Dictionary<int, (string column2Value, string column3Value)> hexvalue { get; set; }
    }
}
