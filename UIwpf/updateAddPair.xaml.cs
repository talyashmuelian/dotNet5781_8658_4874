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
    /// Interaction logic for updateAddPair.xaml
    /// </summary>
    public partial class updateAddPair : Window
    {
        IBL bl;
        private PairConsecutiveStationsBO newItem = new PairConsecutiveStationsBO();
        public PairConsecutiveStationsBO newItem1 { get => newItem; set => newItem = value; }
        public bool ifDone { get; set; } = false;
        public updateAddPair(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = newItem;
        }

        private void Button_ClickDoIt(object sender, RoutedEventArgs e)
        {
            ifDone = true;
            Close();
        }
    }
}
