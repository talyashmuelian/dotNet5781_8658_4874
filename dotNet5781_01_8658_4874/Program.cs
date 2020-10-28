//neria farangi 211344874
//talya shmuelian 211378658
//donNet exe 1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8658_4874
{
    class Program
    {
        static public Random rand = new Random();

        static void Main(string[] args)
        {
            
            List<Bus> listBuses = new List<Bus>();
            int ch;
            do
            {
                Console.WriteLine("choose 1-5");
                Console.WriteLine("1: Insert a new bus");
                Console.WriteLine("2: Select a bus to drive");
                Console.WriteLine("3: Refuel or send to treatment");
                Console.WriteLine("4: Print all buses last treatment Kilometrage");
                Console.WriteLine("5: Exit");
                string chStr = Console.ReadLine();
                ch = int.Parse(chStr);
                switch (ch)
                {
                    case 1:
                        Console.WriteLine("Enter registration-number:");//הודעה למשתמש להכניס מספר רישוי Notify user to enter license number
                        string tempStr = Console.ReadLine();//קליטת מספר רישוי מהמשתמש Receiving a license number from the user
                        int numBus = int.Parse(tempStr);
                        Console.WriteLine("You need to enter the date today. Enter day:");//הודעה למשתמש להכניס תאריך Notice to user to enter a date
                        tempStr = Console.ReadLine();//קליטת יום מהמשתמש Absorption of a day from the user
                        int day = int.Parse(tempStr);
                        Console.WriteLine("Enter month:");//הודעה למשתמש להכניס חודש Notice to user to enter a month
                        tempStr = Console.ReadLine();//קליטת חודש מהמשתמש Absorption of a month from the user
                        int month = int.Parse(tempStr);
                        Console.WriteLine("Enter year:");//הודעה למשתמש להכניס שנה Notify user to enter year
                        tempStr = Console.ReadLine();//קליטת שנה מהמשתמש Absorption of one year from the user
                        int year = int.Parse(tempStr);
                        DateTime date = new DateTime(year, month, day);
                        Bus lineBus;
                        lineBus = new Bus(numBus, date);//יצירת מופע מסוג אוטובוס Creating a bus-type show
                        listBuses.Add(lineBus); //הוספת האוטובוס לרשימה Add the bus to the list
                        lineBus.setYearStart(year);

                        break;
                    case 2:
                        Console.WriteLine("Enter registration-number:");//הודעה למשתמש להכניס מספר רישוי Notify user to enter license number
                        tempStr = Console.ReadLine();//קליטת מספר רישוי מהמשתמש Receiving a license number from the user
                        numBus = int.Parse(tempStr);
                        Random rand = new Random(DateTime.Now.Millisecond);//הגרלת מספר רנדומלי לנסיעה Lottery random number for travel
                        int num = rand.Next(1100);//costing to int
                        bool flag = false;
                        for (var i = 0; i < listBuses.Count; i++)//מעבר על כל הרשימה לחיפוש האוטובוס המבוקש Go through the entire list to search for the requested bus
                        {
                            if (listBuses[i].getNumOfBus() == numBus)
                            {
                                //For a method time that will check whether the bus that is available can travel in terms of fuel and mileage, or whether a year has passed since the last treatment
                                if (listBuses[i].checkIfReady(num) == true)//לזמן מתודה שתבדוק האם האוטובוס הנמצא יכול לצאת לנסיעה מבחינת דלק וקילומטרז, או שעברה שנה מאז הטיפול האחרון'
                                {
                                    listBuses[i].doingDriving(num);//לזמן מתודה שמוסיפה את הקילומטרים של נסיעה זו For a method time that adds the miles of this trip
                                }
                                else
                                    Console.WriteLine("The bus cannot doing the driving");//אם הוא לא יכול לצאת לנסיעה If he can not go on a trip
                                flag = true;
                            }
                        }
                        if (flag == false)
                            Console.WriteLine("registration-number not found");//האוטובוס לא נמצא The bus was not found
                        break;
                    case 3:
                        Console.WriteLine("Enter registration-number:");// Notify user to enter license numberהודעה למשתמש להכניס מספר רישוי
                        tempStr = Console.ReadLine();//קליטת מספר רישוי מהמשתמש Receiving a license number from the user
                        numBus = int.Parse(tempStr);
                        flag = false;
                        for (var i = 0; i < listBuses.Count; i++)//מעבר על כל הרשימה לחיפוש האוטובוס המבוקש Go through the entire list to search for the requested bus
                        {
                            if (listBuses[i].getNumOfBus() == numBus)
                            {
                                Console.WriteLine("Enter 1 for refuel OR 2 to treatment");//הודעה למשתמש אם הוא רוצה לתדלק או לטפל Notify the user if he wants to refuel or take care
                                tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                                int numChoose = int.Parse(tempStr);
                                if (numChoose == 1)//אם המשתמש רוצה לתדלק If the user wants to refuel
                                {
                                    listBuses[i].setKilometers(0);//איפוס השדה שסופר את כמות הקילומטרים מאז התדלוק Reset the field that counts the number of miles since refueling
                                }
                                else//אם המשתמש רוצה לטפל If the user wants to take care of
                                {
                                    listBuses[i].treatment();//זימון מתודה שמטפלת Summoning a method that does therapy
                                }
                                flag = true;
                            }
                        }
                        if (flag == false)
                            Console.WriteLine("registration-number not found");//האוטובוס לא נמצא The bus was not found

                        break;
                    case 4:
                        for (var i = 0; i < listBuses.Count; i++)//מעבר על כל הרשימה לחיפוש האוטובוס המבוקש Go through the entire list to search for the requested bus
                        {
                            listBuses[i].print();
                        }
                        break;
                }

            } while (ch < 5);
            Console.WriteLine("Exit");
        }
    }
    
}
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//1
//Enter registration-number:
//1234567
//You need to enter the date today. Enter day:
//1
//Enter month:
//2
//Enter year:
//2000
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//2
//Enter registration-number:
//1234567
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//1
//Enter registration-number:
//12345678
//You need to enter the date today. Enter day:
//3
//Enter month:
//4
//Enter year:
//2018
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//1
//Enter registration-number:
//7777777
//You need to enter the date today. Enter day:
//23
//Enter month:
//4
//Enter year:
//2015
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//2
//Enter registration-number:
//7777777
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//2
//Enter registration-number:
//1234567
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//2
//Enter registration-number:
//1234567
//The bus cannot doing the driving
//choose 1-5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//3
//Enter registration-number:
//1234567
//Enter 1 for refuel OR 2 to treatment
//1
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//2
//Enter registration - number:
//8888888
//registration - number not found
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//4
//Registration Number:12 - 345 - 67Km since last treatment:1143
//Registration Number:123 - 45 - 678Km since last treatment:0
//Registration Number:77 - 777 - 77Km since last treatment:671
//choose 1 - 5
//1: Insert a new bus
//2: Select a bus to drive
//3: Refuel or send to treatment
//4: Print all buses last treatment Kilometrage
//5: Exit
//5
//Exit
