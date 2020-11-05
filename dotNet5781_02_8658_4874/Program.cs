//neria farangi 211344874
//talya shmuelian 211378658
//donNet exe 2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_8658_4874
{
    class Program
    {
        static void Main(string[] args)
        {
            ListLineBus list1= new ListLineBus();
            int ch;
            do
            {
                Console.WriteLine("choose 1-5");
                Console.WriteLine("1: Insert a new station or new line");//הוספת תחנה או קו אוטובוס
                Console.WriteLine("2: Delete station or line");//מחיקת תחנה או קו אוטובוס
                Console.WriteLine("3: Find lines or routes");//חיפוש קווים שעוברים בתחנה או אפשרויות לנסיעה בין שתי תחנות
                Console.WriteLine("4: Print all the lines or all the stations");//הדפסת כל קווי האוטובוס או כל התחנות ומספרי הקווים שעוברים דרכם
                Console.WriteLine("5: Exit");
                string tempStr;
                int numChoose;
                string chStr = Console.ReadLine();
                ch = int.Parse(chStr);
                switch (ch)
                {
                    case 1://הוספת תחנה או קו אוטובוס
                        Console.WriteLine("Enter 1 for new line OR 2 to new station");//
                        tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                        numChoose = int.Parse(tempStr);
                        //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                        if (numChoose == 1)//רוצה להוסיף קו חדש
                        {
                            LineBus new1 = new LineBus();
                            list1.addLineBus(new1);
                            //ייתכן שצריך כאן לתפוס את החריגה של מצב שהקו קיים כבר
                        }
                        if (numChoose == 2)//רוצה להוסיף תחנה לקו מסוים
                        {
                            Console.WriteLine("Enter the line number you want to add a station to ");
                            tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                            numChoose = int.Parse(tempStr);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם

                        }
                        else
                        {
                            //להוציא חריגה אם המשתמש הכניס כל מספר אחר
                        }

                        break;
                    case 2://מחיקת תחנה או קו אוטובוס
                        Console.WriteLine("Enter 1 for delete line OR 2 to delete station");//
                        tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                        numChoose = int.Parse(tempStr);
                        //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                        if (numChoose == 1)//רוצה למחוק קו
                        {
                            Console.WriteLine("Enter the line number you want to delete");
                            tempStr = Console.ReadLine();//קליטה מהמשתמש את מספר הקו Absorption from the user of his choice
                            numChoose = int.Parse(tempStr);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                            for (var i = 0; i < list1.Buses.Count; i++)
                            {
                                if (list1.Buses[i].BusLine1 == numChoose)//נמצא הקו שרוצים למחוק
                                    list1.delLineBus(list1.Buses[i]);//נזמן עבורו את פונקציית המחיקה
                                else
                                {
                                    //להוציא חריגה שלא נמצא הקו המבוקש למחיקה
                                }
                            }  
                        }
                        if (numChoose == 2)//רוצה למחוק תחנה מקו מסוים
                        {
                            Console.WriteLine("Enter the line number from which you want to delete a station");
                            tempStr = Console.ReadLine();//קליטה מהמשתמש את מספר הקו Absorption from the user of his choice
                            numChoose = int.Parse(tempStr);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                            Console.WriteLine("Enter the line number station to delete");
                            string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה Absorption from the user of his choice
                            int numChoose1 = int.Parse(tempStr1);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                            for (var i = 0; i < list1.Buses.Count; i++)
                            {
                                if (list1.Buses[i].BusLine1 == numChoose)//נמצא הקו שרוצים למחוק ממנו
                                {
                                    for (int j=0;j< list1.Buses[i].Stations.Count;j++)//נעבור על כל התחנות בקו
                                    {
                                        if (list1.Buses[i].Stations[j].BusStationKey_p == numChoose1)//מצאנו את התחנה
                                            list1.Buses[i].delStation(list1.Buses[i].Stations[j]);//נזמן את פונקציית מחיקת התחנה
                                        else
                                        {
                                            //לזרוק חריגה שלא נמצאה התחנה המבוקשת
                                        }
                                    }
                                }
                                else
                                {
                                    //להוציא חריגה שלא נמצא הקו המבוקש למחיקה
                                }
                            }

                        }
                        else
                        {
                            //בכל מקרה שהמשתמש הכניס מספר אחר להוציא חריגה
                        }

                        break;
                    case 3:
                        Console.WriteLine("Enter 1 for print all the lines that pass through the station according to the station number OR 2 to print travel options between two stations");
                        tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                        numChoose = int.Parse(tempStr);
                        //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                        if (numChoose==1)//הדפסת כל הקווים שעוברים בתחנה ע"פ מספר תחנה
                        {
                            Console.WriteLine("Enter the line number station");
                            string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה Absorption from the user of his choice
                            int numChoose1 = int.Parse(tempStr1);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                            List<LineBus> temp = new List<LineBus>();
                            temp = list1.ListOfLineThatPassStation(numChoose1);
                            //לתפוס את החריגה שנזרקה במקרה שלא היו קווים שעוברים בתחנה
                            Console.WriteLine("The bus lines that pass through the station:");
                            for (int i=0;i< temp.Count;i++)
                            {
                                Console.WriteLine(temp[i]);
                            }
                        }
                        if (numChoose == 2)//הדפסת אפשרויות הנסיעה בין שתי תחנות, ממוינות
                        {
                            Console.WriteLine("Enter the line number station1");
                            string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה הראשונה
                            int numChoose1 = int.Parse(tempStr1);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                            Console.WriteLine("Enter the line number station2");
                            string tempStr2 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה השנייה
                            int numChoose2 = int.Parse(tempStr2);
                            //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                            LineBus inTatRoute = new LineBus();//משתנה שישמור את תת המסלול
                            ListLineBus temp = new ListLineBus();//רשימה הקווים שעושים את הדרך בין שתי התחנות המבוקשות
                            BusLineStation station11 = new BusLineStation(numChoose1);//יצירת תחנה ראשונה עם מספר התחנה המתקבלת
                            BusLineStation station22 = new BusLineStation(numChoose2);//יצירת תחנה שנייה עם מספר התחנה המתקבלת
                            for (var i = 0; i < list1.Buses.Count; i++)
                            {
                                inTatRoute = list1.Buses[i].tatRoute(station11, station22);
                                if (inTatRoute != null)//הקו אכן נוסע בין שתי תחנות אלו
                                {
                                    temp.addLineBus(inTatRoute);//הוספת הקו לתוך האוסף של קווי האוטובוס העוברים בתחנה
                                }
                            }
                            temp.Buses.Sort();//מיון לפי זמן הנסיעה
                            Console.WriteLine("List of lines that make the desired route:");
                            for (var i = 0; i < temp.Buses.Count; i++)
                            {
                                Console.WriteLine(temp.Buses[i].BusLine1);//הדפסת מספרי הקווים שעושים את הדרך הזאת
                            }
                        }
                        else
                        {
                            //לזרוק חריגה בכל מקרה של הכנסת מספר לא תקין
                        }


                            break;
                    case 4:
                        Console.WriteLine("Enter 1 for print all bus lines in the system OR 2 to print a list of all the stations and line numbers that pass through them");
                        tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                        numChoose = int.Parse(tempStr);
                        //לתפוס חריגה למקרה שהשתמש לא יכניס שלם
                        if (numChoose == 1)//הדפסת כל קווי האוטובוס במערכת
                        {
                            for (var i = 0; i < list1.Buses.Count; i++)
                            {
                                Console.WriteLine(list1.Buses[i].BusLine1);
                            }
                        }
                        if (numChoose == 2)//הדפסת רשימת כל התחנות ומספרי הקווים שעוברים דרכם
                        {
                            List<BusLineStation> temp111=new List<BusLineStation>();//רשימה שאליה ייכנסו כל התחנות במערכת ללא כפילויות
                            for (var i = 0; i < list1.Buses.Count; i++)
                            {
                                for (int j = 0; j < list1.Buses[i].Stations.Count; j++)//נעבור על כל התחנות בקו
                                {
                                    for (int k=0;k < temp111.Count; k++)//נעבור על הרשימת עזר שלנו לבדוק אם התחנה כבר הוכנסה אליה
                                        if (list1.Buses[i].Stations[j].BusStationKey_p == temp111[k].BusStationKey_p)//התחנה עבר קיימת ברשימת העזר
                                        {
                                            break;
                                        }
                                    else//התחנה לא קיימת ברשימת העזר ולכן צריך להכניס אותה
                                        {
                                            temp111.Add(list1.Buses[i].Stations[j]);//נכניס אותה לרשימת העזר שמכילה את כל התחנות במערכת ללא כפילויות
                                        }
                                }
                            }
                            for (int i=0;i<temp111.Count;i++)//הדפסת רשימת כל התחנות במערכת והקווים שעוברים בהן
                            {
                                Console.WriteLine("Number station"+ temp111[i].BusStationKey_p);//הדפסת מספר התחנה

                                List<LineBus> listOfLinesInSpecificStation = new List<LineBus>();//יצירת רשימה שאליה ייכנסו כל הקווים שעוברים בתחנה הספציפית
                                listOfLinesInSpecificStation = list1.ListOfLineThatPassStation(temp111[i].BusStationKey_p);//זימון פונקציה שמחזירה את רשימת הקווים העוברים בתחנה
                                //לתפוס את החריגה שנזרקה במקרה שלא היו קווים שעוברים בתחנה
                                Console.WriteLine("The bus lines that pass through the station:");
                                for (int j = 0; i < listOfLinesInSpecificStation.Count; j++)//הדפסת רשימת כל הקווים העוברים בתחנה
                                {
                                    Console.WriteLine(listOfLinesInSpecificStation[j]);
                                }
                            }
                        }
                        else
                        {
                            //לזרוק חריגה בכל מקרה של הכנסת מספר לא תקין
                        }
                        break;
                }

            } while (ch < 5);
            
        }
    }
}
