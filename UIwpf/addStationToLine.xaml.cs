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
    /// Interaction logic for addStationToLine.xaml
    /// </summary>
    public partial class addStationToLine : Window
    {
        IBL bl;
        private stationToLine newItem = new stationToLine();
        public stationToLine newItem1 { get => newItem; set => newItem = value; }
        public addStationToLine(IBL _bl, BusLineBO currentLine)
        {
            InitializeComponent();
            bl = _bl;
            newItem.IdentifyNumber = currentLine.IdentifyNumber;
            newItem.LineNumber = currentLine.LineNumber;
            DataContext = newItem;
            stationCB.ItemsSource = bl.GetAllMiniStationsBO();
        }
        private void stationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newItem.CodeStation = (stationCB.SelectedItem as MiniStationBO).CodeStation;
        }

        private void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.chekIfCanToddStationToLine(newItem.CodeStation, newItem.IdentifyNumber, newItem.Location);
                addDataToPaitStation addDataToPaitStationWindow1;
                addDataToPaitStation addDataToPaitStationWindow2;
                bool flag1 = false;//דגל האם הוא אישר הוספת מידע בחלון הראשון
                bool flag2 = false;//דגל האם הוא אישר הוספת מידע בחלון השני
                //זימון פונקציות שבודקת האם קיימות ישויות תחנות עוקבות לתחנה החדשה עם זאת שלפניה ואחריה
                int beforeStation = bl.ifNeedToGetDataToBeforeStation(newItem.IdentifyNumber, newItem.CodeStation, newItem.Location);
                if (beforeStation != 0)
                {
                    //נפתח חלון חדש שמבקש מידע עבור המרחק בין התחנות האלה
                    addDataToPaitStationWindow1 = new addDataToPaitStation(bl, beforeStation, newItem.CodeStation);
                    addDataToPaitStationWindow1.ShowDialog();
                    if (addDataToPaitStationWindow1.ifDone)
                    {
                        flag1 = true;
                    }
                }
                int afterStation = bl.ifNeedToGetDataToAfterStation(newItem.IdentifyNumber, newItem.CodeStation, newItem.Location);
                if (afterStation !=0)
                {
                    //נפתח חלון חדש שמבקש מידע עבור המרחק בין התחנות האלה
                    addDataToPaitStationWindow2 = new addDataToPaitStation(bl, afterStation, newItem.CodeStation);
                    addDataToPaitStationWindow2.ShowDialog();
                    if (addDataToPaitStationWindow2.ifDone)
                    {
                        flag2 = true;
                    }
                }
                if (flag1&&flag2)
                {
                    bl.addStationToLine(newItem.CodeStation, newItem.IdentifyNumber, newItem.Location);
                    MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
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
