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
                        Random rand = new Random(DateTime.Now.Millisecond);
                        int num = rand.Next(1100);

                        //Random r = new Random();//הגרלת מספר רנדומלי לנסיעה Lottery random number for travel
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
    class Bus
    {
		private int yearStart;//השנה שבה האוטובוס נכנס לפעילות The year the bus went into operation
		private int numOfBus;//מספר רישוי license number
		private DateTime dateOfStart;//תאריך תחילת פעילות Activity start date
		private DateTime dateTreatLast;//תאריך טיפול אחרון Last treatment date
		private int kilometersFromTreament;//מספר הקילומטרים מאז הטיפול האחרון Number of miles since last treatment
		private int kilometersUntilLastTreatment;//נסועה בה בוצע הטיפול האחרון A ride in which the last treatment was performed
		private int kilometraj;//נסועה כוללת Total travel
		private int kilometers;//כמות הקילומטרים מאז התדלוק The amount of miles since refueling
		public Bus(int num,DateTime myDate)//c-tor
        {
			numOfBus = num;
			dateOfStart = myDate;
			kilometraj = 0;
			kilometers = 0;
			kilometersFromTreament = 0;
			kilometersUntilLastTreatment = 0;
			dateTreatLast = DateTime.Now;
			yearStart = 0;

		}
		public void setYearStart(int num) { yearStart = num; }
		public int getNumOfBus() { return numOfBus; }
		public void setNumOfBus(int num) { numOfBus = num; }
		public double getKilometers() { return kilometers; }
		public void setKilometers (int num) { kilometers = num; }
		public void treatment()
        {
			dateTreatLast = DateTime.Now;
			kilometersFromTreament = 0;
			kilometersUntilLastTreatment = kilometraj;
		}
		public void print()//הצגת הנסועה מאז הטיפול האחרון Presentation of the passenger since the last treatment
		{
			int start, middle, end;
			if (yearStart>2017)//8 ספרות numbers
            {
				start= numOfBus / 100000;
				middle = (numOfBus % 100000) / 1000;
				end = numOfBus % 1000;
				Console.WriteLine("Registration Number:" + start + "-" + middle + "-" + end+ "Km since last treatment:"+kilometersFromTreament) ;
			}
			else//7 ספרות numbers
            {
				start = numOfBus / 100000;
				middle = (numOfBus % 100000) / 100;
				end = numOfBus % 100;
				Console.WriteLine("Registration Number:" + start + "-" + middle + "-" + end+"Km since last treatment:" + kilometersFromTreament);
			}
			
		}
        public bool checkIfReady(int kilimeterForTravel)//מתודה שבודקת אם אוטבוס יכול לצאת לנסיעה מסוימת A method that checks if a bus can go on a particular trip
		{
			DateTime date1 = DateTime.Now;
			TimeSpan t = date1 - dateTreatLast;
			int space=Convert.ToInt32(t.TotalDays);//casting to int
			if (space > 365)//עברה שנה מאז הטיפול האחרון It has been a year since the last treatment
				return false;
			int sum = kilimeterForTravel + kilometersFromTreament;//סכום הקילומטרים שהיו מאז הטיפול האחרון פלוס הנסיעה שהוגרלה The amount of miles that have been since the last treatment plus the raffle ride
			if (sum>20000)//הנסיעה ארוכה מידי מבחינת קילומטרז' שנשאר לטיפול The journey is too long in terms of mileage left for treatment
				return false;
			sum = kilometers + kilimeterForTravel;// סכום הקילומטרים שהיו מאז התדלוק האחרון פלוס הנסיעה שהוגרלה The amount of miles that have been since the last refueling plus the raffle ride

			if (sum>1200)//הנסיעה ארוכה מידי מבחינת דלק The trip is too long in terms of fuel
				return false;
            return true;
        }
		public void doingDriving(int kilimeterForTravel)
        {
			kilometersFromTreament+= kilimeterForTravel;//עדכון מספר הקילומטרים מאז הטיפול האחרון Update the number of miles since the last treatment
			kilometraj += kilimeterForTravel;//עדכון נסועה כוללת Overall travel update
			kilometers += kilimeterForTravel;//עדכון כמות הקילומטרים מאז התדלוק Update the amount of miles since refueling
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
