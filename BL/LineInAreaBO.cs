using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineInAreaBO
    {
        public string Key { get; set; }//איזור
        public IEnumerable<BusLineBO> ListOfLinesInArea { get; set; }//רשימת הקווים של האיזור

    }
}
