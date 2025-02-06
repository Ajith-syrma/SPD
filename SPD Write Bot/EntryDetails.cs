using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPD_Write_Bot
{
    public class EntryDetails
    {
        public int RowNumber { get; set; }
        public string Expected { get; set; }
        public string TableValue { get; set; }
    }

    public class HexDetails
    {
        public int RowNumber { get; set; }
        public string Expected { get; set; }
        public string TableValue { get; set; }
    }

    public class genclass
    {
        public List<EntryDetails> entries { get; set; }
        public List<HexDetails> hexDetails { get; set; }
    }
}