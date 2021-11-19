using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme12_OrganizationUI
{
    abstract class Employee
    {
        #region Поля

        /// <summary>
        /// Уникальный номер
        /// </summary>
        protected private uint id;

        /// <summary>
        /// Поле "Имя"
        /// </summary>
        protected private string firstName;

        /// <summary>
        /// Поле "Фамилия"
        /// </summary>
        protected private string lastName;

        /// <summary>
        /// Возраст
        /// </summary>
        protected private byte age;

        /// <summary>
        /// Поле "Отдел"
        /// </summary>
        protected private string department;

        /// <summary>
        /// Поле "Оплата труда"
        /// </summary>
        protected private uint salary;

        /// <summary>
        /// Количество закрепленных проектов
        /// </summary>
        protected private byte projectsCount;

        #endregion

        #region Свойства

        public uint Id { get { return this.id; } }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }

        /// <summary>
        /// Возраст
        /// </summary>
        public byte Age { get { return this.age; } set { this.age = value; } }

        /// <summary>
        /// Отдел
        /// </summary>
        public string Department { get { return this.department; } set { this.department = value; } }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public virtual uint Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Количество проектов
        /// </summary>
        public byte ProjectsCount { get { return this.projectsCount; } set { this.projectsCount = value; } }



        #endregion

        #region Конструкторы

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Age"></param>
        /// <param name="Salary"></param>
        /// <param name="Department"></param>
        /// <param name="ProjectsCount"></param>
        public Employee(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectsCount)
        {
            this.id = ID;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.age = Age;
            this.department = Department;
            this.projectsCount = ProjectsCount;
            this.salary = Salary;
        }

        /// <summary>
        /// создание сотрудника с автопараметрами
        /// </summary>
        public Employee() : this(1, "", "", 1, "", 0)
        {
        }

        #endregion

        #region Методы

        /// <summary>
        /// печать в консоль
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{this.id,5} {this.firstName,10} {this.lastName,20} {this.age,5} {this.department,20}  {this.salary,10} {this.projectsCount,8} {this.GetType().Name,10}";
        }


        /// <summary>
        /// Employee в JSON
        /// </summary>
        /// <returns></returns>
        public JObject SerializeEmployeeToJson()
        {
            JObject jEmployee = new JObject
            {
                ["ID"] = this.Id,
                ["FirstName"] = this.FirstName,
                ["LastName"] = this.LastName,
                ["Age"] = this.Age,
                ["Salary"] = this.Salary,
                ["Department"] = this.Department,
                ["ProjectCount"] = this.ProjectsCount,
                ["TYPE"] = this.GetType().Name
            };
            return jEmployee;
        }

        #endregion




    }
}
