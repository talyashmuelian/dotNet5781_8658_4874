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
using System.ComponentModel;
using System.Threading;

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
            Close();
        }
        //private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    if (text == null) return; if (e == null) return;
        //    if (e.Key == Key.Enter || e.Key == Key.Return)
        //    {
        //        if (text.Text.Length > 0)
        //        {
        //            int amount = int.Parse(text.Text);
        //            text.Text = "";
        //            if (sender == txtDeposit) myAccount.Deposit(amount);
        //            else if (sender == txtWithdraw)
        //                if (!myAccount.Withdraw(amount))
        //                    MessaBoxShow("You don’t have enough money!");
        //        }
        //        e.Handled = true;
        //        return;
        //    }
        //    // It`s a system key (add other key here if you want to allow)
        //    if (e.Key == Key.Escape || e.Key == Key.Tab ||
        //    e.Key == Key.Back || e.Key == Key.Delete ||
        //    e.Key == Key.CapsLock || e.Key == Key.LeftShift || 
        //    e.Key == Key.Down || e.Key == Key.Right) return;
        //    char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
        //    if (Char.IsControl(c)) return;
        //    if (Char.IsDigit(c))
        //        if (!(Keyboard.IsKeyDown(Key.LeftShift) ||
        //        Keyboard.IsKeyDown(Key.RightAlt)))
        //            return;
        //    e.Handled = true;
        //    MessageBox.Show("Only numbers are allowed");
        //}

        //private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    TextBox text = sender as TextBox;
        //    if (text == null) return;
        //    if (e == null) return;


        //    if ((e.Key == Key.Enter) || (e.Key == Key.Return))
        //    {
        //        if (text.Text.Length > 0)
        //        {

        //            drive();
        //            text.Text = "";
        //            this.Close();
        //            //forbid letters and signs (#,$, %, ...)
        //            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
        //            return;

        //        }


        //    }


        //    //allow get out of the text box
        //    if (e.Key == Key.Tab)
        //        return;

        //    //allow list of system keys (add other key here if you want to allow)
        //    if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
        //        e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
        //     || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
        //        return;

        //    char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

        //    //allow control system keys
        //    if (Char.IsControl(c)) return;

        //    //allow digits (without Shift or Alt)
        //    if (Char.IsDigit(c))
        //        if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
        //            return; //let this key be written inside the textbox



        //}

    }
}
