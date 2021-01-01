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
    /// Interaction logic for deleteLine.xaml
    /// </summary>
    public partial class deleteLine : Window
    {
        IBL bl;
        private BusLineBO newItem = new BusLineBO();
        public BusLineBO newItem1 { get => newItem; set => newItem = value; }
        public bool ifDone { get; set; } = false;
        public deleteLine(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void Button_ClickDel(object sender, RoutedEventArgs e)
        {
            ifDone = true;
            Close();
        }
    }
}
