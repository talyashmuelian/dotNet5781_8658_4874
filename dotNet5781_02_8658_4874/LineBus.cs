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
        static public Random rand = new Random(DateTime.Now.Millisecond);//הגרלת מספר קו
        public List<BusLineStation> Stations = new List<BusLineStation>();//מסלול הקו- רשימת התחנות שלו- יכול להיות שאסור לעשות ציבורי ואז צריך לחשוב איך לאפשר גישה לאוסף קוי אוטובוס
        protected static int BusLine;//מספר קו
        //protected BusLineStation FirstStation;//תחנת מוצא
        //protected BusLineStation LastStation;//תחנה סופית
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
        public void add4Stations ()//מתודה שמוסיפה 5 תחנות לקו. מתוכן 3 מוגרלות
        {
            BusLineStation s1 = new BusLineStation(); BusLineStation s2 = new BusLineStation(); BusLineStation s3 = new BusLineStation();
            BusLineStation s4 = new BusLineStation(1); BusLineStation s5 = new BusLineStation(2);
            Stations.Add(s1); Stations.Add(s2); Stations.Add(s3); Stations.Add(s4); Stations.Add(s5);
        }
        public LineBus()//בנאי
        {
            
            //Random rand = new Random(DateTime.Now.Millisecond);//הגרלת מספר קו
            int num = rand.Next(1,999);//ייתכן שצריך לבדוק שלא קיים קו כזה כבר ואם כן להוציא חריגה
            BusLine = num;
            //rand = new Random(DateTime.Now.Millisecond);//הגרלת מספר בין 1 ל5 כדי לבחור איזור בארץ
            num = rand.Next(1, 5);
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

        //public List<BusLineStation> BackRoute()
        //{
        //    List<BusLineStation> temp = new List<BusLineStation>(); ;
        //    for (var i = Stations.Count; i >=0 ; i--)
        //    {
        //        temp.Add(Stations[i]);

        //    }
        //    return temp;
        //}
        public override string ToString()
        {
            { return "The line of bus: " + BusLine1 + ", Area: " + Area1 + ", Stations-go" + Stations /*+ ", Stations-back" + BackRoute()*/; }//לוודא שהוא יכול לקבל מתודה ולא רק שדות
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
        public float getDistance(BusLineStation station1, BusLineStation station2)//מתודה שמחזירה את המרחק בין שתי תחנות (לאו דווקא סמוכות)
        {
            float temp = 0;
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
        public float getTimeOfDriving(BusLineStation station1, BusLineStation station2)//מתודה שמחזירה את זמן הנסיעה בין שתי תחנות (לאו דווקא סמוכות)
        {
            float temp = 0;
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
            return null;//ייתכן שצריך להוציא חריגה על כך שאחת או שתי התחנות לא נמצאו בקו זה
        }
        public float AllTheTime()//כל הזמן מתחילת המסלול עד סופו
        {
            float temp = 0;
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
