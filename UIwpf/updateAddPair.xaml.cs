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
    /// Interaction logic for updateAddPair.xaml
    /// </summary>
    public partial class updateAddPair : Window
    {
        IBL bl;
        private PairConsecutiveStationsBO newItem = new PairConsecutiveStationsBO();
        public PairConsecutiveStationsBO newItem1 { get => newItem; set => newItem = value; }
        public bool ifDone { get; set; } = false;
        public updateAddPair(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
            station1CB.ItemsSource = bl.GetAllMiniStationsBO();
            station2CB.ItemsSource = bl.GetAllMiniStationsBO();
        }
        private void station1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newItem.StationNum1 = (station1CB.SelectedItem as MiniStationBO).CodeStation;
        }
        private void station2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as MiniStationBO).CodeStation == (station1CB.SelectedItem as MiniStationBO).CodeStation)
                    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                newItem.StationNum2 = (station2CB.SelectedItem as MiniStationBO).CodeStation;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void Button_ClickDoIt(object sender, RoutedEventArgs e)
        {
            try
            {
                int hours1 = Convert.ToInt32(hours.Text);
                int minutes1 = Convert.ToInt32(minutes.Text);
                int seconds1 = Convert.ToInt32(seconds.Text);
                if (hours1 < 0 || hours1 > 24 || minutes1 < 0 || minutes1 > 60 || seconds1 < 0 || seconds1 > 60)
                {
                    throw new LineTripExceptionBO("הזמן אינו תקין");
                }
                TimeSpan time = new TimeSpan(hours1, minutes1, seconds1);
                newItem.TimeDriving = time;
                if ((station2CB.SelectedItem as MiniStationBO).CodeStation == (station1CB.SelectedItem as MiniStationBO).CodeStation)
                    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                ifDone = true;
                Close();
            }
            catch (BusStationExceptionBO ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); Close(); }

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
