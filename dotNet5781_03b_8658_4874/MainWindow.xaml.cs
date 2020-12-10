//neria farangi 211344874
//talya shmuelian 211378658
//donNet exe 3b
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

using dotNet5781_01_8658_4874;

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker workerFuel;
        BackgroundWorker workerTravel;

        static public Random rand1 = new Random();
        public static ObservableCollection<Bus> collectionBuses = new ObservableCollection<Bus>();
        public MainWindow()
        {
            InitializeComponent();
            Bus bus1 = new Bus(rand1.Next(1000000,9999999), new DateTime(2017, 9, 1));
            Bus bus2 = new Bus(rand1.Next(10000000,99999999), DateTime.Now);
            Bus bus3 = new Bus(rand1.Next(10000000, 99999999), new DateTime(2018, 12, 2));
            Bus bus4 = new Bus(rand1.Next(1000000, 9999999), new DateTime(2016, 12, 2)); 
            Bus bus5 = new Bus(rand1.Next(10000000, 99999999), new DateTime(2019, 12, 2));
            Bus bus6 = new Bus(rand1.Next(1000000, 9999999), new DateTime(2017, 10, 1));
            Bus bus7 = new Bus(rand1.Next(10000000, 99999999), new DateTime(2018, 10, 2)); 
            Bus bus8 = new Bus(rand1.Next(1000000, 9999999), new DateTime(2012, 3, 11)); 
            Bus bus9 = new Bus(rand1.Next(10000000, 99999999), new DateTime(2019, 10, 2)); 
            Bus bus10 = new Bus(rand1.Next(1000000, 9999999), new DateTime(2005, 11, 1));
            bus1.kilometers1 = 1000;//אתחול אוטובוס כך שיהיה לו מעט דלק
            bus1.ifCanToFuel1 = true;
            DateTime myDate = new DateTime(2019, 9, 1);
            bus2.dateTreatLast1 = myDate;//אתחול אוטובוס כך שעברה שנה מאז הטיפול האחרון שלו
            bus2.Flag1 = (state)5; //bus2.ifReady1 = false;// האוטובוס הזה לא מוכן לנסיעה
            bus3.kilometersFromTreament1 = 19000;//אתחול אוטובוס כך שהוא כמעט צריך ללכת לטיפול מבחינת קילומטרים
            collectionBuses.Add(bus1); collectionBuses.Add(bus2); collectionBuses.Add(bus3); collectionBuses.Add(bus4);
            collectionBuses.Add(bus5); collectionBuses.Add(bus6); collectionBuses.Add(bus7); collectionBuses.Add(bus8);
            collectionBuses.Add(bus9); collectionBuses.Add(bus10);
            lbBusesOnSystem.ItemsSource = collectionBuses;
        }
        public void myRefresh()
        {
            lbBusesOnSystem.Items.Refresh();
        }
        private void button1_Click(object sender, RoutedEventArgs e)//חלון הוספת אוטובוס
        {
            Window1 secondWindow = new Window1();
            secondWindow.ShowDialog();
        }
        private void workerTravel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            Bus forNow = e.UserState as Bus;
            forNow.time1 = progress;
            lbBusesOnSystem.Items.Refresh();
        }
        private void workerTravel_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = (Bus)e.Argument;
            Bus current= (Bus)e.Argument;
            int distance1 = current.numOfKmInTheLastTime1;
            int quik = rand1.Next(20, 50);//הגרלת מרחק בקמש
            int time = distance1 /quik;
            for (int i = time * 6; i >= 0; i--)
            {
                Thread.Sleep(1000);
                workerTravel.ReportProgress(i, e.Argument);
            }
        }
        private void workerTravel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus result = (Bus)e.Result;
            result.Flag1 = (state)1;
            result.ifReady1 = true;
            result.ifCanToFuel1 = true;
            result.ifCanToTreat1 = true;
            lbBusesOnSystem.Items.Refresh();
        }
        private void travelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לנסיעה
        {
            var fxElt = sender as FrameworkElement;
            Bus CurrentBus = fxElt.DataContext as Bus;
            Window2 windowBusDrive = new Window2();
            windowBusDrive.ShowDialog();
            int.TryParse(windowBusDrive.ditance.Text, out int num);
            try
            {
                if (num == 0)
                    return;
                if (CurrentBus.checkIfReady(num) == true)
                {
                    CurrentBus.doingDriving(num);
                    MessageBox.Show("Sent successfully", "");
                }
                CurrentBus.numOfKmInTheLastTime1=num;
                CurrentBus.Flag1 = (state)2;//הפיכת הסטטוס לבנסיעה
                CurrentBus.ifReady1 = false;
                CurrentBus.ifCanToFuel1 = false;
                CurrentBus.ifCanToTreat1 = false;
                lbBusesOnSystem.Items.Refresh();
                workerTravel = new BackgroundWorker();
                workerTravel.DoWork += workerTravel_DoWork;
                workerTravel.ProgressChanged += workerTravel_ProgressChanged;
                workerTravel.RunWorkerCompleted += workerTravel_RunWorkerCompleted;
                workerTravel.WorkerReportsProgress = true;
                workerTravel.WorkerSupportsCancellation = true;
                workerTravel.RunWorkerAsync(CurrentBus);//שולחים כפרמטר את המרחק של הנסיעה
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }
        private void workerFuel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            Bus forNow = e.UserState as Bus;
            forNow.time1 = progress;
            lbBusesOnSystem.Items.Refresh();
        }
        private void workerFuel_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = e.Argument;
            for (int i = 12; i >=0; i--)
            {
                Thread.Sleep(1000);
                workerFuel.ReportProgress(i, e.Argument);
            }
        }
        private void workerFuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus result = (Bus)e.Result;
            result.Flag1 =(state)1;
            result.ifReady1 = true;
            result.ifCanToTreat1 = true;
            lbBusesOnSystem.Items.Refresh();
        }
        private void fuelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לתדלוק
        {
            var fxElt = sender as FrameworkElement;
            Bus CurrentBus = fxElt.DataContext as Bus;
            try
            {
                if (CurrentBus.kilometers1 == 0)
                    throw new ObjectNotAllowedException("The container is full");
                CurrentBus.setKilometers(0);//איפוס השדה שסופר את כמות הקילומטרים מאז התדלוק
                CurrentBus.Flag1 = (state)3;//הפיכת הסטטוס לבתדלוק
                lbBusesOnSystem.Items.Refresh();
                CurrentBus.ifReady1 = false;//לא יכול לבצע נסיעה בזמן תדלוק
                CurrentBus.ifCanToFuel1 = false;//לא יכול לבצע פעמיים תדלוק
                CurrentBus.ifCanToTreat1 = false;//לא יכול לבצע טיפול תתוך כדי תדלוק
                workerFuel = new BackgroundWorker();
                workerFuel.DoWork += workerFuel_DoWork;
                workerFuel.ProgressChanged += workerFuel_ProgressChanged;
                workerFuel.RunWorkerCompleted += workerFuel_RunWorkerCompleted;
                workerFuel.WorkerReportsProgress = true;
                workerFuel.WorkerSupportsCancellation = true;
                workerFuel.RunWorkerAsync(CurrentBus);
                MessageBox.Show("Refueling successfully", "");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void showData_doubleClick(object sender, RoutedEventArgs e)//חלון נתוני אוטובוס
        {
            var fxElt = sender as ListBox;
            Bus CurrentBus = fxElt.SelectedItem as Bus;
            WindowShowData showDataWindow = new WindowShowData(CurrentBus);
            showDataWindow.ShowDialog();
        }
        

    }
}
