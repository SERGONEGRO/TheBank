using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBank.Models
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

    public abstract class Employee:Person
    {
        #region Свойства
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

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
        /// Производит расчет заработной платы и возвращает ее
        /// </summary>
        /// <returns></returns>
        public abstract float GetWage();
       

        #endregion
    }
}
