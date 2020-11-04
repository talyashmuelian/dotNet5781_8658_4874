using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace dotNet5781_02_8658_4874
{ 
    class ListLineBus : IEnumerable, IEnumerator

    {

        public List<LineBus> Buses;//רשימה של קווי אוטובוסים
        public LineBus this[int lineNumber]
        {
            get
            {
                int index1 = FindIndex(lineNumber);
                return Buses[index1];
            }
            set
            {
                int index1 = FindIndex(lineNumber);
                Buses[index1] = value;
            }
        }
        private int FindIndex(int lineNumber)
        {
            int index = Buses.FindIndex((LineBus line) => { return line.BusLine1 == lineNumber; });
            if (index==-1)
            {
                //לזרוק חריגה אם האינדקס קטן מאפס
            }
            return index;

        }
        public ListLineBus()//בנאי שיוצר אוטמטית 10 קווי אוטובוס
        {
            LineBus s1 = new LineBus(); LineBus s2 = new LineBus(); LineBus s3 = new LineBus(); LineBus s4 = new LineBus();
            LineBus s5 = new LineBus(); LineBus s6 = new LineBus(); LineBus s7 = new LineBus(); LineBus s8 = new LineBus();
            LineBus s9 = new LineBus(); LineBus s10 = new LineBus();
            Buses.Add(s1); Buses.Add(s2); Buses.Add(s3); Buses.Add(s4); Buses.Add(s5); Buses.Add(s6); Buses.Add(s7); Buses.Add(s8); Buses.Add(s9); Buses.Add(s10);
        }

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
        public void addStationToSpesificLine(LineBus busToAddHim)//מוסיפה תחנה לקו ספציפי. צריך לטפל בה.
        {
            for (var i = 0; i < Buses.Count; i++)
            {
                if (busToAddHim.BusLine1== Buses[i].BusLine1)
                {
                    //Buses[i].addStation
                }
            }
        }
        public List<LineBus> ListOfLineThatPassStation (int CodeStation)//מחזירה את רשימת הקווים שעוברים בתחנה

        {
            List<LineBus> temp = new List<LineBus>();
            for (var i = 0; i < Buses.Count; i++)
            {
                for (var j = 0; j < Buses[i].Stations.Count; j++)
                {
                    if (Buses[i].Stations[j].BusStationKey_p == CodeStation)//אם נמצאה התחנה בקו הזה
                        temp.Add(Buses[i]);//נוסיף את הקו לרשימת הקווים שעוברים בתחנה
                }
            }
            return temp;//צריך לבדוק אם הרשימה ריקה ואם כן צריך להוציא חריגה
        }
        public List<LineBus> getSorted()
        {
            List<LineBus> temp= new List<LineBus>();//יצירת רשימת עזר 
            temp = Buses;//הכנסת הרשימה שלנו לתוך רשימת העזר
            temp.Sort();//מיון רשימת העזר
            return temp;//החזרת רשימת העזר הממוינת
        }
    }
}
