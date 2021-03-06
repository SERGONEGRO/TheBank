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
using TheBank.ViewModel;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для EmployeesTablePage.xaml
    /// </summary>
    public partial class EmployeesTablePage : Page
    {
        public EmployeesTablePage(EmployeesPageVM employeesPageVM)
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new EmployeesTablePageVM(this, employeesPageVM);
        }
    }
}
