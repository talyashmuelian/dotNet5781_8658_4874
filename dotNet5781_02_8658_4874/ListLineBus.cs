using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace dotNet5781_02_8658_4874
{ 
    class ListLineBus : IEnumerable

    {

        public List<LineBus> Buses=new List<LineBus>();//רשימה של קווי אוטובוסים
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
            var index = Buses.FindIndex((LineBus line) => { return line.BusLine1 == lineNumber; });
            if (index == -1)
            {
                //לזרוק חריגה אם האינדקס קטן מאפס
            }
            return index;

        }
        //public ListLineBus()
        //{

        //}
        public void add20LinesToSystem()//מתודה שמוסיפה 20 קווים אקראיים למערכת
        {
            LineBus s1 = new LineBus(); LineBus s2 = new LineBus(); LineBus s3 = new LineBus(); LineBus s4 = new LineBus();
            LineBus s5 = new LineBus(); LineBus s6 = new LineBus(); LineBus s7 = new LineBus(); LineBus s8 = new LineBus();
            LineBus s9 = new LineBus(); LineBus s10 = new LineBus();
            LineBus s11 = new LineBus(); LineBus s12 = new LineBus(); LineBus s13 = new LineBus(); LineBus s14 = new LineBus();
            LineBus s15 = new LineBus(); LineBus s16 = new LineBus(); LineBus s17 = new LineBus(); LineBus s18 = new LineBus();
            LineBus s19 = new LineBus(); LineBus s20 = new LineBus();
            s1.add4Stations(); s2.add4Stations(); s3.add4Stations(); s4.add4Stations(); s5.add4Stations(); s6.add4Stations();
            s7.add4Stations(); s8.add4Stations(); s9.add4Stations(); s10.add4Stations(); s11.add4Stations(); s12.add4Stations();
            s13.add4Stations(); s14.add4Stations(); s15.add4Stations(); s16.add4Stations(); s17.add4Stations(); s18.add4Stations();
            s19.add4Stations(); s20.add4Stations();
            Buses.Add(s1); Buses.Add(s2); Buses.Add(s3); Buses.Add(s4); Buses.Add(s5); Buses.Add(s6); Buses.Add(s7); Buses.Add(s8); Buses.Add(s9); Buses.Add(s10);
            Buses.Add(s11); Buses.Add(s12); Buses.Add(s13); Buses.Add(s14); Buses.Add(s15); Buses.Add(s16); Buses.Add(s17); Buses.Add(s18); Buses.Add(s19); Buses.Add(s20);
        }

        public int Count { get; private set; }
        public IEnumerator GetEnumerator()
        { return Buses.GetEnumerator(); }

        //int index = -1;
        //public object Current
        //{ get { return Buses[index]; } }

        //public bool MoveNext()
        //{
        //    index++;
        //    if (index >= Count)
        //    {
        //        index = -1;
        //        return false;
        //    }
        //    return true;
        //}

        //public void Reset()
        //{ index = -1; }

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
