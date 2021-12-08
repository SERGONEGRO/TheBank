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
    public class EmployeesPageVM : ViewModel
    {
        /// <summary>
        /// Текущая отображаемая страница
        /// </summary>
        Page currentPage;

        /// <summary>
        /// Текущая отображаемая страница (свойство)
        /// </summary>
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                NotifyPropertyChanged("CurrentPage");
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mainWindow"></param>
        public EmployeesPageVM(Page page)
        {
            //показываем таблицу сотрудников
            currentPage = new EmployeesTablePage(this);

            //подписываемся на событие "БД обновилась"
            DataWorker.DBChanged += UpdateEmployeesTable;
        }

        #region КОМАНДЫ
        /// <summary>
        /// Добавить сотрудника (команда)
        /// </summary>
        RelayCommand addEmployeeCommand;

        /// <summary>
        /// Добавить сотрудника (свойство)
        /// </summary>
        public RelayCommand AddEmployeeCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (addEmployeeCommand == null)
                {
                    addEmployeeCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = new EmployeePage();
                    });
                }
                return addEmployeeCommand;
            }
        }

        /// <summary>
        /// Показать таблицу сотрудников (команда)
        /// </summary>
        RelayCommand showTableCommand;

        /// <summary>
        /// Показать таблицу сотрудников (свойство)
        /// </summary>
        public RelayCommand ShowTableCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (showTableCommand == null)
                {
                    showTableCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = new EmployeesTablePage(this);
                    });
                }
                return showTableCommand;
            }
        }

        /// <summary>
        /// Показать таблицу учета рабочего времени (команда)
        /// </summary>
        RelayCommand showTimeTrackingPageCommand;

        /// <summary>
        /// Показать таблицу учета рабочего времени (свойство)
        /// </summary>
        public RelayCommand ShowTimeTrackingPageCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (showTimeTrackingPageCommand == null)
                {
                    showTimeTrackingPageCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = new TimeTrackingPage();
                    });
                }
                return showTimeTrackingPageCommand;
            }
        }

        /// <summary>
        /// Показать сводку по з/п (команда)
        /// </summary>
        RelayCommand showSalaryPageCommand;

        /// <summary>
        /// Показать сводку по з/п (свойство)
        /// </summary>
        public RelayCommand ShowSalaryPageCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (showSalaryPageCommand == null)
                {
                    showSalaryPageCommand = new RelayCommand(obj =>
                    {
                        CurrentPage = new SalaryPage();
                    });
                }
                return showSalaryPageCommand;
            }
        }
        #endregion

        #region МЕТОДЫ
        /// <summary>
        /// Обновляет таблицу сотрудников
        /// </summary>
        private void UpdateEmployeesTable()
        {
            CurrentPage = new EmployeesTablePage(this);
        }
        #endregion


    }
}

