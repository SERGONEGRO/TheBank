using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme12_OrganizationUI.Models
{
    public abstract class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public byte Age
        {
            get
            {
                if (DateTime.Now <= DateOfBirth)
                    return 0;
                int n = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth.DayOfYear > DateTime.Now.DayOfYear)
                    --n;
                if (n > 99) return 99;
                return (byte)n;
            }
        }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

    }
}
