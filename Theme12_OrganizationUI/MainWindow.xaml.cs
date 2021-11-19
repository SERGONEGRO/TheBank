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

namespace Theme12_OrganizationUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Organization TheBestCoders;

        public MainWindow()
        {
            InitializeComponent();
            TheBestCoders = new Organization(3);

            cbDepartment.ItemsSource = TheBestCoders.deps;
        }

        private void btnRef(object sender, RoutedEventArgs e)
        {
            cbDepartment.Items.Refresh();
            lvWorkers.Items.Refresh();
        }

        private void CbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvWorkers.ItemsSource = TheBestCoders.deps[cbDepartment.SelectedIndex].employees;
        }


        //private bool find(Worker arg)
        //{
        //    return arg.DepartamentId == (cbDepartment.SelectedItem as Department).DepartmentId;
        //}
    }
}
