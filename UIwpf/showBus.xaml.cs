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
    /// Interaction logic for showBus.xaml
    /// </summary>
    public partial class showBus : Window
    {
        IBL bl;
        public showBus(IBL _bl, BusBO currentBus)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentBus;
        }
    }
}
