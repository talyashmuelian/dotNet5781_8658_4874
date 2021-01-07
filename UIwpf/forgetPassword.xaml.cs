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
    /// Interaction logic for forgetPassword.xaml
    /// </summary>
    public partial class forgetPassword : Window
    {
        IBL bl;
        public forgetPassword(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }

        private void Button_ClickReturnPassword(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("הסיסמה שלך היא:   "+ bl.forgetPassWord(userName.Text, checkAsk.Text), "", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
