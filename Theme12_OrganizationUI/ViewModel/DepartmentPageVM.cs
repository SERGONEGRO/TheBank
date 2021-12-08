using TheBank.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheBank.ViewModel
{
    class DepartmentPageVM : ViewModel
    {
        #region КОНСТРУКТОРЫ
        /// <summary>
        /// Конструктор (для показа инфы о существующем департаменте)
        /// </summary>
        /// <param name="department"></param>
        public DepartmentPageVM(Page page, Department department)
        {
            this.department = department;
            isNew = false;
            FillView(page);
        }

        /// <summary>
        /// Конструктор (для создания нового департамента)
        /// </summary>
        public DepartmentPageVM(Page page)
        {
            department = new Department();
            isNew = true;
            FillView(page);
            messageTB.Text = "Создание нового департамента. Заполните все обязательные (*) поля.";
        }
        #endregion

        #region ЭЛЕМЕНТЫ ВЬЮШКИ
        /// <summary>
        /// Текстблок: Сообщение 
        /// </summary>
        TextBlock messageTB;

        /// <summary>
        /// Текстбокс: Название департамента 
        /// </summary>
        TextBox DepartmentNameTB;

        /// <summary>
        /// Комбобокс: родительский департамент
        /// </summary>
        ComboBox ParentDepartmentCB;

        /// <summary>
        /// Кнопка "переименовать департамент"
        /// </summary>
        Button RenameDepartmentBttn;

        /// <summary>
        /// Кнопка "переместить департамент"
        /// </summary>
        Button ReplaceDepartmentBttn;

        /// <summary>
        /// Кнопка "сохранить департамент"
        /// </summary>
        Button SaveDepartmentBttn;
        #endregion

        #region ИСТОЧНИКИ ДАННЫХ
        /// <summary>
        /// Родительский департамент
        /// </summary>
        public Department ParentDepartment { get; set; }

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
        /// Дочерние департаменты
        /// </summary>
        public ObservableCollection<Department> ChildrenDepartments { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        #endregion

        #region ПРОЧИЕ ПОЛЯ
        /// <summary>
        /// Департамент
        /// </summary>
        readonly Department department;

        /// <summary>
        /// Новый департамент?
        /// </summary>
        readonly bool isNew;
        #endregion

        #region КОМАНДЫ
        /// <summary>
        /// Сохранить департамент (команда)
        /// </summary>
        RelayCommand saveDepartmentCommand;

        /// <summary>
        /// Сохранить департамент (свойство)
        /// </summary>
        public RelayCommand SaveDepartmentCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (saveDepartmentCommand == null)
                {
                    saveDepartmentCommand = new RelayCommand(obj =>
                    {
                        //отключаем подсветку
                        TurnOffBacklight();
                        //проверяем заполнение необходимых полей
                        if (CheckTextBoxContent(DepartmentNameTB, messageTB, "Название не может быть пустым!")
                       && CheckComboBox(ParentDepartmentCB, messageTB, "Укажите родительский департамент!"))
                        {
                            //запрашиваем подтверждение
                            string MBText = $"Вы точно хотите сохранить изменения?";
                            string MBCaption = "Требуется подтверждение";
                            MessageBoxResult result = MessageBox.Show(MBText, MBCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                            //если пользователь подтвердил
                            if (result == MessageBoxResult.OK)
                            {
                                //пытаемся добавить новый департамент
                                if (!DataWorker.AddDepartment((ParentDepartmentCB.SelectedItem as Department).Name, DepartmentNameTB.Text))
                                {
                                    ShowWarning(messageTB, "Департамент с таким названием уже существует!");
                                }
                            }
                        }
                    }
                );
                }
                return saveDepartmentCommand;
            }
        }

        /// <summary>
        /// Переместить департамент (команда)
        /// </summary>
        RelayCommand replaceDepartmentCommand;

        /// <summary>
        /// Переместить департамент (свойство)
        /// </summary>
        public RelayCommand ReplaceDepartmentCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (replaceDepartmentCommand == null)
                {
                    replaceDepartmentCommand = new RelayCommand(obj =>
                    {
                        //если родительский департамент изменился
                        if (ParentDepartmentCB.SelectedItem != null && department.ParentName != (ParentDepartmentCB.SelectedItem as Department).Name)
                        {
                            //запрашиваем подтверждение
                            string MBText = $"Вы точно хотите переместить департамент?";
                            string MBCaption = "Требуется подтверждение";
                            MessageBoxResult result = MessageBox.Show(MBText, MBCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                            //если пользователь подтвердил
                            if (result == MessageBoxResult.OK)
                            {
                                DataWorker.ReplaceDepartment(department.Name, (ParentDepartmentCB.SelectedItem as Department).Name);
                                MessageBox.Show("Департамент успешно перемещен.");
                            }
                        }
                    }
                    );
                }
                return replaceDepartmentCommand;

            }
        }

        /// <summary>
        /// Переименовать департамент (команда)
        /// </summary>
        RelayCommand renameDepartmentCommand;

        /// <summary>
        /// Переименовать департамент (свойство)
        /// </summary>
        public RelayCommand RenameDepartmentCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (renameDepartmentCommand == null)
                {
                    renameDepartmentCommand = new RelayCommand(obj =>
                    {
                        //если название департамента изменилось
                        if (department.Name != DepartmentNameTB.Text)
                        {
                            //запрашиваем подтверждение
                            string MBText = $"Вы точно хотите переименовать департамент?";
                            string MBCaption = "Требуется подтверждение";
                            MessageBoxResult result = MessageBox.Show(MBText, MBCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                            //если пользователь подтвердил
                            if (result == MessageBoxResult.OK)
                            {
                                //Пытаемся переименовать департамент
                                if (!DataWorker.RenameDepartment(department.Name, DepartmentNameTB.Text))
                                {
                                    ShowWarning(messageTB, "Департамент с таким названием уже существует!");
                                }
                                else
                                {
                                    MessageBox.Show("Департамент успешно переименован.");
                                }
                            }
                        }
                    });
                }
                return renameDepartmentCommand;
            }
        }
        #endregion

        #region МЕТОДЫ   
        /// <summary>
        /// Заполняет вьюшку
        /// </summary>
        /// <param name="page"></param>
        private void FillView(Page page)
        {
            //находим элементы вьюшки
            messageTB = page.FindName("messageTB") as TextBlock;
            DepartmentNameTB = page.FindName("DepartmentNameTB") as TextBox;
            ParentDepartmentCB = page.FindName("ParentDepartmentCB") as ComboBox;
            RenameDepartmentBttn = page.FindName("RenameDepartmentBttn") as Button;
            SaveDepartmentBttn = page.FindName("SaveDepartmentBttn") as Button;
            ReplaceDepartmentBttn = page.FindName("ReplaceDepartmentBttn") as Button;

            //отображаем необходимую информацию
            DepartmentNameTB.Text = department.Name;

            //определяем источники данных
            ChildrenDepartments = department.Departments;
            Employees = department.Employees;
            SelectAvailableDepartments();
            ParentDepartment = Departments.Where(n => n.Name == department.ParentName).FirstOrDefault();

            //управляем видимостью кнопок
            if (isNew)
            {
                RenameDepartmentBttn.Visibility = Visibility.Hidden;
                ReplaceDepartmentBttn.Visibility = Visibility.Hidden;
                SaveDepartmentBttn.Visibility = Visibility.Visible;
            }
            else
            {
                RenameDepartmentBttn.Visibility = Visibility.Visible;
                ReplaceDepartmentBttn.Visibility = Visibility.Visible;
                SaveDepartmentBttn.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Выбирает доступные родительские департаменты
        /// </summary>
        private void SelectAvailableDepartments()
        {
            //берем из базы все департаменты
            departments = DataWorker.GetAllDepartments();
            //если мы не создаем новый департамент, а редактируем существующий, то его нельзя переместить в себя и в любого из его потомков
            if (isNew == false)
            {
                //удаляем из выпадающего списка всех потомков и сам департамент
                RemoveDescendants(department);
            }
        }

        /// <summary>
        /// Убираем из выпадающего списка все недопустимые департаменты
        /// </summary>
        private void RemoveDescendants(Department department)
        {
            //удаляем потомков
            foreach (var item in department.Departments)
            {
                RemoveDescendants(item);
            }

            //удаляем сам департамент
            Departments.Remove(Departments.Where(n => n.Name == department.Name).FirstOrDefault());
        }

        /// <summary>
        /// Снимает подсветку с элементов вьюшки
        /// </summary>
        private void TurnOffBacklight()
        {
            DepartmentNameTB.BorderBrush = Brushes.LightGray;
            ParentDepartmentCB.BorderBrush = Brushes.LightGray;
            messageTB.Foreground = Brushes.Black;
            messageTB.Text = "";
        }
        #endregion
    }
}
