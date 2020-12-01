using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace dotNet5781_02_8658_4874
{ 
    public class ListLineBus : IEnumerable
    {
        public List<LineBus> Buses { get; set; } = new List<LineBus>();//רשימה של קווי אוטובוסים
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
            try
            {
                if (index == -1)
                {
                    throw new ObjectNotFoundException("Error: not found");//לזרוק חריגה אם האינדקס קטן מאפס
                }
            }
            catch (ObjectNotFoundException ex) { Console.WriteLine(ex.Message); }
            return index;


        }
        public void add10LinesToSystem()//מתודה שמוסיפה 10 קווים אקראיים למערכת
        {
            LineBus s1 = new LineBus(); LineBus s2 = new LineBus(); LineBus s3 = new LineBus(); LineBus s4 = new LineBus();
            LineBus s5 = new LineBus(); LineBus s6 = new LineBus(); LineBus s7 = new LineBus(); LineBus s8 = new LineBus();
            LineBus s9 = new LineBus(); LineBus s10 = new LineBus();
            s1.add10RandomStations(); s2.add10RandomStations(); s3.add10RandomStations(); s4.add10RandomStations(); s5.add10RandomStations(); s6.add10RandomStations();
            s7.add10RandomStations(); s8.add10RandomStations(); s9.add10RandomStations(); s10.add10RandomStations();
            Buses.Add(s1); Buses.Add(s2); Buses.Add(s3); Buses.Add(s4); Buses.Add(s5); Buses.Add(s6); Buses.Add(s7); Buses.Add(s8); Buses.Add(s9); Buses.Add(s10);
        }
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
            List<LineBus> temp = new List<LineBus>();
            bool flag = false;
            for (var i = 0; i < Buses.Count; i++)
            {
                for (var j = 0; j < Buses[i].Stations.Count; j++)
                {
                    if (Buses[i].Stations[j].BusStationKey_p == CodeStation)//אם נמצאה התחנה בקו הזה
                    {
                        temp.Add(Buses[i]);//נוסיף את הקו לרשימת הקווים שעוברים בתחנה
                        flag = true;
                    }
                      
                }
            }
            try
            {
                if (flag==false)
                    throw new ObjectNotFoundException("Error: The requested station was not found");
                if (temp.Count == 0)
                    throw new ListEmptyException("There are no lines passing through the station");
            }

            catch (ListEmptyException ex) { Console.WriteLine(ex.Message); return null; }
            catch (ObjectNotFoundException ex) { Console.WriteLine(ex.Message); }
            return temp;//צריך לבדוק אם הרשימה ריקה ואם כן צריך להוציא חריגה
        }
        public List<LineBus> getSorted()
        {
            List<LineBus> temp= new List<LineBus>();//יצירת רשימת עזר 
            temp = Buses;//הכנסת הרשימה שלנו לתוך רשימת העזר
            temp.Sort();//מיון רשימת העזר
            return temp;//החזרת רשימת העזר הממוינת
        }
        public List<BusLineStation> AllStationInSystem()//מחזירה רשימה של כל התחנות במערכת ללא כפילויות
        {
            List<BusLineStation> temp111 = new List<BusLineStation>();//רשימה שאליה ייכנסו כל התחנות במערכת ללא כפילויות
            temp111.Add(Buses[0].Stations[0]);
            for (var i = 0; i < Buses.Count; i++)
            {
                for (int j = 0; j < Buses[i].Stations.Count; j++)//נעבור על כל התחנות בקו
                {
                    bool flag = false;//דגל שהוא אמת אם התחנה כבר נמצאת ברשימה
                    for (int k = 0; k < temp111.Count; k++)//נעבור על הרשימת עזר שלנו לבדוק אם התחנה כבר הוכנסה אליה
                    {
                        if (Buses[i].Stations[j].BusStationKey_p == temp111[k].BusStationKey_p)//התחנה עבר קיימת ברשימת העזר
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)//התחנה לא קיימת ברשימת העזר ולכן צריך להכניס אותה
                        temp111.Add(Buses[i].Stations[j]);//נכניס אותה לרשימת העזר שמכילה את כל התחנות במערכת ללא כפילויות
                }
            }
            return temp111;
        }
        public bool IfStationInSystem(int numStation)
        {
            List<BusLineStation> temp123 = new List<BusLineStation>();
            temp123=AllStationInSystem();
            for (int i=0;i< temp123.Count;i++)
            {
                if (numStation == temp123[i].BusStationKey_p)
                    return true;
            }
            return false;
        }
    }
}
