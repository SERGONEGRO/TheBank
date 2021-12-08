using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TheBank.Models
{
    /// <summary>
    /// Департамент
    /// </summary>
    public class Department
    {
        #region КОНСТРУКТОРЫ
        /// <summary>
        /// Дефолтный конструктор
        /// </summary>
        public Department()
        {
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();
        }

        /// <summary>
        /// Кастомный конструктор для создания корневого департамента (без родителя)
        /// </summary>
        public Department(string name)
        {
            Name = name;
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();
        }
        /// <summary>
        /// Кастомный конструктор для создания обычного департамента (всегда имеет родителя)
        /// </summary>
        public Department(string name, string parentName)
        {
            Name = name;
            ParentName = parentName;
            Departments = new ObservableCollection<Department>();
            Employees = new ObservableCollection<Employee>();
        }

        #endregion

        #region СВОЙСТВА
        /// <summary>
        /// Наименование департамента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Родительский департамент
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// Дочерние департаменты
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        #endregion

        #region МЕТОДЫ 
        /// <summary>
        /// Проверяет, есть ли в департаменте дочерние департаменты и/или сотрудники
        /// </summary>
        /// <returns></returns>
        internal bool HasChildren()
        {
            if (Departments.Count > 0 || Employees.Count > 0)
            {
                return true;
            }
            return false;
        }

        #region МЕТОДЫ ДЛЯ РАБОТЫ С ДЕПАРТАМЕНТАМИ
        /// <summary>
        /// Возвращает список всех департаментов-потомков и текущего
        /// </summary>
        /// <returns></returns>
        internal List<Department> GetDepartments()
        {
            //создаем новый список
            List<Department> list = new List<Department>();
            //пробегаемся по всем дочерним департаментам
            foreach (var item in Departments)
            {
                //собираем из них дочерние департаменты и добавляем их в созданный список
                list.AddRange(item.GetDepartments());
            }
            //добавляем в список сам департамент
            list.Add(this);
            //возвращаем список
            return list;
        }

        /// <summary>
        /// Ищет департамент среди наследников и возвращает его
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        internal Department GetDepartment(string departmentName)
        {
            //сначала, проверяем сам департамент
            if (Name == departmentName)
            {
                return this;
            }
            else
            {
                foreach (var item in Departments)
                {
                    if (item.GetDepartment(departmentName) != null)
                    {
                        return item.GetDepartment(departmentName);
                    }
                }
                return null;
            }
        }

        #endregion

        #region МЕТОДЫ ДЛЯ РАБОТЫ С ЗАРПЛАТАМИ
        /// <summary>
        /// Возвращает сумму всех з/п сотрудников дочерних департаментов
        /// </summary>
        /// <returns></returns>
        internal float GetChildrenWages()
        {
            float wages = 0;
            foreach (var item in Departments)
            {
                wages += item.GetWages();
                wages += item.GetChildrenWages();
            }
            return wages;
        }

        /// <summary>
        /// Возвращает сумму всех з/п сотрудников этого департамента, включая менеджера
        /// </summary>
        /// <returns></returns>
        internal float GetWages()
        {
            float wages = GetSSWages();

            //перебираем всех сотрудников (кроме менеджера)
            foreach (var item in Employees)
            {
                //если это менеджер
                if (item.Cathegory == Cathegory.Менеджер)
                {
                    //прибавляем его з/п к общей
                    wages += item.GetWage();
                }
            }

            return wages;
        }

        /// <summary>
        /// Возвращает сумму всех з/п сотрудников этого департамента, кроме менеджера
        /// </summary>
        /// <returns></returns>
        internal float GetSSWages()
        {
            float wages = 0;

            //перебираем всех сотрудников (кроме менеджера)
            foreach (var item in Employees)
            {
                //если это не менеджер
                if (item.Cathegory != Cathegory.Менеджер)
                {
                    //прибавляем его з/п к общей
                    wages += item.GetWage();
                }
            }

            return wages;
        }
        #endregion

        #region МЕТОДЫ ДЛЯ РАБОТЫ С СОТРУДНИКАМИ
        /// <summary>
        /// Возвращает коллекцию всех сотрудников департамента и его наследников
        /// </summary>
        /// <returns></returns>
        internal ObservableCollection<Employee> GetEmployees()
        {
            //создаем коллекцию
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            //помещаем в нее всех сотрудников самого департамента
            foreach (var item in Employees)
            {
                employees.Add(item);
            }
            //помещаем в нее всех сотрудников из департаментов-потомков
            foreach (var item in Departments)
            {
                foreach (var n in item.GetEmployees())
                {
                    employees.Add(n);
                }
            }

            //возвращаем коллекцию
            return employees;
        }

        /// <summary>
        /// Ищет сотрудника по персональному номеру и возвращает его
        /// </summary>
        /// <param name="personnelNumber"></param>
        /// <returns></returns>
        internal Employee GetEmployee(int personnelNumber)
        {
            //сначала ищем среди сотрудников самого департамента
            if (SearchInEmployees(personnelNumber) != null)
            {
                return SearchInEmployees(personnelNumber);
            }
            //если не нашли, ищем в департаментах-потомках
            else
            {
                foreach (var item in Departments)
                {
                    if (item.GetEmployee(personnelNumber) != null)
                    {
                        return item.GetEmployee(personnelNumber);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Ищем сотрудника среди сотрудников этого департамента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Employee SearchInEmployees(int id)
        {
            //проверяем всех сотрудников
            foreach (var item in Employees)
            {
                //если нашли нужного
                if (item.Id == id)
                {
                    return item;
                }
            }
            //если никого не нашли
            return null;
        }

        /// <summary>
        /// Удаляет сотрудника
        /// </summary>
        /// <param name="employee"></param>
        internal void RemoveEmployee(Employee employee)
        {
            Employees.Remove(Employees.Where(n => n.Id == employee.Id).FirstOrDefault());
        }

        /// <summary>
        /// Добавляет сотрудника
        /// </summary>
        internal void AddEmployee(Employee employee)
        {
            Employee newEmployee = employee;
            newEmployee.DepartmentName = Name;
            Employees.Add(newEmployee);
        }
        #endregion

        #endregion

    }
}
