using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBank.Models
{

    class Manager : Employee
    {
        /// <summary>
        /// Производит расчет заработной платы и возвращает ее
        /// </summary>
        /// <returns></returns>
        public override float GetWage()
        {
            //находим родительский департамент
            Department parent = DataWorker.GetDepartment(DepartmentName);
            //получаем из него сумму всех з/п подчиненных сотрудников (т.е. всех, кроме менежера)
            //получаем из него сумму всех з/п сотрудников в дочерних департаментах
            //складываем эти цифры
            //высчитываем 15% от этой суммы
            float managerWage = (parent.GetSSWages() + parent.GetChildrenWages()) / 100 * 15;
            //проверяем, превышает ли полученная цифра минимальную з/п менеджера
            if (managerWage >= 1300)
            {
                return managerWage;
            }
            else
            {
                return 1300;
            }
        }

        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public Manager() : base()
        {
        }
    }
}
