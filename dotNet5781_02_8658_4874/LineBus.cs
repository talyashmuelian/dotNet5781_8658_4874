using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    enum MyEnum { General = 1, North, South, Center, Jerusalem }
    class LineBus : IComparable

    {
        public List<BusLineStation> Stations = new List<BusLineStation>();//מסלול הקו- רשימת התחנות שלו- יכול להיות שאסור לעשות ציבורי ואז צריך לחשוב איך לאפשר גישה לאוסף קוי אוטובוס
        protected int BusLine;//מספר קו
        protected string Area;//איזור בארץ
        public BusLineStation FirstStation
        {
            set { Stations[0] = value; }
            get { return Stations[0]; }
        }
        public BusLineStation LastStation
        {
            set { Stations[Stations.Count] = value; }
            get { return Stations[Stations.Count]; }
        }
        public void add4Stations ()//מתודה שמוסיפה 13 תחנות לקו. מתוכן 3 מוגרלות
        {
            BusLineStation s1 = new BusLineStation(); BusLineStation s2 = new BusLineStation(); BusLineStation s3 = new BusLineStation();
            BusLineStation s4 = new BusLineStation(1); BusLineStation s5 = new BusLineStation(2); BusLineStation s6 = new BusLineStation(3);
            BusLineStation s7 = new BusLineStation(4); BusLineStation s8 = new BusLineStation(5); BusLineStation s9 = new BusLineStation(6);
            BusLineStation s10 = new BusLineStation(7); BusLineStation s11 = new BusLineStation(8); BusLineStation s12 = new BusLineStation(9);
            BusLineStation s13 = new BusLineStation(10);
            Stations.Add(s1); Stations.Add(s2); Stations.Add(s3); Stations.Add(s4); Stations.Add(s5);
            Stations.Add(s6); Stations.Add(s7); Stations.Add(s8); Stations.Add(s9); Stations.Add(s10);
            Stations.Add(s11); Stations.Add(s12); Stations.Add(s13);
        }
        public LineBus()//בנאי
        {
            int num = Program2.rand.Next(1,999);//ייתכן שצריך לבדוק שלא קיים קו כזה כבר ואם כן להוציא חריגה
            BusLine = num;
            num = Program2.rand.Next(1, 5);
            switch (num)
            {
                case 1:
                    Area = "General";
                    break;
                case 2:
                    Area = "North";
                    break;
                case 3:
                    Area = "South";
                    break;
                case 4:
                    Area = "Center";
                    break;
                case 5:
                    Area = "Jerusalem";
                    break;
            }
        }


        public int BusLine1 { get => BusLine; set => BusLine = value; }
        public string Area1 { get => Area; set => Area = value; }
        public string  stringStation ()//שרשור כל התחנות למחרוזת על מנת שנוכל להציג אותה בטוסטרינג
        {
            string str=null;
            for (int i=0;i<Stations.Count;i++)
            {
                str=str + Stations[i].BusStationKey_p;
                str = str + "=>";
            }
            return str;
        }
        public override string ToString()
        {
            { return "The line of bus: " + BusLine1 + ", Area: " + Area1 + ", Stations-go: "+ stringStation(); }
        }
        public void addStation(BusLineStation toAdd)//הוספת תחנה למסלול הקו
        {
            Console.WriteLine("Enter 1 if you want to add to the begin, 2 to add to the middle, 3 to the end:");
            string tempStr = Console.ReadLine();//קליטת בחירת המשתמש
            int choose = int.Parse(tempStr);
            if (choose == 1)
            {
                Stations.Add(toAdd);
            }
            if (choose == 2)
            {
                Console.WriteLine("insert the index that you want to add after him:");
                tempStr = Console.ReadLine();//קליטת בחירת המשתמש
                choose = int.Parse(tempStr);
                Stations.Insert(choose, toAdd);//מחיקה לפי האינדקס שהתקבל
            }
            if (choose == 3)
            {
                Stations.Insert(0, toAdd);
            }
            if(choose != 1&& choose !=2 && choose != 3)
                throw new FormatException("The number you entered is incorrect. enter 1 or 2 or 3");
        }
        public void delStation(BusLineStation toDel)//מחיקת תחנה ממסלול הקו
        {
            Stations.Remove(toDel);
        }
        public bool IfStationHere(BusLineStation toCheck)//מתודה בוליאנית שבודקת האם תחנה מסוימת נמצאת במסלול הקו
        {
            for (var i = 0; i < Stations.Count; i++)//עוברת על כל התחנות במסלול ובודקת אם מספר התחנה זהה
            {
                if (Stations[i].BusStationKey_p == toCheck.BusStationKey_p)
                {
                    return true;
                }
            }
            return false;
        }
        public double getDistance(BusLineStation station1, BusLineStation station2)//מתודה שמחזירה את המרחק בין שתי תחנות (לאו דווקא סמוכות)
        {
            double temp = 0;
            for (var i = 0; i < Stations.Count; i++)//עוברת על כל התחנות במסלול ובודקת אם מספר התחנה זהה
            {
                if ((Stations[i].BusStationKey_p == station1.BusStationKey_p) || (Stations[i].BusStationKey_p == station2.BusStationKey_p))//אם הגעת לאחת התחנות
                {
                    if (Stations[i].BusStationKey_p == station1.BusStationKey_p)//אם הגעת לתחנה הראשונה
                    {
                        for (var j = i + 1; j < Stations.Count; j++)//מעבר המשך על כל הרשימה עד לתחנה השנייה וחיבור של כל המרחקים
                        {
                            temp += Stations[j].Distance_p;
                            if (Stations[i].BusStationKey_p == station2.BusStationKey_p)//ברגע שהגענו לתחנה השנייה נצא מהלולאה
                                break;
                        }
                        return temp;//החזרת כל המרחקים
                    }
                    if (Stations[i].BusStationKey_p == station1.BusStationKey_p)//אם הגעת לתחנה השנייה
                    {
                        for (var j = i + 1; j < Stations.Count; j++)//מעבר המשך על כל הרשימה עד לתחנה הראשונה וחיבור של כל המרחקים
                        {
                            temp += Stations[j].Distance_p;
                            if (Stations[i].BusStationKey_p == station2.BusStationKey_p)//ברגע שהגענו לתחנה הראשונה נצא מהלולאה
                                break;
                        }
                        return temp;//החזרת כל המרחקים
                    }
                }
            }
            return 0;//אולי צריך להוציא חריגה שלא נמצאו התחנות או אחת מהן
        }
        public double getTimeOfDriving(BusLineStation station1, BusLineStation station2)//מתודה שמחזירה את זמן הנסיעה בין שתי תחנות (לאו דווקא סמוכות)
        {
            double temp = 0;
            for (var i = 0; i < Stations.Count; i++)//עוברת על כל התחנות במסלול ובודקת אם מספר התחנה זהה
            {
                if ((Stations[i].BusStationKey_p == station1.BusStationKey_p) || (Stations[i].BusStationKey_p == station2.BusStationKey_p))//אם הגעת לאחת התחנות
                {
                    if (Stations[i].BusStationKey_p == station1.BusStationKey_p)//אם הגעת לתחנה הראשונה
                    {
                        for (var j = i + 1; j < Stations.Count; j++)//מעבר המשך על כל הרשימה עד לתחנה השנייה וחיבור של כל הזמנים
                        {
                            temp += Stations[j].TimeOfDriving_p;
                            if (Stations[i].BusStationKey_p == station2.BusStationKey_p)//ברגע שהגענו לתחנה השנייה נצא מהלולאה
                                break;
                        }
                        return temp;//החזרת כל הזמנים
                    }
                    if (Stations[i].BusStationKey_p == station1.BusStationKey_p)//אם הגעת לתחנה השנייה
                    {
                        for (var j = i + 1; j < Stations.Count; j++)//מעבר המשך על כל הרשימה עד לתחנה הראשונה וחיבור של כל הזמנים
                        {
                            temp += Stations[j].TimeOfDriving_p;
                            if (Stations[i].BusStationKey_p == station2.BusStationKey_p)//ברגע שהגענו לתחנה הראשונה נצא מהלולאה
                                break;
                        }
                        return temp;//החזרת כל הזמנים
                    }
                }
            }
            return 0;//אולי צריך להוציא חריגה שלא נמצאו התחנות או אחת מהן
        }
        public bool IsStationHere(BusLineStation station)//בודקת אם תחנה כלשהי נמצאת בקו הזה
        {
            for (var i = 0; i < Stations.Count; i++)
            {
                if (Stations[i].BusStationKey_p == station.BusStationKey_p)
                    return true;
            }
            return false;
        }
        public LineBus tatRoute(BusLineStation station1, BusLineStation station2)//מתודה שמחזירה תת מסלול מתוך קו אוטובוס
        {
            if (IsStationHere(station1) && IsStationHere(station2))//בדיקה ששתי התחנות אכן קיימות בקו
            {
                LineBus temp = new LineBus();
                temp.BusLine1 = BusLine1;
                temp.Area = Area;
                int num1 = 0;//משתנה לצורך שמירת האינדקס של התחנה הראשונה
                int num2 = 0;//משתנה לצורך שמירת האינדקס של התחנה השנייה
                for (var i = 0; i < Stations.Count; i++)
                {
                    if (Stations[i].BusStationKey_p == station1.BusStationKey_p)
                        num1 = i;
                    if (Stations[i].BusStationKey_p == station2.BusStationKey_p)
                        num2 = i;
                }
                for (int j = num1; j <= num2; j++)//מכניסה את התחנות שבאמצע לרשימה במופע העזר של תת המסלול
                {
                    temp.Stations.Add(Stations[j]);
                }
                return temp;

            }
            return null;
        }
        public double AllTheTime()//כל הזמן מתחילת המסלול עד סופו
        {
            double temp = 0;
            for (var i = 1; i < Stations.Count; i++)
            {
                temp += Stations[i].TimeOfDriving_p;
            }
            return temp;//החזרת כל הזמנים
        }
        public int CompareTo(object obj)
        {
            LineBus s = (LineBus)obj;
            return AllTheTime().CompareTo(s.AllTheTime());
        }

    }
}
