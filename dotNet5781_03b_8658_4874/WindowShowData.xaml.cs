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
using dotNet5781_01_8658_4874;

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for WindowShowData.xaml
    /// </summary>
    public partial class WindowShowData : Window
    {
        public Bus MyData { get; set; }

        public WindowShowData(Bus current)
        {
            InitializeComponent();
            grid2.DataContext = current;
            MyData = current;
        }
        private void travelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לנסיעה
        {
            var fxElt = sender as FrameworkElement;
            //Bus CurrentBus = fxElt.DataContext as Bus;
            MyData= fxElt.DataContext as Bus;
            Window2 windowBusDrive = new Window2();
            windowBusDrive.ShowDialog();
            int.TryParse(windowBusDrive.ditance.Text, out int num);
            try
            {
                if (MyData.checkIfReady(num) == false)
                    throw new ObjectCannotDoException("The bus cannot make the travel");//אם האוטובוס לא יכול לבצע את הנסיעה
                else
                {
                    MyData.doingDriving(num);
                    MessageBox.Show("נשלח בהצלחה", "");
                }
            }
            catch (ObjectCannotDoException ex) { MessageBox.Show(ex.Message); }
        }
        private void fuelButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לתדלוק
        {
            var fxElt = sender as FrameworkElement;
            //Bus CurrentBus = fxElt.DataContext as Bus;
            MyData = fxElt.DataContext as Bus;
            MyData.setKilometers(0);//איפוס השדה שסופר את כמות הקילומטרים מאז התדלוק
            MessageBox.Show("התדלוק בוצע בהצלחה", "");
        }
        private void treatmentButton_Click(object sender, RoutedEventArgs e)//כפתור שליחה לטיפול
        {
            var fxElt = sender as FrameworkElement;
            //Bus CurrentBus = fxElt.DataContext as Bus;
            MyData = fxElt.DataContext as Bus;
            MyData.treatment();//שליחה לטיפול
            MessageBox.Show("הטיפול בוצע בהצלחה", "");
        }
    }
}
