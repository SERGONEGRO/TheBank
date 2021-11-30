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
        public byte Age { get; set; }

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
        public byte ProjectsCount { get; set; }

        /// <summary>
        /// Производит расчет заработной платы и возвращает ее
        /// </summary>
        /// <returns></returns>
        public abstract float GetWage();



        #endregion

        #region Методы

       

        /// <summary>
        /// Employee в JSON
        /// </summary>
        /// <returns></returns>
        public JObject SerializeEmployeeToJson()
        {
            JObject jEmployee = new JObject
            {
                ["ID"] = Id,
                ["FirstName"] = FirstName,
                ["LastName"] = LastName,
                ["Age"] = Age,
                ["Salary"] = GetWage(),
                ["Department"] = DepartmentName,
                ["ProjectCount"] = ProjectsCount,
                ["Cathegory"] = Cathegory.ToString()
            };
            return jEmployee;
        }

        #endregion




    }
}
