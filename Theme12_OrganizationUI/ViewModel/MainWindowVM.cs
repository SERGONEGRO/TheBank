using TheBank.Models;
using TheBank.View;
using System;
using System.Windows;

namespace TheBank.ViewModel
{
    class MainWindowVM : ViewModel
    {
        /// <summary>
        /// Главное окно
        /// </summary>
        Window window;

        /// <summary>
        /// Страница "Департаменты"
        /// </summary>
        public DepartmentsPage DepartmentsPage { get; set; }

        /// <summary>
        /// Страница "Сотрудники"
        /// </summary>
        public EmployeesPage EmployeesPage { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mainWindow"></param>
        public MainWindowVM(Window mainWindow)
        {
            window = mainWindow;
            DepartmentsPage = new DepartmentsPage();
            EmployeesPage = new EmployeesPage();
        }
    }
}
