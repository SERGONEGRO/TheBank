namespace Theme12_OrganizationUI.Models
{
    class EditableStaff
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public EditableStaff(string surename, string name, string departmentName,int hours, int personnelNumber)
        {
            PersonnelNumber = personnelNumber;
            Fullname = $"{surename} {name}";
            DepartmentName = departmentName;
            OriginalHours = hours;
            EditableHours = hours;
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
        /// Количество отработанных часов (как в базе)
        /// </summary>
        public int OriginalHours { get; set; }

        /// <summary>
        /// Количество отработанных часов (показываем и редактируем)
        /// </summary>
        public int EditableHours { get; set; }
    }
}
