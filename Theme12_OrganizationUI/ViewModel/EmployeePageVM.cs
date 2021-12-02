using Theme12_OrganizationUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Theme12_OrganizationUI.ViewModel
{
    class EmployeePageVM : ViewModel
    {

        #region КОНСТРУКТОРЫ
        /// <summary>
        /// Конструктор (новый сорудник)
        /// </summary>
        /// <param name="page"></param>
        public EmployeePageVM(Page page)
        {
            this.page = page;
            isNew = true;
            Initialize();
        }

        /// <summary>
        /// Конструктор (существующий сорудник)
        /// </summary>
        /// <param name="page"></param>
        public EmployeePageVM(Page page, Employee employee)
        {
            this.page = page;
            isNew = false;
            this.employee = employee;
            FillViewsAndSources();
            debugBttn.Visibility = Visibility.Hidden;
            departmentCB.IsEnabled = false;
        }
        #endregion

        #region ВЬЮ-ЭЛЕМЕНТЫ
        /// <summary>
        /// Текстблок: Место для вывода сообщения
        /// </summary>
        TextBlock messageTB;

        /// <summary>
        /// Кнопка рандомного заполнения полей
        /// </summary>
        Button debugBttn;

        /// <summary>
        /// Комбобокс: департамент
        /// </summary>
        ComboBox departmentCB;

        /// <summary>
        /// Граница блока: пол
        /// </summary>
        Border genderBorder;

        /// <summary>
        /// Граница блока: департамент
        /// </summary>
        Border departmentBorder;

        /// <summary>
        /// Граница блока: категория должности
        /// </summary>
        Border positionCathegoryBorder;

        #region ТЕКСТБОКСЫ
        /// <summary>
        /// Текстбокс: Фамилия
        /// </summary>
        TextBox lastnameTB;

        /// <summary>
        /// Текстбокс: Имя
        /// </summary>
        TextBox firstnameTB;

        /// <summary>
        /// Текстбокс: Ставка
        /// </summary>
        TextBox rateTB;

      
        #endregion

        #region РАДИОКНОПКИ
       
        /// <summary>
        /// Радиокнопка: Менеджер
        /// </summary>
        RadioButton isManagerRB;

        /// <summary>
        /// Радиокнопка: Специалист
        /// </summary>
        RadioButton isSpecialistRB;

        /// <summary>
        /// Радиокнопка: Обслуживающий персонал
        /// </summary>
        RadioButton isInternRB;
        #endregion

        #region ДАТЫ
        /// <summary>
        /// Поле даты: День рождения
        /// </summary>
        DatePicker birthDP;

       
        #endregion

       
        #endregion

        #region ИСТОЧНИКИ ДАННЫХ
       
        /// <summary>
        /// Доступные для выбора департаменты
        /// </summary>
        List<Department> departments;

        /// <summary>
        /// Доступные для выбора департаменты (свойство)
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
        /// Департамент
        /// </summary>
        Department department;

        /// <summary>
        /// Департамент (свойство)
        /// </summary>
        public Department Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
                NotifyPropertyChanged("Department");
            }
        }
        #endregion

        #region ПРОЧИЕ ПОЛЯ
        /// <summary>
        /// Вьюшка
        /// </summary>
        readonly Page page;

        /// <summary>
        /// Новый сотрудник
        /// </summary>
        readonly bool isNew;

        /// <summary>
        /// Сотрудник
        /// </summary>
        readonly Employee employee;

        /// <summary>
        /// рандомайзер
        /// </summary>
        readonly Random random = new Random();
        #endregion

        #region КОМАНДЫ
       
        /// <summary>
        /// Команда: Сохранить сотрудника
        /// </summary>
        RelayCommand saveEmployeeCommand;

        /// <summary>
        /// Свойство: Сохранить сотрудника
        /// </summary>
        public RelayCommand SaveEmployeeCommand
        {
            get
            {
                if (saveEmployeeCommand == null)
                {
                    saveEmployeeCommand = new RelayCommand(obj =>
                    {
                        //проверяем заполнение обязательных полей
                        if (CheckFillings())
                        {
                            //запрашиваем подтверждение
                            string MBText;
                            if (isNew) MBText = $"Вы точно хотите создать нового сотрудника?";
                            else MBText = $"Вы точно хотите отредактировать сотрудника?";
                            string MBCaption = "Требуется подтверждение";
                            MessageBoxResult result = MessageBox.Show(MBText, MBCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question);

                            //если подтверждение получено
                            if (result == MessageBoxResult.OK)
                            {
                                //объявляем нового сотрудника
                                Employee newEmployee;

                                //определяем класс нового сотрудника
                                if (isManagerRB.IsChecked == true) newEmployee = new Manager();
                                else if (isSpecialistRB.IsChecked == true) newEmployee = new Worker();
                                else newEmployee = new Intern();

                                //заполняем поля нового сотрудника
                                FillEmployeeFields(newEmployee);

                                //если создаем нового сотрудника
                                if (isNew) DataWorker.AddEmployee(newEmployee);
                                //если редактируем существующего сотрудника
                                else DataWorker.EditEmployee(newEmployee);

                                //выводим сообщение о результате
                                if (isNew) MessageBox.Show("Новый сотрудник создан");
                                else MessageBox.Show("Сотрудник успешно отредактирован");
                            }
                        }
                    });
                }
                return saveEmployeeCommand;
            }
        }

        /// <summary>
        /// Команда: Заполнить поля рандомно
        /// </summary>
        RelayCommand randomEmployeeCommand;

        /// <summary>
        /// Свойство: Заполнить поля рандомно
        /// </summary>
        public RelayCommand RandomEmployeeCommand
        {
            get
            {
                if (randomEmployeeCommand == null)
                {
                    randomEmployeeCommand = new RelayCommand(obj =>
                    {
                        //снимаем подсветку с полей
                        RemoveBacklight();
                        //Заполняем поля рандомными значениями
                        FillRandom();
                    });
                }
                return randomEmployeeCommand;
            }
        }
        #endregion

        #region МЕТОДЫ
        /// <summary>
        /// Заполняет обязательные поля рандомными значениями (кроме департамента, ставки и категории должности)
        /// </summary>
        private void FillRandom()
        {
            lastnameTB.Text = GenerateString("Иванов", "Петров", "Сидоров", "Мухоморов", "Коняев", "Афанасьев", "Самсонов", "Кузякин");
            firstnameTB.Text = GenerateString("Иван", "Владислав", "Кирилл", "Арсений", "Семён", "Матвей", "Александр", "Степан");
            birthDP.SelectedDate = GenerateDate();
        }

        /// <summary>
        /// Генерирует число, заданной длины
        /// </summary>
        private string Generateint(int v)
        {
            string str = "";
            for (int i = 0; i < v; i++)
            {
                str += random.Next(10);
            }
            return str;
        }

        /// <summary>
        /// Генерирует рандомную строку
        /// </summary>
        /// <returns></returns>
        private string GenerateString(params string[] parameters)
        {
            //возвращаем случайную строку
            return parameters[random.Next(parameters.Length)];
        }

        /// <summary>
        /// Генерирует рандомную дату
        /// </summary>
        /// <returns></returns>
        private DateTime GenerateDate()
        {
            var startDate = new DateTime(1985, 1, 1);
            Console.WriteLine(startDate.ToString("yyyy.dd.MM"));
            var newDate = startDate.AddDays(random.Next(9999));
            Console.WriteLine(newDate.ToString("yyyy.dd.MM"));
            return newDate;
        }

        /// <summary>
        /// Заполняет поля сотрудника данными из формы
        /// </summary>
        /// <param name="newEmployee"></param>
        private void FillEmployeeFields(Employee newEmployee)
        {
            //заполняем личные данные
            newEmployee.LastName = lastnameTB.Text;
            newEmployee.FirstName = firstnameTB.Text;
           
            newEmployee.DateOfBirth = (DateTime)birthDP.SelectedDate;
           
            //заполняем служебные данные
           
            if (isManagerRB.IsChecked == true)
            {
                newEmployee.Cathegory = Cathegory.Менеджер;
            }
            else if (isSpecialistRB.IsChecked == true)
            {
                newEmployee.Cathegory = Cathegory.Специалист;
                newEmployee.Rate = Convert.ToInt32(rateTB.Text);
            }
            else if (isInternRB.IsChecked == true)
            {
                newEmployee.Cathegory = Cathegory.Интерн;
                newEmployee.Rate = Convert.ToInt32(rateTB.Text);
            }
            newEmployee.DepartmentName = (departmentCB.SelectedItem as Department).Name;

        }

        /// <summary>
        /// Заполняет поля вьюшки и источники данных
        /// </summary>
        private void FillViewsAndSources()
        {
            Initialize();

            //заполняем текстбоксы
            lastnameTB.Text = employee.LastName;
            firstnameTB.Text = employee.FirstName;

            //заполняем радиокнопки: Категория должности (менеджер, специалист, персонал)
            if (employee.Cathegory == Cathegory.Менеджер) { isManagerRB.IsChecked = true; }
            else if (employee.Cathegory == Cathegory.Специалист) { isSpecialistRB.IsChecked = true; }
            else if (employee.Cathegory == Cathegory.Интерн) { isInternRB.IsChecked = true; }

            //заполняем даты
            birthDP.SelectedDate = employee.DateOfBirth;
           
            //текущий департамент
            department = Departments.Where(n => n.Name == employee.DepartmentName).FirstOrDefault();
        }

        /// <summary>
        /// инициализирует элементы и источники данных
        /// </summary>
        private void Initialize()
        {
            //текстблок
            messageTB = page.FindName("messageTB") as TextBlock;

            //кнопка
            debugBttn = page.FindName("debugBttn") as Button;

            //границы
            genderBorder = page.FindName("genderBorder") as Border;
            departmentBorder = page.FindName("departmentBorder") as Border;
            positionCathegoryBorder = page.FindName("positionCathegoryBorder") as Border;

            //текстбоксы
            lastnameTB = page.FindName("lastnameTB") as TextBox;
            firstnameTB = page.FindName("firstnameTB") as TextBox;
            rateTB = page.FindName("rateTB") as TextBox;
           
            //радиокнопки
            
            isManagerRB = page.FindName("isManagerRB") as RadioButton;
            isSpecialistRB = page.FindName("isSpecialistRB") as RadioButton;
            isInternRB = page.FindName("isInternRB") as RadioButton;

            //даты
            birthDP = page.FindName("birthDP") as DatePicker;
          
            //комбобокс
            departmentCB = page.FindName("departmentCB") as ComboBox;

            //источники данных
            departments = DataWorker.GetAllDepartments();
        }

        /// <summary>
        /// Проверяет заполнение обязательных полей
        /// </summary>
        /// <returns></returns>
        private bool CheckFillings()
        {
            //отключаем подсветку выделенных элементов
            RemoveBacklight();

            if (!CheckTextBoxContent(lastnameTB, messageTB, "Необходимо указать фамилию!")
                || !CheckTextBoxContent(firstnameTB, messageTB, "Необходимо указать имя!")
                || !CheckDatePicker(birthDP, messageTB, "Необходимо указать дату рождения!")
                || !CheckDepartmentSelection()
                || !CheckPositionCathegory()
                || !CheckRate()
                )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяет, правильно ли заполнено поле "ставка"
        /// </summary>
        /// <returns></returns>
        private bool CheckRate()
        {
            //если категория сотрудника - не менеджер
            if (isManagerRB.IsChecked != true)
            {
                //проверяем, заполнено ли поле
                if (!CheckTextBoxContent(rateTB, messageTB, "Необходимо указать ставку!"))
                {
                    return false;
                }
                //проверяем, являются ли символы, введенные пользователем, числом
                else if (!int.TryParse(rateTB.Text, out int num))
                {
                    rateTB.BorderBrush = Brushes.Red;
                    messageTB.Text = "Неправильно указана ставка! Допускаются только числа.";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет, выбран ли департамент
        /// </summary>
        /// <returns></returns>
        private bool CheckDepartmentSelection()
        {
            if (departmentCB.SelectedItem == null)
            {
                departmentBorder.BorderBrush = Brushes.Red;
                messageTB.Text = "Необходимо указать департамент!";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Отключает подсветку выделенных элементов
        /// </summary>
        private void RemoveBacklight()
        {
            //Убираем сообщение
            messageTB.Text = "";

            lastnameTB.BorderBrush = Brushes.Gray;
            firstnameTB.BorderBrush = Brushes.Gray;
            rateTB.BorderBrush = Brushes.Gray;
            departmentCB.BorderBrush = Brushes.Gray;
            birthDP.BorderBrush = Brushes.Gray;
            departmentBorder.BorderBrush = Brushes.Transparent;
            positionCathegoryBorder.BorderBrush = Brushes.Transparent;
        }

        /// <summary>
        /// Проверяет выбор категории должности
        /// </summary>
        /// <returns></returns>
        private bool CheckPositionCathegory()
        {
            if (isManagerRB.IsChecked == false && isSpecialistRB.IsChecked == false && isInternRB.IsChecked == false)
            {
                positionCathegoryBorder.BorderBrush = Brushes.Red;
                messageTB.Text = "Необходимо указать категорию должности!";
                return false;
            }
            return true;
        }

        #endregion
    }
}
