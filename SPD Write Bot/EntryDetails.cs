using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPD_Write_Bot
{
    public class EntryDetails
    {
        public int bytsEntry { get; set; }
        public string ExpectedValue { get; set; }
        public string OriginalValue { get; set; }
    }

    public class HexDetails
    {
        public int bytshexvalue { get; set; }
        public string ExpectedValuehex { get; set; }
        public string hexoriginalvalues { get; set; }
    }

    public class genclass
    {
        public List<EntryDetails> entries { get; set; }
        public List<HexDetails> hexDetails { get; set; }
    }
}