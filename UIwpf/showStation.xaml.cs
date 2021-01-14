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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

using BLAPI;
using BO;
namespace UIwpf
{
    /// <summary>
    /// Interaction logic for showStation.xaml
    /// </summary>
    public partial class showStation : Window
    {
        IBL bl;
        private Stopwatch stopWatch;
        private bool isTimerRun;
        BackgroundWorker timerworker;
        TimeSpan tsStartTime;

        public showStation(IBL _bl, BusStationBO currentStation)
        {
            InitializeComponent();
            bl = _bl;
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
            tsStartTime = DateTime.Now.TimeOfDay;
            stopWatch.Restart();
            isTimerRun = true;
            timerworker.RunWorkerAsync(currentStation);
            DataContext = currentStation;
            try { lbLinesInStationOnSystem.DataContext = currentStation.ListOfLines.ToList(); }
            catch  { }//תיתפס כאן חריגה במצב שבו אין קווים שעוברים בתחנה
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurrentTime = tsStartTime + stopWatch.Elapsed;
            string timerText = tsCurrentTime.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
            BusStationBO currentStation = e.UserState as BusStationBO;
            dgForStation.ItemsSource = bl.GetLineTimingsPerStation(currentStation, tsCurrentTime);
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(231, e.Argument);
                Thread.Sleep(1000);
            }
        }
    }
}
