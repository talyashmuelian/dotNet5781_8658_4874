﻿using System;
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
using BLAPI;
using BO;
namespace UIwpf
{
    /// <summary>
    /// Interaction logic for addDataToPaitStation.xaml
    /// </summary>
    public partial class addDataToPaitStation : Window
    {
        IBL bl;
        private PairConsecutiveStationsBO newItem = new PairConsecutiveStationsBO();
        public PairConsecutiveStationsBO newItem1 { get => newItem; set => newItem = value; }
        private NamesStations help = new NamesStations();
        public NamesStations help1 { get => help; set => help = value; }
        private string NameStation1;
        private string NameStation2;
        public bool ifDone { get; set; } = false;
        public addDataToPaitStation(IBL _bl, int station1, int station2)
        {
            InitializeComponent();
            bl = _bl;
            newItem.StationNum1 = station1;
            newItem.StationNum2 = station2;
            help.NameStation1 = bl.GetBusStationBO(station1).NameStation;
            help.NameStation2 = bl.GetBusStationBO(station2).NameStation;
            nameStaion1.DataContext = help;
            nameStaion2.DataContext = help;
            numStaion1.DataContext = newItem;
            numStaion2.DataContext = newItem;
            //distance.DataContext = newItem;
            //timeDriving.DataContext = newItem;
            
            
            
        }

        private void Button_ClickAddData(object sender, RoutedEventArgs e)
        {
            try
            {
                newItem.Distance = Convert.ToDouble(distance.Text);
                int hours1 = Convert.ToInt32(hours.Text);
                int minutes1 = Convert.ToInt32(minutes.Text);
                int seconds1 = Convert.ToInt32(seconds.Text);
                if (hours1 < 0 || hours1 > 24 || minutes1 < 0 || minutes1 > 60 || seconds1 < 0 || seconds1 > 60)
                {
                    throw new LineTripExceptionBO("הזמן אינו תקין");
                }
                TimeSpan time = new TimeSpan(hours1, minutes1, seconds1);
                newItem.TimeDriving = time;
                bl.addPairConsecutiveStations(newItem);
                ifDone = true;
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            
        }
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //e.Key == Key.Enter ||
            //allow get out of the text box
            if (e.Key == Key.Return || e.Key == Key.Tab)
            {
                Close();
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                return;
            }
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
    }
}
