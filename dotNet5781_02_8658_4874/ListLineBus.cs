using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    class ListLineBus : IEnumerable, IEnumerator

    {
        protected List<LineBus> Buses;//רשימה של קווי אוטובוסים
        public int Count { get; private set; }
        public IEnumerator GetEnumerator()
        { return this; }

        int index = -1;
        public object Current
        { get { return Buses[index]; } }

        public bool MoveNext()
        {
            index++;
            if (index >= Count)
            {
                index = -1;
                return false;
            }
            return true;
        }

        public void Reset()
        { index = -1; }

        public void addLineBus(LineBus busToAdd)
        {
            for (var i = 0; i < Buses.Count; i++)
            {
                if (busToAdd.BusLine1 == Buses[i].BusLine1)
                    return;//ייתכן שצריך לזרוק חריגה שאומרת שכבר קיים מספר קו כזה
            }
            Buses.Add(busToAdd);
        }
        public void delLineBus(LineBus busToDel)
        {
            bool flag = false;
            for (var i = 0; i < Buses.Count; i++)
            {
                if (busToDel.BusLine1 == Buses[i].BusLine1)
                    flag = true;
            }
            if (flag==true)
                Buses.Remove(busToDel);
        }
        public List<LineBus> ListOfLineThatPassStation (int CodeStation)//מחזירה את רשימת הקווים שעוברים בתחנה
        {
            for (var i = 0; i < Buses.Count; i++)
            {
                for (var j = 0; j < Buses[i].Stations.Count; j++)
                {

                }
            }
                List<LineBus> temp;
        }
    }
}
