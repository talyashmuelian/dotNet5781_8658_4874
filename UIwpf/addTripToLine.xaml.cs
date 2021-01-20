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
using BLAPI;
using BO;
namespace UIwpf
{
    /// <summary>
    /// Interaction logic for addTripToLine.xaml
    /// </summary>
    public partial class addTripToLine : Window
    {
        IBL bl;
        private BusLineBO newItem = new BusLineBO();
        public BusLineBO newItem1 { get => newItem; set => newItem = value; }
        public addTripToLine(IBL _bl, BusLineBO currentLine)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentLine;
            newItem = currentLine;
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                int hours1 = Convert.ToInt32(hours.Text);
                int minutes1 = Convert.ToInt32(minutes.Text);
                int seconds1 = Convert.ToInt32(seconds.Text);
                if (hours1 < 0 || hours1 > 24 || minutes1 < 0 || minutes1 > 60 || seconds1 < 0 || seconds1 > 60)
                {
                    throw new LineTripExceptionBO("השעה שהכנסת אינה תקינה");
                }
                TimeSpan trip = new TimeSpan(hours1, minutes1, seconds1);
                bl.addLineTrip(new LineTripBO { IdentifyNumber = newItem.IdentifyNumber, TripStart = trip });
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
