
namespace Theme12_OrganizationUI.Models
{
    class Worker : Employee
    {
        /// <summary>
        /// Производит расчет заработной платы и возвращает ее
        /// </summary>
        public override float GetWage()
        {
            return Rate * 1;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Worker() 
        {
        }
    }
}
