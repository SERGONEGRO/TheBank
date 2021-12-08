namespace TheBank.Models
{
    class EditableStaff
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public EditableStaff(string surename, string name, string departmentName, string cathegory, int hours, int personnelNumber)
        {
            PersonnelNumber = personnelNumber;
            Fullname = $"{surename} {name}";
            DepartmentName = departmentName;
            Cathegory = cathegory;
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
        /// Категория должности
        /// </summary>
        public string Cathegory { get; set; }

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
