using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme12_OrganizationUI.Models
{

    #region ПЕРЕЧИСЛЕНИЯ
   
    /// <summary>
    /// Категория должности
    /// </summary>
    public enum Cathegory
    {
        Менеджер,
        Специалист,
        Интерн
    }
    #endregion

    public abstract class Employee
    {
        #region Свойства
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

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
        { get
            {
                if (DateTime.Now <= DateOfBirth)
                    return 0;
                int n = DateTime.Now.Year - DateOfBirth.Year;
                if (DateOfBirth.DayOfYear > DateTime.Now.DayOfYear)
                    --n;
                if (n > 99) return 99;
                return (byte) n;
            }
        }

        /// <summary>
        /// Отдел
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Категория должности
        /// </summary>
        public Cathegory Cathegory { get; set; }

        /// <summary>
        /// Ставка
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Количество проектов
        /// </summary>
        //public byte ProjectsCount { get; set; }

        /// <summary>
        /// Производит расчет заработной платы и возвращает ее
        /// </summary>
        /// <returns></returns>
        public abstract float GetWage();
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        #endregion
    }
}
