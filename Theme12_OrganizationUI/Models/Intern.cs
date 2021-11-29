using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme12_OrganizationUI.Models
{
    class Intern : Employee
    {
        /// <summary>
        /// Оклад
        /// </summary>
        new public uint Salary
        {
            get
            {
                return 500;    //500$ в месяц
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Age"></param>
        /// <param name="Salary"></param>
        /// <param name="Department"></param>
        /// <param name="ProjectsCount"></param>
        public Intern(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectsCount)
            : base(ID, FirstName, LastName, Age, Department, ProjectsCount)
        {
            this.salary = Salary;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Intern() : base()
        {
        }
    }
}
