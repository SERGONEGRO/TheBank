namespace Theme12_OrganizationUI.Models
{
    class Payroll
    {
        public Payroll(int personnelNumber, string surename, string name, string departmentName, string cathegory, float wage)
        {
            PersonnelNumber = personnelNumber;
            Fullname = $"{surename} {name} ";
            DepartmentName = departmentName;
            Cathegory = cathegory;
            Wage = wage;
        }

        /// <summary>
        /// Табельный номер сотрудника
        /// </summary>
        public int PersonnelNumber { get; set; }

        /// <summary>
        /// Фамилия, имя, отчество (если есть)
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Департамент
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Cathegory { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        public float Wage { get; set; }
    }
}
