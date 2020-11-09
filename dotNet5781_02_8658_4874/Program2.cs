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
    
    class Program2
    {
        static public Random rand = new Random();//הגרלת מספר קו
        static void Main(string[] args)
        {
            

            ListLineBus list1 = new ListLineBus();
            list1.add20LinesToSystem();//זימון מתודה שמתאחלת את המערכת ב20 קווים ולכל אחד מהם 13 תחנות
            int ch=0;
            do
            {
                try
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
                    if (ch>5)
                        throw new FormatException("The number you entered is incorrect");
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
                                Console.WriteLine("Done successfully");
                            }
                            if (numChoose == 2)//רוצה להוסיף תחנה לקו מסוים
                            {
                                Console.WriteLine("Enter the line number you want to add a station to ");
                                tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                                int numChoose3 = int.Parse(tempStr);//קליטת מספר הקו
                                int indexOfWantedBus = -1;
                                for (int i = 0; i < list1.Buses.Count; i++)//נחפש את הקו שאליו המשתמש ביקש להוסיף תחנה
                                {
                                    if (list1.Buses[i].BusLine1 == numChoose3)
                                    {
                                        indexOfWantedBus = i;
                                        break;
                                    }
                                }
                                if (indexOfWantedBus < 0)//הקו לא נמצא
                                {
                                    throw new ObjectNotFoundException("Error: Line does not exist");
                                }
                                Console.WriteLine("Enter the station number you want to add to the line ");
                                string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה Absorption from the user of his choice
                                int numChoose1 = int.Parse(tempStr1);//קליטת מספר התחנה
                                List<BusLineStation> temp111 = new List<BusLineStation>();//רשימה שאליה ייכנסו כל התחנות במערכת ללא כפילויות
                                temp111 = list1.AllStationInSystem();//זימון מתודה שמחזירה לתוך המשתנה את רשימת כל התחנות שקיימות כעת במערכת
                                bool flag1 = false;//דגל שיהפוך לאמת אם התחנה אכן תימצא בתחנות הקיימות במערכת
                                for (int i = 0; i < temp111.Count; i++)//בדיקה שהתחנה המבוקשת אכן נמצאת בתחנות המערכת בכלל
                                {
                                    if (temp111[i].BusStationKey_p == numChoose1)
                                    {
                                        flag1 = true;
                                        break;
                                    }
                                }
                                if (flag1 == false)//התחנה לא קיימת במערכת
                                {
                                    throw new ObjectNotFoundException("Error: The station does not exist in the system");
                                }

                                try
                                {
                                    for (int j = 0; j < list1.Buses[indexOfWantedBus].Stations.Count; j++)//נעבור על כל התחנות בקו
                                    {
                                        if (list1.Buses[indexOfWantedBus].Stations[j].BusStationKey_p == numChoose1)//התחנה כבר קיימת בקו ואין אפשרות להוסיף אותה עקב כך
                                        {
                                            throw new AnObjectAlreadyExistsException("Error: The station already exists on the line");//להוציא חריגה שהתחנה כבר קיימת
                                        }
                                    }
                                }
                                catch (AnObjectAlreadyExistsException ex) { Console.WriteLine(ex.Message); break; }
                                BusLineStation StationToAdd = new BusLineStation(numChoose1);
                                list1.Buses[indexOfWantedBus].addStation(StationToAdd);//הוספת התחנה לקו המבוקש
                                Console.WriteLine("Done successfully");
                            }
                            if (numChoose !=2 && numChoose!=1)
                            {
                                throw new FormatException("The number you entered is incorrect. Insert 1 or 2");
                            }

                            break;
                        case 2://מחיקת תחנה או קו אוטובוס
                            Console.WriteLine("Enter 1 for delete line OR 2 to delete station");//
                            tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                            numChoose = int.Parse(tempStr);

                            if (numChoose == 1)//רוצה למחוק קו
                            {
                                Console.WriteLine("Enter the line number you want to delete");
                                tempStr = Console.ReadLine();//קליטה מהמשתמש את מספר הקו Absorption from the user of his choice
                                int numChoose2 = int.Parse(tempStr);
                                bool flag = false;//דגל שיהיה אמת אם הקו המבוקש נמצא ויהיה שקר אם הוא לא נמצא
                                for (var i = 0; i < list1.Buses.Count; i++)
                                {
                                    if (list1.Buses[i].BusLine1 == numChoose2)//נמצא הקו שרוצים למחוק
                                    {
                                        list1.delLineBus(list1.Buses[i]);//נזמן עבורו את פונקציית המחיקה
                                        Console.WriteLine("Done successfully");
                                        flag = true;
                                    }
                                }
                                if (flag==false)
                                    throw new ObjectNotFoundException("Error: The line does not exist");//להוציא חריגה שלא נמצא הקו המבוקש למחיקה
                            }
                            if (numChoose == 2)//רוצה למחוק תחנה מקו מסוים
                            {
                                Console.WriteLine("Enter the line number from which you want to delete a station");
                                tempStr = Console.ReadLine();//קליטה מהמשתמש את מספר הקו Absorption from the user of his choice
                                int numChoose2 = int.Parse(tempStr);
                                int indexOfWantedBus = -1;
                                for (int i = 0; i < list1.Buses.Count; i++)//נחפש את הקו שממנו אנחנו רוצים למחוק תחנה
                                {
                                    if (list1.Buses[i].BusLine1 == numChoose2)
                                    {
                                        indexOfWantedBus = i;
                                        break;
                                    }
                                }
                                if (indexOfWantedBus < 0)//אם הקו לא קיים
                                {
                                    throw new ObjectNotFoundException("Error: Line does not exist");
                                }
                                Console.WriteLine("Enter the line number station to delete");
                                string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה Absorption from the user of his choice
                                int numChoose1 = int.Parse(tempStr1);
                                bool flag1 = false;//דגל שיהיה אמת אם התחנה אכן נמצאה בקו ויהיה שקר אם היא לא נמצאה
                                for (var i = 0; i < list1.Buses.Count; i++)
                                {
                                    if (list1.Buses[i].BusLine1 == numChoose2)//נמצא הקו שרוצים למחוק ממנו
                                    {
                                        for (int j = 0; j < list1.Buses[i].Stations.Count; j++)//נעבור על כל התחנות בקו
                                        {
                                            if (list1.Buses[i].Stations[j].BusStationKey_p == numChoose1)//מצאנו את התחנה
                                            {
                                                list1.Buses[i].delStation(list1.Buses[i].Stations[j]);//נזמן את פונקציית מחיקת התחנה
                                                flag1 = true;
                                                Console.WriteLine("Done successfully");
                                            }
                                            
                                        }
                                        if (flag1==false)//התחנה לא נמצאה בקו המבוקש
                                            throw new ObjectNotFoundException("Error: The station was not on the requested line");
                                    }
                                }

                            }
                            if (numChoose != 2 && numChoose != 1)
                            {
                                throw new FormatException("The number you entered is incorrect. Insert 1 or 2");
                            }

                            break;
                        case 3:
                            Console.WriteLine("Enter 1 for print all the lines that pass through the station according to the station number OR 2 to print travel options between two stations");
                            tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                            numChoose = int.Parse(tempStr);
                            if (numChoose == 1)//הדפסת כל הקווים שעוברים בתחנה ע"פ מספר תחנה
                            {
                                Console.WriteLine("Enter the number station");
                                string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה Absorption from the user of his choice
                                int numChoose1 = int.Parse(tempStr1);
                                List<LineBus> temp = new List<LineBus>();
                                temp = list1.ListOfLineThatPassStation(numChoose1);
                                Console.WriteLine("The bus lines that pass through the station:");
                                for (int i = 0; i < temp.Count; i++)
                                {
                                    Console.WriteLine(temp[i].BusLine1);
                                }

                            }
                            if (numChoose == 2)//הדפסת אפשרויות הנסיעה בין שתי תחנות, ממוינות לפי אורך הנסיעה, מהקצר לארוך
                            {
                                Console.WriteLine("Enter the line number station1");
                                string tempStr1 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה הראשונה
                                int numChoose1 = int.Parse(tempStr1);
                                if (list1.IfStationInSystem(numChoose1)== false)//בדיקה האם התחנה אכן קיימת במערכת
                                    throw new ObjectNotFoundException("Error: The requested station was not found");
                                Console.WriteLine("Enter the line number station2");
                                string tempStr2 = Console.ReadLine();//קליטה מהמשתמש את מספר התחנה השנייה
                                int numChoose2 = int.Parse(tempStr2);
                                if (list1.IfStationInSystem(numChoose2) == false)//בדיקה האם התחנה אכן קיימת במערכת
                                    throw new ObjectNotFoundException("Error: The requested station was not found");
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
                            if (numChoose != 2 && numChoose != 1)
                            {
                                throw new FormatException("The number you entered is incorrect. Insert 1 or 2");
                            }


                            break;
                        case 4:
                            Console.WriteLine("Enter 1 for print all bus lines in the system OR 2 to print a list of all the stations and line numbers that pass through them");
                            tempStr = Console.ReadLine();//קליטה מהמשתמש את בחירתו Absorption from the user of his choice
                            numChoose = int.Parse(tempStr);
                            if (numChoose == 1)//הדפסת כל קווי האוטובוס במערכת
                            {
                                for (var i = 0; i < list1.Buses.Count; i++)
                                {
                                    Console.WriteLine(list1.Buses[i]);
                                }
                            }
                            if (numChoose == 2)//הדפסת רשימת כל התחנות ומספרי הקווים שעוברים דרכם
                            {
                                List<BusLineStation> temp111 = new List<BusLineStation>();//רשימה שאליה ייכנסו כל התחנות במערכת ללא כפילויות
                                temp111 = list1.AllStationInSystem();//זימון מתודה שמחזירה לתוך המשתנה את רשימת כל התחנות שקיימות כעת במערכת
                                for (int i = 0; i < temp111.Count; i++)//הדפסת רשימת כל התחנות במערכת והקווים שעוברים בהן
                                {
                                    Console.WriteLine("Station details: " + temp111[i]);//הדפסת התחנה

                                    List<LineBus> listOfLinesInSpecificStation = new List<LineBus>();//יצירת רשימה שאליה ייכנסו כל הקווים שעוברים בתחנה הספציפית
                                    listOfLinesInSpecificStation = list1.ListOfLineThatPassStation(temp111[i].BusStationKey_p);//זימון פונקציה שמחזירה את רשימת הקווים העוברים בתחנה
                                                                                                                               //לתפוס את החריגה שנזרקה במקרה שלא היו קווים שעוברים בתחנה
                                    Console.WriteLine("The bus lines that pass through the station:");
                                    for (int j = 0; j < listOfLinesInSpecificStation.Count; j++)//הדפסת רשימת כל הקווים העוברים בתחנה
                                    {
                                        Console.WriteLine(listOfLinesInSpecificStation[j].BusLine1);
                                    }
                                }
                            }
                            if (numChoose != 2 && numChoose != 1)
                            {
                                throw new FormatException("The number you entered is incorrect Insert 1 or 2");
                            }
                            break;
                    }
                }
                catch (FormatException ex) { Console.WriteLine(ex.Message); }
                catch (ObjectNotFoundException ex) { Console.WriteLine(ex.Message); }
                
            } while (ch != 5);
            Console.WriteLine("exit");
        }
    }
}
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//4
//Enter 1 for print all bus lines in the system OR 2 to print a list of all the stations and line numbers that pass through them
//2
//Station details: Bus Station Code: 150665, Latitude: 31.90277, Longitude: 35.0961
//The bus lines that pass through the station:
//51
//Station details: Bus Station Code: 38046, Latitude: 32.50485, Longitude: 34.44748
//The bus lines that pass through the station:
//51
//Station details: Bus Station Code: 237081, Latitude: 32.03075, Longitude: 34.63725
//The bus lines that pass through the station:
//51
//Station details: Bus Station Code: 1, Latitude: 32, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 2, Latitude: 33, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 3, Latitude: 31, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 4, Latitude: 33, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 5, Latitude: 31, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 6, Latitude: 33, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 7, Latitude: 33, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 8, Latitude: 31, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 9, Latitude: 32, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 10, Latitude: 31, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//949
//Station details: Bus Station Code: 984487, Latitude: 33.12173, Longitude: 34.70838
//The bus lines that pass through the station:
//454
//Station details: Bus Station Code: 225472, Latitude: 32.55397, Longitude: 35.22892
//The bus lines that pass through the station:
//454
//Station details: Bus Station Code: 375441, Latitude: 32.40823, Longitude: 34.60338
//The bus lines that pass through the station:
//454
//Station details: Bus Station Code: 4354, Latitude: 31.58525, Longitude: 34.73729
//The bus lines that pass through the station:
//173
//Station details: Bus Station Code: 549957, Latitude: 31.08106, Longitude: 34.86857
//The bus lines that pass through the station:
//173
//Station details: Bus Station Code: 221450, Latitude: 32.0017, Longitude: 35.17201
//The bus lines that pass through the station:
//173
//Station details: Bus Station Code: 733436, Latitude: 31.84302, Longitude: 34.44845
//The bus lines that pass through the station:
//79
//Station details: Bus Station Code: 856638, Latitude: 32.7145, Longitude: 35.1673
//The bus lines that pass through the station:
//79
//Station details: Bus Station Code: 60196, Latitude: 32.61319, Longitude: 34.35629
//The bus lines that pass through the station:
//79
//Station details: Bus Station Code: 14552, Latitude: 31.52829, Longitude: 34.35251
//The bus lines that pass through the station:
//324
//Station details: Bus Station Code: 744455, Latitude: 31.61908, Longitude: 35.35927
//The bus lines that pass through the station:
//324
//Station details: Bus Station Code: 768022, Latitude: 31.5377, Longitude: 35.05478
//The bus lines that pass through the station:
//324
//Station details: Bus Station Code: 441671, Latitude: 32.30964, Longitude: 34.92971
//The bus lines that pass through the station:
//453
//Station details: Bus Station Code: 529791, Latitude: 31.23092, Longitude: 34.99791
//The bus lines that pass through the station:
//453
//Station details: Bus Station Code: 837361, Latitude: 32.53524, Longitude: 34.54513
//The bus lines that pass through the station:
//453
//Station details: Bus Station Code: 151365, Latitude: 31.10103, Longitude: 35.1815
//The bus lines that pass through the station:
//768
//Station details: Bus Station Code: 423408, Latitude: 33.2267, Longitude: 35.43189
//The bus lines that pass through the station:
//768
//Station details: Bus Station Code: 582575, Latitude: 31.30107, Longitude: 35.05908
//The bus lines that pass through the station:
//768
//Station details: Bus Station Code: 956773, Latitude: 31.92876, Longitude: 35.30025
//The bus lines that pass through the station:
//774
//Station details: Bus Station Code: 883816, Latitude: 32.71688, Longitude: 34.63807
//The bus lines that pass through the station:
//774
//Station details: Bus Station Code: 764204, Latitude: 33.13577, Longitude: 34.48048
//The bus lines that pass through the station:
//774
//Station details: Bus Station Code: 848409, Latitude: 31.39652, Longitude: 34.811
//The bus lines that pass through the station:
//980
//Station details: Bus Station Code: 765138, Latitude: 32.78159, Longitude: 34.98075
//The bus lines that pass through the station:
//980
//Station details: Bus Station Code: 155904, Latitude: 31.891, Longitude: 34.62819
//The bus lines that pass through the station:
//980
//Station details: Bus Station Code: 827245, Latitude: 32.75418, Longitude: 35.36312
//The bus lines that pass through the station:
//251
//Station details: Bus Station Code: 647388, Latitude: 31.81518, Longitude: 34.72608
//The bus lines that pass through the station:
//251
//Station details: Bus Station Code: 463595, Latitude: 31.43417, Longitude: 34.65601
//The bus lines that pass through the station:
//251
//Station details: Bus Station Code: 797907, Latitude: 32.04226, Longitude: 35.389
//The bus lines that pass through the station:
//272
//Station details: Bus Station Code: 817139, Latitude: 31.12466, Longitude: 34.85633
//The bus lines that pass through the station:
//272
//Station details: Bus Station Code: 783474, Latitude: 32.49142, Longitude: 35.49676
//The bus lines that pass through the station:
//272
//Station details: Bus Station Code: 11153, Latitude: 32.73188, Longitude: 34.84593
//The bus lines that pass through the station:
//13
//Station details: Bus Station Code: 681875, Latitude: 31.04824, Longitude: 35.49831
//The bus lines that pass through the station:
//13
//Station details: Bus Station Code: 265456, Latitude: 32.48024, Longitude: 35.10221
//The bus lines that pass through the station:
//13
//Station details: Bus Station Code: 270568, Latitude: 32.51574, Longitude: 34.82358
//The bus lines that pass through the station:
//914
//Station details: Bus Station Code: 919017, Latitude: 33.22538, Longitude: 35.05177
//The bus lines that pass through the station:
//914
//Station details: Bus Station Code: 356839, Latitude: 32.39917, Longitude: 35.34446
//The bus lines that pass through the station:
//914
//Station details: Bus Station Code: 533293, Latitude: 31.20559, Longitude: 35.26926
//The bus lines that pass through the station:
//706
//Station details: Bus Station Code: 944156, Latitude: 33.05655, Longitude: 35.05262
//The bus lines that pass through the station:
//706
//Station details: Bus Station Code: 163069, Latitude: 32.4844, Longitude: 35.38951
//The bus lines that pass through the station:
//706
//Station details: Bus Station Code: 552024, Latitude: 31.31094, Longitude: 35.42823
//The bus lines that pass through the station:
//370
//Station details: Bus Station Code: 416433, Latitude: 31.18783, Longitude: 35.46839
//The bus lines that pass through the station:
//370
//Station details: Bus Station Code: 121577, Latitude: 31.6122, Longitude: 34.74679
//The bus lines that pass through the station:
//370
//Station details: Bus Station Code: 427281, Latitude: 31.08018, Longitude: 35.4322
//The bus lines that pass through the station:
//837
//Station details: Bus Station Code: 946486, Latitude: 33.18614, Longitude: 35.01379
//The bus lines that pass through the station:
//837
//Station details: Bus Station Code: 826527, Latitude: 31.75213, Longitude: 34.86348
//The bus lines that pass through the station:
//837
//Station details: Bus Station Code: 156536, Latitude: 32.44344, Longitude: 34.83992
//The bus lines that pass through the station:
//283
//Station details: Bus Station Code: 452698, Latitude: 31.34943, Longitude: 35.38205
//The bus lines that pass through the station:
//283
//Station details: Bus Station Code: 991195, Latitude: 32.74483, Longitude: 35.11196
//The bus lines that pass through the station:
//283
//Station details: Bus Station Code: 905337, Latitude: 31.18371, Longitude: 35.17076
//The bus lines that pass through the station:
//610
//Station details: Bus Station Code: 275530, Latitude: 33.06581, Longitude: 35.00345
//The bus lines that pass through the station:
//610
//Station details: Bus Station Code: 899747, Latitude: 33.27667, Longitude: 35.4448
//The bus lines that pass through the station:
//610
//Station details: Bus Station Code: 911165, Latitude: 32.63803, Longitude: 35.46612
//The bus lines that pass through the station:
//222
//Station details: Bus Station Code: 708884, Latitude: 32.73753, Longitude: 34.95391
//The bus lines that pass through the station:
//222
//Station details: Bus Station Code: 263414, Latitude: 31.34106, Longitude: 34.59856
//The bus lines that pass through the station:
//222
//Station details: Bus Station Code: 476184, Latitude: 31.33601, Longitude: 34.60001
//The bus lines that pass through the station:
//949
//Station details: Bus Station Code: 789512, Latitude: 31.68908, Longitude: 34.7624
//The bus lines that pass through the station:
//949
//Station details: Bus Station Code: 650757, Latitude: 31.96205, Longitude: 34.61353
//The bus lines that pass through the station:
//949
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//1
//Enter 1 for new line OR 2 to new station
//3
//The number you entered is incorrect.Insert 1 or 2
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//1
//Enter 1 for new line OR 2 to new station
//1
//Done successfully
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//1
//Enter 1 for new line OR 2 to new station
//2
//Enter the line number you want to add a station to
//949
//Enter the station number you want to add to the line
//263414
//Enter 1 if you want to add to the begin, 2 to add to the middle, 3 to the end:
//1
//Done successfully
//choose 1-5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//2
//Enter 1 for delete line OR 2 to delete station
//1
//Enter the line number you want to delete
//12
//Error: The line does not exist
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//2
//Enter 1 for delete line OR 2 to delete station
//1
//Enter the line number you want to delete
//949
//Done successfully
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//2
//Enter 1 for delete line OR 2 to delete station
//2
//Enter the line number from which you want to delete a station
//211165
//Error: Line does not exist
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//2
//Enter 1 for delete line OR 2 to delete station
//2
//Enter the line number from which you want to delete a station
//222
//Enter the line number station to delete
//263414
//Done successfully
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//3
//Enter 1 for print all the lines that pass through the station according to the station number OR 2 to print travel options between two stations
//1
//Enter the number station
//4
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//3
//Enter 1 for print all the lines that pass through the station according to the station number OR 2 to print travel options between two stations
//2
//Enter the line number station1
//3
//Enter the line number station2
//9
//List of lines that make the desired route:
//706
//774
//454
//610
//222
//272
//837
//173
//453
//283
//251
//79
//768
//13
//980
//51
//914
//370
//324
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//4
//Enter 1 for print all bus lines in the system OR 2 to print a list of all the stations and line numbers that pass through them
//1
//The line of bus: 51, Area: General, Stations - go: 150665=>38046=>237081=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 454, Area: General, Stations - go: 984487=>225472=>375441=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 173, Area: North, Stations - go: 4354=>549957=>221450=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 79, Area: North, Stations - go: 733436=>856638=>60196=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 324, Area: Center, Stations - go: 14552=>744455=>768022=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 453, Area: North, Stations - go: 441671=>529791=>837361=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 768, Area: General, Stations - go: 151365=>423408=>582575=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 774, Area: General, Stations - go: 956773=>883816=>764204=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 980, Area: Center, Stations - go: 848409=>765138=>155904=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 251, Area: North, Stations - go: 827245=>647388=>463595=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 272, Area: General, Stations - go: 797907=>817139=>783474=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 13, Area: Center, Stations - go: 11153=>681875=>265456=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 914, Area: Center, Stations - go: 270568=>919017=>356839=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 706, Area: General, Stations - go: 533293=>944156=>163069=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 370, Area: South, Stations - go: 552024=>416433=>121577=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 837, Area: Center, Stations - go: 427281=>946486=>826527=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 283, Area: Center, Stations - go: 156536=>452698=>991195=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 610, Area: North, Stations - go: 905337=>275530=>899747=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 222, Area: North, Stations - go: 911165=>708884=>1=>2=>3=>4=>5=>6=>7=>8=>9=>10=>
//The line of bus: 897, Area: North, Stations - go:
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//4
//Enter 1 for print all bus lines in the system OR 2 to print a list of all the stations and line numbers that pass through them
//2
//Station details: Bus Station Code: 150665, Latitude: 31.90277, Longitude: 35.0961
//The bus lines that pass through the station:
//51
//Station details: Bus Station Code: 38046, Latitude: 32.50485, Longitude: 34.44748
//The bus lines that pass through the station:
//51
//Station details: Bus Station Code: 237081, Latitude: 32.03075, Longitude: 34.63725
//The bus lines that pass through the station:
//51
//Station details: Bus Station Code: 1, Latitude: 32, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 2, Latitude: 33, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 3, Latitude: 31, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 4, Latitude: 33, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 5, Latitude: 31, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 6, Latitude: 33, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 7, Latitude: 33, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 8, Latitude: 31, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 9, Latitude: 32, Longitude: 34
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 10, Latitude: 31, Longitude: 35
//The bus lines that pass through the station:
//51
//454
//173
//79
//324
//453
//768
//774
//980
//251
//272
//13
//914
//706
//370
//837
//283
//610
//222
//Station details: Bus Station Code: 984487, Latitude: 33.12173, Longitude: 34.70838
//The bus lines that pass through the station:
//454
//Station details: Bus Station Code: 225472, Latitude: 32.55397, Longitude: 35.22892
//The bus lines that pass through the station:
//454
//Station details: Bus Station Code: 375441, Latitude: 32.40823, Longitude: 34.60338
//The bus lines that pass through the station:
//454
//Station details: Bus Station Code: 4354, Latitude: 31.58525, Longitude: 34.73729
//The bus lines that pass through the station:
//173
//Station details: Bus Station Code: 549957, Latitude: 31.08106, Longitude: 34.86857
//The bus lines that pass through the station:
//173
//Station details: Bus Station Code: 221450, Latitude: 32.0017, Longitude: 35.17201
//The bus lines that pass through the station:
//173
//Station details: Bus Station Code: 733436, Latitude: 31.84302, Longitude: 34.44845
//The bus lines that pass through the station:
//79
//Station details: Bus Station Code: 856638, Latitude: 32.7145, Longitude: 35.1673
//The bus lines that pass through the station:
//79
//Station details: Bus Station Code: 60196, Latitude: 32.61319, Longitude: 34.35629
//The bus lines that pass through the station:
//79
//Station details: Bus Station Code: 14552, Latitude: 31.52829, Longitude: 34.35251
//The bus lines that pass through the station:
//324
//Station details: Bus Station Code: 744455, Latitude: 31.61908, Longitude: 35.35927
//The bus lines that pass through the station:
//324
//Station details: Bus Station Code: 768022, Latitude: 31.5377, Longitude: 35.05478
//The bus lines that pass through the station:
//324
//Station details: Bus Station Code: 441671, Latitude: 32.30964, Longitude: 34.92971
//The bus lines that pass through the station:
//453
//Station details: Bus Station Code: 529791, Latitude: 31.23092, Longitude: 34.99791
//The bus lines that pass through the station:
//453
//Station details: Bus Station Code: 837361, Latitude: 32.53524, Longitude: 34.54513
//The bus lines that pass through the station:
//453
//Station details: Bus Station Code: 151365, Latitude: 31.10103, Longitude: 35.1815
//The bus lines that pass through the station:
//768
//Station details: Bus Station Code: 423408, Latitude: 33.2267, Longitude: 35.43189
//The bus lines that pass through the station:
//768
//Station details: Bus Station Code: 582575, Latitude: 31.30107, Longitude: 35.05908
//The bus lines that pass through the station:
//768
//Station details: Bus Station Code: 956773, Latitude: 31.92876, Longitude: 35.30025
//The bus lines that pass through the station:
//774
//Station details: Bus Station Code: 883816, Latitude: 32.71688, Longitude: 34.63807
//The bus lines that pass through the station:
//774
//Station details: Bus Station Code: 764204, Latitude: 33.13577, Longitude: 34.48048
//The bus lines that pass through the station:
//774
//Station details: Bus Station Code: 848409, Latitude: 31.39652, Longitude: 34.811
//The bus lines that pass through the station:
//980
//Station details: Bus Station Code: 765138, Latitude: 32.78159, Longitude: 34.98075
//The bus lines that pass through the station:
//980
//Station details: Bus Station Code: 155904, Latitude: 31.891, Longitude: 34.62819
//The bus lines that pass through the station:
//980
//Station details: Bus Station Code: 827245, Latitude: 32.75418, Longitude: 35.36312
//The bus lines that pass through the station:
//251
//Station details: Bus Station Code: 647388, Latitude: 31.81518, Longitude: 34.72608
//The bus lines that pass through the station:
//251
//Station details: Bus Station Code: 463595, Latitude: 31.43417, Longitude: 34.65601
//The bus lines that pass through the station:
//251
//Station details: Bus Station Code: 797907, Latitude: 32.04226, Longitude: 35.389
//The bus lines that pass through the station:
//272
//Station details: Bus Station Code: 817139, Latitude: 31.12466, Longitude: 34.85633
//The bus lines that pass through the station:
//272
//Station details: Bus Station Code: 783474, Latitude: 32.49142, Longitude: 35.49676
//The bus lines that pass through the station:
//272
//Station details: Bus Station Code: 11153, Latitude: 32.73188, Longitude: 34.84593
//The bus lines that pass through the station:
//13
//Station details: Bus Station Code: 681875, Latitude: 31.04824, Longitude: 35.49831
//The bus lines that pass through the station:
//13
//Station details: Bus Station Code: 265456, Latitude: 32.48024, Longitude: 35.10221
//The bus lines that pass through the station:
//13
//Station details: Bus Station Code: 270568, Latitude: 32.51574, Longitude: 34.82358
//The bus lines that pass through the station:
//914
//Station details: Bus Station Code: 919017, Latitude: 33.22538, Longitude: 35.05177
//The bus lines that pass through the station:
//914
//Station details: Bus Station Code: 356839, Latitude: 32.39917, Longitude: 35.34446
//The bus lines that pass through the station:
//914
//Station details: Bus Station Code: 533293, Latitude: 31.20559, Longitude: 35.26926
//The bus lines that pass through the station:
//706
//Station details: Bus Station Code: 944156, Latitude: 33.05655, Longitude: 35.05262
//The bus lines that pass through the station:
//706
//Station details: Bus Station Code: 163069, Latitude: 32.4844, Longitude: 35.38951
//The bus lines that pass through the station:
//706
//Station details: Bus Station Code: 552024, Latitude: 31.31094, Longitude: 35.42823
//The bus lines that pass through the station:
//370
//Station details: Bus Station Code: 416433, Latitude: 31.18783, Longitude: 35.46839
//The bus lines that pass through the station:
//370
//Station details: Bus Station Code: 121577, Latitude: 31.6122, Longitude: 34.74679
//The bus lines that pass through the station:
//370
//Station details: Bus Station Code: 427281, Latitude: 31.08018, Longitude: 35.4322
//The bus lines that pass through the station:
//837
//Station details: Bus Station Code: 946486, Latitude: 33.18614, Longitude: 35.01379
//The bus lines that pass through the station:
//837
//Station details: Bus Station Code: 826527, Latitude: 31.75213, Longitude: 34.86348
//The bus lines that pass through the station:
//837
//Station details: Bus Station Code: 156536, Latitude: 32.44344, Longitude: 34.83992
//The bus lines that pass through the station:
//283
//Station details: Bus Station Code: 452698, Latitude: 31.34943, Longitude: 35.38205
//The bus lines that pass through the station:
//283
//Station details: Bus Station Code: 991195, Latitude: 32.74483, Longitude: 35.11196
//The bus lines that pass through the station:
//283
//Station details: Bus Station Code: 905337, Latitude: 31.18371, Longitude: 35.17076
//The bus lines that pass through the station:
//610
//Station details: Bus Station Code: 275530, Latitude: 33.06581, Longitude: 35.00345
//The bus lines that pass through the station:
//610
//Station details: Bus Station Code: 899747, Latitude: 33.27667, Longitude: 35.4448
//The bus lines that pass through the station:
//610
//Station details: Bus Station Code: 911165, Latitude: 32.63803, Longitude: 35.46612
//The bus lines that pass through the station:
//222
//Station details: Bus Station Code: 708884, Latitude: 32.73753, Longitude: 34.95391
//The bus lines that pass through the station:
//222
//choose 1 - 5
//1: Insert a new station or new line
//2: Delete station or line
//3: Find lines or routes
//4: Print all the lines or all the stations
//5: Exit
//5
//exit
//Press any key to continue . . .
