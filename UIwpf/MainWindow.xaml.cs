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

using BLAPI;
using BO;

namespace UIwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = BLFactory.GetBl();
        private UserBO newItem = new UserBO();
        public UserBO newItem1 { get => newItem; set => newItem = value; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = newItem;
        }
        

        private void Button_ClickLogIn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.ifUserAndPassCorrect(newItem.UserName, password.Password))//אם קיים במערכת משתמש כזה
                {
                    manager managerWindow = new manager(bl);
                    managerWindow.ShowDialog();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void forgetPassWord_doubleClick(object sender, RoutedEventArgs e)//שכחתי סיסמה
        {
            forgetPassword forgetPasswordWindow = new forgetPassword(bl);
            forgetPasswordWindow.ShowDialog();
        }
        private void signUp_doubleClick(object sender, RoutedEventArgs e)//הרשמת משתמש מנהל
        {
            signUp signUpWindow = new signUp(bl);
            signUpWindow.ShowDialog();
        }

        private void Button_ClickPassenger(object sender, RoutedEventArgs e)
        {
            passenger passengerWindow = new passenger(bl);
            passengerWindow.Show();
        }
    }
}
