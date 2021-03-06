using TheBank.Models;
using TheBank.ViewModel;
using System.Windows.Controls;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        /// <summary>
        /// Конструктор (при показе существующего департамента)
        /// </summary>
        /// <param name="department"></param>
        public DepartmentPage(Department department)
        {
            InitializeComponent();

            //подключаем VM
            DataContext = new DepartmentPageVM(this, department);

        }

        /// <summary>
        /// Конструктор (при создании нового департамента)
        /// </summary>
        public DepartmentPage(string parentName)
        {
            InitializeComponent();

            //подключаем VM
            DataContext = new DepartmentPageVM(this);
        }
    }
}
