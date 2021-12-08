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
using TheBank.Models;
using TheBank.ViewModel;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    { /// <summary>
      /// Для создания нового сотрудника
      /// </summary>
        public EmployeePage()
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new EmployeePageVM(this);
        }

        /// <summary>
        /// Для редактирования существующего сотрудника
        /// </summary>
        /// <param name="employee"></param>
        public EmployeePage(Employee employee)
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new EmployeePageVM(this, employee);
        }
    }
}
