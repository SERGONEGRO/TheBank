using TheBank.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace TheBank.Models
{
    class DataWorker
    {
        //Событие "в БД что-то поменялось"
        public static event Action DBChanged;

        #region МЕТОДЫ, ВОЗВРАЩАЮЩИЕ ДАННЫЕ
        /// <summary>
        /// Возвращает дерево департаментов
        /// </summary>
        public static ObservableCollection<Department> GetDepartmentsTree()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //возвращаем дерево департаментов
                return db.DepartmentsTree;
            }
        }

        /// <summary>
        /// Возвращает все существующие департаменты списком
        /// </summary>
        /// <returns></returns>
        internal static List<Department> GetAllDepartments()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.DepartmentsTree[0].GetDepartments();
            }
        }

        /// <summary>
        /// Возвращает список всех сотрудников
        /// </summary>
        /// <returns></returns>
        internal static ObservableCollection<Employee> GetEmployees()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //возвращаем список сотрудников
                return db.DepartmentsTree[0].GetEmployees();
            }
        }

        /// <summary>
        /// Возвращает зарплатные ведомости на всех сотрудников
        /// </summary>
        /// <returns></returns>
        internal static ObservableCollection<Payroll> GetPayrolls()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {

                ObservableCollection<Payroll> payrolls = new ObservableCollection<Payroll>();

                foreach (var item in db.DepartmentsTree[0].GetEmployees())
                {
                    payrolls.Add(new Payroll(item.Id, item.LastName, item.FirstName, item.DepartmentName,
                        item.Cathegory.ToString(), item.GetWage()));
                }

                return payrolls;
            }
        }

        /// <summary>
        /// Возвращает коллекцию всеx Интернов (сотрудников с почасовой оплатой)
        /// </summary>
        /// <returns></returns>
        internal static ObservableCollection<EditableStaff> GetStaffs()
        {
            ObservableCollection<EditableStaff> staffs = new ObservableCollection<EditableStaff>();

            foreach (var item in GetEmployees())
            {
                if (item.Cathegory == Cathegory.Интерн)
                {
                    staffs.Add(new EditableStaff(item.LastName, item.FirstName, item.DepartmentName,item.Cathegory.ToString(), item.Hours, item.Id));
                }
            }
            return staffs;
        }

        /// <summary>
        /// Возвращает запрошенный департамент (находит по имени)
        /// </summary>
        /// <param name="departmentName"></param>
        internal static Department GetDepartment(string departmentName)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.DepartmentsTree[0].GetDepartment(departmentName);
            }
        }

        /// <summary>
        /// Находит и возвращает незанятый табельный номер для сотрудника
        /// </summary>
        /// <returns></returns>
        internal static int GetFreeNumberEmployee()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //получаем список сотрудников
                ObservableCollection<Employee> employees = db.DepartmentsTree[0].GetEmployees();
                //если список пуст, возвращаем единичку
                
                if (employees.Count == 0)
                {
                    return 1;
                }
                //иначе 
                else
                {
                    //сортируем его по табельному номеру
                    var sortedEmployees = from u in employees
                                          orderby u.Id
                                          select u;
                    //возвращаем последний номер +1
                    return sortedEmployees.Last().Id + 1;
                }
            }
        }

        /// <summary>
        /// Находит и возвращает незанятый табельный номер для клиента
        /// </summary>
        /// <returns></returns>
        internal static int GetFreeNumberClient()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //получаем список сотрудников
                ObservableCollection<Client> clients = GetAllClients();
                //если список пуст, возвращаем единичку

                if (clients.Count == 0)
                {
                    return 1;
                }
                //иначе 
                else
                {
                    //сортируем его по табельному номеру
                    var sortedClients = from u in clients
                                          orderby u.Id
                                          select u;
                    //возвращаем последний номер +1
                    return sortedClients.Last().Id + 1;
                }
            }
        }

        /// <summary>
        /// Возвращает всех клиентов
        /// </summary>
        /// <returns></returns>
        internal static ObservableCollection<Client> GetAllClients()
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                ObservableCollection<Client> clients = new ObservableCollection<Client>();
                foreach (var item in db.ClientsList)
                {
                    clients.Add(item);
                }
                return clients;
            }
        }

        #endregion

        #region  МЕТОДЫ РЕДАКТИРОВАНИЯ
        /// <summary>
        /// Переименовывает указанный департамент
        /// </summary>
        /// <returns></returns>
        internal static bool RenameDepartment(string oldName, string newName)
        {
            //проверка имени на уникальность
            if (DepartmentNameIsUnique(newName))
            {
                //обращаемся к бд
                using (ApplicationContext db = new ApplicationContext())
                {
                    //находим нужный департамент
                    Department department = db.DepartmentsTree[0].GetDepartment(oldName);
                    //переименовываем его
                    department.Name = newName;
                    //меняем название родительского департамента у всех его сотрудников 
                    foreach (var item in department.Employees)
                    {
                        item.DepartmentName = newName;
                    }
                    //сохраняем БД
                    db.Save();
                    //инициируем событие "БД изменилась"
                    DBChanged();

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Перемещает указанный департамент 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        internal static void ReplaceDepartment(string departmentName, string newParentName)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим департамент 
                Department department = db.DepartmentsTree[0].GetDepartment(departmentName);
                //находим старый родительский департамент
                Department parent = db.DepartmentsTree[0].GetDepartment(department.ParentName);
                //удаляем из него департамент
                parent.Departments.Remove(department);
                //находим  новый родительский департамент
                Department newParent = db.DepartmentsTree[0].GetDepartment(newParentName);
                //помещаем в него департамент
                newParent.Departments.Add(department);
                //меняем запись о родителе в департаменте
                department.ParentName = newParent.Name;
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }

        /// <summary>
        /// Сохраняет изменения в отработанных часах сотрудника
        /// </summary>
        /// <param name="personnelNumber"></param>
        /// <param name="editableHours"></param>
        internal static void EditStaffHours(int personnelNumber, int editableHours)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим сотрудника
                Employee employee = db.DepartmentsTree[0].GetEmployee(personnelNumber);
                //сохраняем нового сотрудника на место старого
                employee.Hours = editableHours;
                //сохраняем БД
                db.Save();
            }
        }

        /// <summary>
        /// Переводит сотрудника в другой департамент
        /// </summary>
        internal static void ReplaceEmployee(Employee employee, string newDepartmentName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим старый родительский департамент
                Department oldDepartment = db.DepartmentsTree[0].GetDepartment(employee.DepartmentName);
                //удаляем из него сотрудника
                oldDepartment.RemoveEmployee(employee);
                //находим новый департамент 
                Department newDepartment = db.DepartmentsTree[0].GetDepartment(newDepartmentName);
                //добавляем в него сотрудника
                newDepartment.AddEmployee(employee);
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }

        /// <summary>
        /// Редактирует сотрудника
        /// </summary>
        /// <param name="newEmployee"></param>
        internal static void EditEmployee(Employee newEmployee)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим сотрудника
                Employee employee = db.DepartmentsTree[0].GetEmployee(newEmployee.Id);
                //находим родительский департамент
                Department parent = db.DepartmentsTree[0].GetDepartment(newEmployee.DepartmentName);

                //удаляем сотрудника
                parent.RemoveEmployee(employee);
                //добавляем в него сотрудника
                parent.AddEmployee(newEmployee);

                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }
        /// <summary>
        /// Редактирует клиента
        /// </summary>
        /// <param name="newEmployee"></param>
        internal static void EditClient(Client newClient)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим клиента
                Client client = db.ClientsList.Find(x => x.Id == newClient.Id);
                //удаляем клиента
                db.ClientsList.Remove(client);
                //добавляем в него клиента
                db.ClientsList.Add(newClient);
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }
        #endregion

        #region МЕТОДЫ ДОБАВЛЕНИЯ
        /// <summary>
        /// Пытается добавить новый департамент
        /// </summary>
        /// <returns></returns>
        internal static bool AddDepartment(string parentName, string newDepartmentName)
        {
            //проверка имени на уникальность
            if (DepartmentNameIsUnique(newDepartmentName))
            {
                //обращаемся к бд
                using (ApplicationContext db = new ApplicationContext())
                {
                    //находим родителя
                    Department department = db.DepartmentsTree[0].GetDepartment(parentName);
                    //добавляем новый департамент
                    department.Departments.Add(new Department(newDepartmentName, parentName));
                    //сохраняем БД
                    db.Save();

                    //инициируем событие "БД изменилась"
                    DBChanged();

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Добавляет нового сотрудника
        /// </summary>
        /// <param name="newEmployee"></param>
        internal static void AddEmployee(Employee newEmployee)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим родительский департамент
                Department parent = db.DepartmentsTree[0].GetDepartment(newEmployee.DepartmentName);
                //назначаем сотруднику персональный номер
                newEmployee.Id = GetFreeNumberEmployee();
                //добавляем в него сотрудника
                parent.AddEmployee(newEmployee);
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }

        /// <summary>
        /// Добавляет нового клиента
        /// </summary>
        /// <param name="newEmployee"></param>
        internal static void AddClient(Client newClient)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //назначаем сотруднику персональный номер
                newClient.Id = GetFreeNumberClient();
                //добавляем в него сотрудника
                db.ClientsList.Add(newClient);
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }
        #endregion

        #region МЕТОДЫ ПРОВЕРКИ
        /// <summary>
        /// Проверяет название имени департамента на уникальность
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static bool DepartmentNameIsUnique(string name)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                ////пытаемся найти департамент с таким именем используя linq
                bool checkIsExist = db.DepartmentsTree.Any(el => el.Name == name);
                return !checkIsExist;
                ////пытаемся найти департамент с таким именем 
                //if (db.DepartmentsTree[0].GetDepartment(name) != null)
                //{
                //    return false;
                //}
                //return true;
            }
        }

        #endregion

        #region МЕТОДЫ УДАЛЕНИЯ
        /// <summary>
        /// Удаляет указанный департамент
        /// </summary>
        /// <param name="name"></param>
        internal static void DeleteDepartment(Department department)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим родительский департамент
                Department parent = db.DepartmentsTree[0].GetDepartment(department.ParentName);
                //удаляем из него нужный
                parent.Departments.Remove(parent.Departments.Where(n => n.Name == department.Name).FirstOrDefault());
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
            }
        }

        /// <summary>
        /// Удаляет указанного сотрудника
        /// </summary>
        /// <returns></returns>
        internal static bool RemoveEmployee(Employee employee)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                //находим родительский департамент
                Department parent = db.DepartmentsTree[0].GetDepartment(employee.DepartmentName);
                //удаляем из него сотрудника
                parent.RemoveEmployee(employee);
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
                return true;
            }
        }

        /// <summary>
        /// Удаляет указанного клиента
        /// </summary>
        /// <returns></returns>
        internal static bool RemoveClient(Client client)
        {
            //обращаемся к бд
            using (ApplicationContext db = new ApplicationContext())
            {
                db.ClientsList.Remove(client);
                //сохраняем БД
                db.Save();
                //инициируем событие "БД изменилась"
                DBChanged();
                return true;
            }
        }
        #endregion
    }
}
