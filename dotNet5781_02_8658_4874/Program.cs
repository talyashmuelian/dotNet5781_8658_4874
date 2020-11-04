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
                        
                        break;
                    case 4:
                        
                        break;
                }

            } while (ch < 5);
            
        }
    }
}
