using TheBank.Models;
using TheBank.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace TheBank.ViewModel
{
    class EmployeesTablePageVM : ViewModel
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public EmployeesTablePageVM(Page page, EmployeesPageVM employeesPageVM)
        {
            employees = DataWorker.GetEmployees();
            tableLV = page.FindName("tableLV") as ListView;
            this.employeesPageVM = employeesPageVM;
        }

        /// <summary>
        /// Страница "сотрудники"
        /// </summary>
        EmployeesPageVM employeesPageVM;

        /// <summary>
        /// Таблица сотрудников
        /// </summary>
        ListView tableLV;

        /// <summary>
        /// Все сотрудники
        /// </summary>
        ObservableCollection<Employee> employees;

        /// <summary>
        /// Все сотрудники (свойство)
        /// </summary>
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                NotifyPropertyChanged("Employees");
            }
        }

        #region КОМАНДЫ
        /// <summary>
        /// Удалить сотрудника (команда)
        /// </summary>
        RelayCommand removeEmployeeCommand;

        /// <summary>
        /// Удалить сотрудника (свойство)
        /// </summary>
        public RelayCommand RemoveEmployeeCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (removeEmployeeCommand == null)
                {
                    removeEmployeeCommand = new RelayCommand(obj =>
                    {
                        //получаем выделенного сотрудника
                        Employee employee = tableLV.SelectedItem as Employee;
                        //пытаемся удалить его
                        if (DataWorker.RemoveEmployee(employee))
                        {
                            MessageBox.Show("Сотрудник успешно удален");
                        }
                    });
                }
                return removeEmployeeCommand;
            }
        }

        /// <summary>
        /// Переместить сотрудника (команда)
        /// </summary>
        RelayCommand replaceEmployeeCommand;

        /// <summary>
        /// Переместить сотрудника (свойство)
        /// </summary>
        public RelayCommand ReplaceEmployeeCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (replaceEmployeeCommand == null)
                {
                    replaceEmployeeCommand = new RelayCommand(obj =>
                    {
                        //если выбран какой-то сотрудник
                        if (tableLV.SelectedItem != null)
                        {
                            //создаем новое окошко
                            Window window = new ReplaceEmployeeWindow(tableLV.SelectedItem as Employee);
                            //помещаем его по центру
                            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            //показываем его
                            window.Show();
                        }
                    });
                }
                return replaceEmployeeCommand;
            }
        }

        /// <summary>
        /// Редактировать сотрудника (команда)
        /// </summary>
        RelayCommand editEmployeeCommand;

        /// <summary>
        /// Редактировать сотрудника (свойство)
        /// </summary>
        public RelayCommand EditEmployeeCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (editEmployeeCommand == null)
                {
                    editEmployeeCommand = new RelayCommand(obj =>
                    {
                        if (tableLV.SelectedItem != null)
                        {
                            Page page = new EmployeePage(tableLV.SelectedItem as Employee);
                            employeesPageVM.CurrentPage = page;
                        }
                    });
                }
                return editEmployeeCommand;
            }
        }

        /// <summary>
        /// Редактировать сотрудника (команда)
        /// </summary>
        RelayCommand sortByNameCommand;

        /// <summary>
        /// Редактировать сотрудника (свойство)
        /// </summary>
        public RelayCommand SortByNameCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (sortByNameCommand == null)
                {
                    sortByNameCommand = new RelayCommand(obj =>
                    {
                        //сортируем сотрудников по фамилии
                        var soretedEmployees = from u in Employees
                                               orderby u.LastName
                                               select u;
                        ObservableCollection<Employee> temp = new ObservableCollection<Employee>();
                        foreach (var item in soretedEmployees)
                        {
                            temp.Add(item);
                        }
                        Employees = temp;
                    });
                }
                return sortByNameCommand;
            }
        }
        #endregion
    }
}
