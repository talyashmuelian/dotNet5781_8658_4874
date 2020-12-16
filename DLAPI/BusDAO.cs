﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusDAO
    {
        public int License { get; set; }//מספר רישוי
        public DateTime StartOfWork { get; set; }//תאריך רישוי
        public int TotalKms { get; set; }//קילומטרז'
        public int Fuel { get; set; }//מיכל דלק
        public Status Status { get; set; }

        public override string ToString()
        {
            return this.ToStringProperty();
        }
        //....
    }
}
