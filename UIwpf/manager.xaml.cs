﻿using System;
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
using System.ComponentModel;
using BLAPI;
using BO;

namespace UIwpf
{
    /// <summary>
    /// Interaction logic for manager.xaml
    /// </summary>
    public partial class manager : Window
    {
        IBL bl = BLFactory.GetBl();
        ObservableCollection<BusBO> buses = new ObservableCollection<BusBO>();
        public manager()
        {
            InitializeComponent();
            IEnumerable< BusBO> busList = bl.GetAllBusesBO();
            foreach (var item in busList)
            {
                buses.Add(item);
            }
            this.lbBusesOnSystem.ItemsSource = buses;
            //lbBusesOnSystem.ItemsSource = ;
        }

    }
}
