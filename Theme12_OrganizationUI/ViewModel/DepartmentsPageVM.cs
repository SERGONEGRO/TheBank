using TheBank.Models;
using TheBank.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TheBank.ViewModel
{
    class DepartmentsPageVM : ViewModel
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DepartmentsPageVM(Page view)
        {
            //инициализируем все
            View = view;
            departmentsTW = View.FindName("departmentsTW") as TreeView;

            //Обновляем источник данных для дерева департаментов
            departmentsTree = DataWorker.GetDepartmentsTree();

            //подписываемся на событие "смена выделенного элемента дерева"
            departmentsTW.SelectedItemChanged += ShowSElectedDepartment;

            //подписываемся на событие "БД обновилась"
            DataWorker.DBChanged += UpdateDepartments;

            //создаем экземпляр страницы с информацией о корневом департаменте
            DepartmentPage page = new DepartmentPage(DepartmentsTree[0]);

            //показываем ее во фрейме
            currentPage = page;
        }

        /// <summary>
        /// Обновляем отображение дерева департаментов
        /// </summary>
        /// <returns></returns>
        private void UpdateDepartments()
        {
            //брем новое дерево из БД
            DepartmentsTree = DataWorker.GetDepartmentsTree();
            //во фрейме справа показываем корневой департамент
            CurrentPage = new DepartmentPage(DepartmentsTree[0]);
        }

        /// <summary>
        /// Вьюшка
        /// </summary>
        public Page View { get; set; }

        /// <summary>
        /// Дерево "департаменты"
        /// </summary>
        TreeView departmentsTW;

        /// <summary>
        /// Текущая страница, отображаемая во фрейме
        /// </summary>
        Page currentPage;

        /// <summary>
        /// Текущая страница, отображаемая во фрейме (свойство)
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
        /// Дерево департаментов
        /// </summary>
        ObservableCollection<Department> departmentsTree;

        /// <summary>
        /// Дерево департаментов (свойство)
        /// </summary>
        public ObservableCollection<Department> DepartmentsTree
        {
            get
            {
                return departmentsTree;
            }
            set
            {
                departmentsTree = value;
                NotifyPropertyChanged("DepartmentsTree");
            }
        }

        /// <summary>
        /// Вызывается при смене выделенного элемента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowSElectedDepartment(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //Получаем выделенный департамент
            Department department = departmentsTW.SelectedItem as Department;
            //создаем новую страничку с инфой о департаменте
            DepartmentPage page = new DepartmentPage(department);
            //показываем ее во фрейме
            CurrentPage = page;
        }

        /// <summary>
        /// Команда: Добавить департамент
        /// </summary>
        RelayCommand addDepartmentCommand;

        /// <summary>
        /// Свойство: Добавить департамент
        /// </summary>
        public RelayCommand AddDepartmentCommand
        {
            get
            {
                if (addDepartmentCommand == null)
                {
                    addDepartmentCommand = new RelayCommand(obj =>
                    {
                        //Получаем имя родительского департамента
                        string parentName;
                        //если есть выбранный элемент - то он и будет родительским
                        if (departmentsTW.SelectedItem != null)
                        {
                            parentName = (departmentsTW.SelectedItem as Department).Name;
                        }
                        //если нет выбранного элемента, то родительским будет корневой
                        else
                        {
                            parentName = departmentsTree[0].Name;
                        }
                        //создаем новую страничку с инфой о пустом пока департаменте
                        DepartmentPage page = new DepartmentPage(parentName);
                        //показываем ее во фрейме
                        CurrentPage = page;
                    });
                }
                return addDepartmentCommand;
            }
        }

        /// <summary>
        /// Команда: Удалить департамент
        /// </summary>
        RelayCommand deleteDepartmentCommand;

        /// <summary>
        /// Свойство: Удалить департамент
        /// </summary>
        public RelayCommand DeleteDepartmentCommand
        {
            get
            {
                if (deleteDepartmentCommand == null)
                {
                    deleteDepartmentCommand = new RelayCommand(obj =>
                    {
                        if (departmentsTW.SelectedItem != null)
                        {
                            //Получаем выделенный департамент
                            Department department = departmentsTW.SelectedItem as Department;
                            //проверяем, не является ли департамент корневым
                            if (string.IsNullOrEmpty(department.ParentName))
                            {
                                string MBText = "Нельзя удалить корневой департамент!";
                                string MBCaption = "Ошибка удаления!";
                                MessageBox.Show(MBText, MBCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            //проверяем, содержит ли департамент вложенные департаменты и/или сотрудников
                            else if (department.HasChildren())
                            {
                                string MBText = "Нельзя удалить департамент, содержащий другие департаменты или сотрудников!";
                                string MBCaption = "Ошибка удаления!";
                                MessageBox.Show(MBText, MBCaption, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                string MBText = $"Вы точно хотите удалить {department.Name}?";
                                string MBCaption = "Требуется подтверждение";
                                MessageBoxResult result = MessageBox.Show(MBText, MBCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                                if (result == MessageBoxResult.OK)
                                {
                                    DataWorker.DeleteDepartment(department);
                                }
                            }
                        }
                    });
                }
                return deleteDepartmentCommand;
            }
        }
    }
}
