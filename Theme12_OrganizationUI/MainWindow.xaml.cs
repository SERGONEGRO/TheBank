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
{//https://www.wpf-tutorial.com/ru/85/элемент-управления-treeview/treeview-привязка-данных-и-несколько-шаблонов/
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Organization TheBestCoders;

        public MainWindow()
        {
            InitializeComponent();
            TheBestCoders = new Organization(5);  
            tvDepartments.ItemsSource = TheBestCoders.deps;
        }

        private void btnRef(object sender, RoutedEventArgs e)
        {
            cbDepartment.Items.Refresh();
            tvDepartments.Items.Refresh();
            //lvWorkers.Items.Refresh();
        }

        private void CbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lvWorkers.ItemsSource = TheBestCoders.deps[cbDepartment.SelectedIndex].employees;
            //dgEmployees.ItemsSource = TheBestCoders.deps[tvDepartments.].employees;
        }

        private void tvDepartments_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //lvWorkers.ItemsSource = TheBestCoders.deps[tvDepartments.SelectedItem. ].employees;
            tbTest.Text = tvDepartments.SelectedValue.ToString();
        }


        //private bool find(Worker arg)
        //{
        //    return arg.DepartamentId == (cbDepartment.SelectedItem as Department).DepartmentId;
        //}
    }
}
