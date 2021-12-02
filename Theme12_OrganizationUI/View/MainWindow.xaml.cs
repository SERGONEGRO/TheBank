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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Theme12_OrganizationUI.ViewModel;

namespace Theme12_OrganizationUI.View
{//https://www.wpf-tutorial.com/ru/85/элемент-управления-treeview/treeview-привязка-данных-и-несколько-шаблонов/
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Organization TheBestCoders;

        public MainWindow()
        {
            InitializeComponent();

            //подключаем VM
            DataContext = new MainWindowVM(this);
        }
    }
}
