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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using dotNet5781_01_8658_4874;

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for WindowShowData.xaml
    /// </summary>
    public partial class WindowShowData : Window
    {
        public Bus MyData { get; set; }
        BackgroundWorker workerTreatment;
        BackgroundWorker workerFuel;
        BackgroundWorker workerTravel;

        public WindowShowData(Bus current)
        {
            InitializeComponent();
            grid2.DataContext = current;
            MyData = current;
        }
        private void workerTravel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //string timerText = stopWatch.Elapsed.ToString();
            //timerText = timerText.Substring(0, 8);
            //this.timerTextBlock.Text = timerText;
        }
        private void workerTravel_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = (Bus)e.Argument;
            Bus current = (Bus)e.Argument;
            int distance1 = current.numOfKmInTheLastTime1;
            int quik = MainWindow.rand1.Next(20, 50);//הגרלת מרחק בקמש
            int time = distance1 / quik;
            Thread.Sleep(time * 6000);//צריך לתת לו לישון לפי זמן הנסיעה
        }
        private void workerTravel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus result = (Bus)e.Result;
            result.Flag1 = (state)1;
        }
        private void travelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לנסיעה
        {
            var fxElt = sender as FrameworkElement;
            //Bus CurrentBus = fxElt.DataContext as Bus;
            MyData = fxElt.DataContext as Bus;
            Window2 windowBusDrive = new Window2();
            windowBusDrive.ShowDialog();
            int.TryParse(windowBusDrive.ditance.Text, out int num);
            try
            {
                if (MyData.checkIfReady(num) == true)
                {
                    MyData.doingDriving(num);
                    MessageBox.Show("Sent successfully", "");
                }
                MyData.numOfKmInTheLastTime1 = num;
                MyData.Flag1 = (state)2;//הפיכת הסטטוס לבנסיעה
                workerTravel = new BackgroundWorker();
                workerTravel.DoWork += workerTravel_DoWork;
                workerTravel.ProgressChanged += workerTravel_ProgressChanged;
                workerTravel.RunWorkerCompleted += workerTravel_RunWorkerCompleted;
                workerTravel.WorkerReportsProgress = true;
                workerTravel.WorkerSupportsCancellation = true;
                workerTravel.RunWorkerAsync(MyData);//שולחים כפרמטר את המרחק של הנסיעה
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void workerFuel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //string timerText = stopWatch.Elapsed.ToString();
            //timerText = timerText.Substring(0, 8);
            //this.timerTextBlock.Text = timerText;
        }


        private void workerFuel_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(12000);//צריך לתת לו לישון לפי זמן הנסיעה
            e.Result = e.Argument;
        }
        private void workerFuel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus result = (Bus)e.Result;
            result.Flag1 = (state)1;
        }
        private void fuelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לתדלוק
        {
            var fxElt = sender as FrameworkElement;
            //Bus CurrentBus = fxElt.DataContext as Bus;
            MyData = fxElt.DataContext as Bus;
            MyData.setKilometers(0);//איפוס השדה שסופר את כמות הקילומטרים מאז התדלוק
            MyData.Flag1 = (state)3;//הפיכת הסטטוס לבתדלוק
            MessageBox.Show("Refueling successfully", "");
            workerFuel = new BackgroundWorker();
            workerFuel.DoWork += workerFuel_DoWork;
            workerFuel.ProgressChanged += workerFuel_ProgressChanged;
            workerFuel.RunWorkerCompleted += workerFuel_RunWorkerCompleted;
            workerFuel.WorkerReportsProgress = true;
            workerFuel.WorkerSupportsCancellation = true;
            workerFuel.RunWorkerAsync(MyData);

        }
        private void workerTreatment_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //string timerText = stopWatch.Elapsed.ToString();
            //timerText = timerText.Substring(0, 8);
            //this.timerTextBlock.Text = timerText;
        }
        private void workerTreatment_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(144000);
            e.Result = e.Argument;
        }
        private void workerTreatment_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus result = (Bus)e.Result;
            result.Flag1 = (state)1;
        }
        private void treatmentButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לטיפול
        {
            var fxElt = sender as FrameworkElement;
            //Bus CurrentBus = fxElt.DataContext as Bus;
            MyData = fxElt.DataContext as Bus;
            MyData.treatment();//שליחה לטיפול
            MessageBox.Show("Treatment successfully", "");
            MyData.Flag1 = (state)4;//שינוי הסטטוס לבטיפול
            workerTreatment = new BackgroundWorker();
            workerTreatment.DoWork += workerTreatment_DoWork;
            workerTreatment.ProgressChanged += workerTreatment_ProgressChanged;
            workerTreatment.RunWorkerCompleted += workerTreatment_RunWorkerCompleted;
            workerTreatment.WorkerReportsProgress = true;
            workerTreatment.WorkerSupportsCancellation = true;
            workerTreatment.RunWorkerAsync(MyData);
        }
    }
}
