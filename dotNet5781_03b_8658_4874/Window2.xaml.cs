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

namespace dotNet5781_03b_8658_4874
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window//חלון להוספת נסיעה לאוטובוס מסוים
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void sentButton_Click(object sender, RoutedEventArgs e)
        {
            if (true)//לזמן מתודה שתבדוק האם האוטובוס הנמצא יכול לצאת לנסיעה מבחינת דלק וקילומטרז, או שעברה שנה מאז הטיפול האחרון'
            {
                MessageBox.Show("נשלח בהצלחה");//listBuses[i].doingDriving(num);//לזמן מתודה שמוסיפה את הקילומטרים של נסיעה זו For a method time that adds the miles of this trip
            }
            else
            {
                MessageBox.Show("האוטובוס אינו יכול לבצע את הנסיעה מחוסר דלק או עקב קילומטרז' גבוה מידי", "שגיאה");
            }
        }
    }
}
