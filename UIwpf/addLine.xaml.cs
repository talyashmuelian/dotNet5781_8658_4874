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
    /// Interaction logic for addLine.xaml
    /// </summary>
    public partial class addLine : Window
    {
        IBL bl;
        private BusLineBO newItem = new BusLineBO();
        public BusLineBO newItem1 { get => newItem; set => newItem = value; }
        private List<string> areas = new List<string>();
        public addLine(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            areas.Add("ירושלים");
            areas.Add("מרכז");
            areas.Add("דרום");
            areas.Add("צפון");
            DataContext = newItem;
            areaCB.ItemsSource = areas;
            station1CB.ItemsSource =bl.GetAllBusStationsBO();
            station2CB.ItemsSource = bl.GetAllBusStationsBO();
        }
        private void areaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newItem.Area = areaCB.SelectedItem.ToString();
        }
        private void station1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newItem.FirstStationNum = (station1CB.SelectedItem as BusStationBO).CodeStation;
        }
        private void station2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as BusStationBO).CodeStation == (station1CB.SelectedItem as BusStationBO).CodeStation)
                    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                newItem.LastStationNum = (station2CB.SelectedItem as BusStationBO).CodeStation;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as BusStationBO).CodeStation == (station1CB.SelectedItem as BusStationBO).CodeStation)
                    throw new BO.BusStationExceptionBO("לא ניתן לבחור את אותה תחנה פעמיים");
                BusStationBO newStation1 = bl.GetBusStationBO(newItem.FirstStationNum);
                BusStationBO newStation2 = bl.GetBusStationBO(newItem.LastStationNum);
                PairConsecutiveStationsBO pair = bl.GetPairConsecutiveStationsBO(newItem.FirstStationNum, newItem.LastStationNum);
                if (pair == null)//אין עדיין מידע עבור זוג התחנות הללו
                {
                    //נפתח חלון חדש שמבקש מידע עבור המרחק בין התחנות האלה
                    addDataToPaitStation addDataToPaitStationWindow = new addDataToPaitStation(bl, newItem.FirstStationNum, newItem.LastStationNum);
                    addDataToPaitStationWindow.ShowDialog();
                }
                bl.addBusLine(newItem);
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
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

            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
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
