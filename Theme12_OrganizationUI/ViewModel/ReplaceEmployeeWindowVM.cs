using TheBank.Models;
using TheBank.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TheBank.ViewModel
{
    class ReplaceEmployeeWindowVM : ViewModel
    {
        public ReplaceEmployeeWindowVM(Window window, Employee employee)
        {
            messageTB = window.FindName("messageTB") as TextBlock;
            departmentsCB = window.FindName("departmentsCB") as ComboBox;
            messageTB.Text = $"Выберите департамент, в который будет перемещен  {employee.LastName} {employee.FirstName}";
            departments = DataWorker.GetAllDepartments();
            this.employee = employee;
            this.window = window;
        }

        /// <summary>
        /// Окно
        /// </summary>
        Window window;

        /// <summary>
        /// Сотрудник, которого редактируем
        /// </summary>
        Employee employee;

        /// <summary>
        /// Текстблок сообщения
        /// </summary>
        TextBlock messageTB;

        /// <summary>
        /// Выпадающий список департаментов
        /// </summary>
        ComboBox departmentsCB;

        /// <summary>
        /// Все департаменты
        /// </summary>
        List<Department> departments;

        /// <summary>
        /// Все департаменты (свойство)
        /// </summary>
        public List<Department> Departments
        {
            get
            {
                return departments;
            }
            set
            {
                departments = value;
                NotifyPropertyChanged("Departments");
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
                        //если выбран какой-то департамент
                        if (departmentsCB.SelectedItem != null)
                        {
                            //проверяем, не тот ли это, в котором сотрудник УЖЕ числится
                            if ((departmentsCB.SelectedItem as Department).Name != employee.DepartmentName)
                            {
                                //перемещаем сотрудника
                                DataWorker.ReplaceEmployee(employee, (departmentsCB.SelectedItem as Department).Name);
                                //закрываем окно
                                window.Close();
                            }
                        }
                    });
                }
                return replaceEmployeeCommand;
            }
        }


    }
}
