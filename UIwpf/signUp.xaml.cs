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
    /// Interaction logic for signUp.xaml
    /// </summary>
    public partial class signUp : Window
    {
        IBL bl;
        private UserBO newItem = new UserBO();
        public UserBO newItem1 { get => newItem; set => newItem = value; }
        public signUp(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void Button_ClickSignUp(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.addUser(new UserBO { UserName = newItem.UserName, PassWord = newItem.PassWord, CheckAsk = newItem.CheckAsk });
                MessageBox.Show("!ההרשמה בוצעה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();
        }
    }
}
