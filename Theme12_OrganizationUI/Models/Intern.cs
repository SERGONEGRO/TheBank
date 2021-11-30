
namespace Theme12_OrganizationUI.Models
{
    class Intern : Employee
    {
        /// Производит расчет заработной платы и возвращает ее
        public override float GetWage()
        {
            return Rate * Hours;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Intern() : base()
        {
        }
    }
}
