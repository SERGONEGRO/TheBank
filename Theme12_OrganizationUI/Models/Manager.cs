using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme12_OrganizationUI.Models
{

    class Manager : Employee
    {
        /// <summary>
        /// Оплата = 15% от всех, но не меньше 1300
        /// </summary
        new public double Salary
        {
            get
            {
                return salary;
            }

            set { this.salary = (uint)value; }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Age"></param>
        /// <param name="Department"></param>
        /// <param name="ProjectsCount"></param>
        /// <param name=""></param>
        public Manager(uint ID, string FirstName, string LastName, byte Age, string Department, byte ProjectCount)
            : base(ID, FirstName, LastName, Age, Department, ProjectCount)
        {
        }

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public Manager() : base()
        {
        }
    }
}
