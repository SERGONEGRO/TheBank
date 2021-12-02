using Theme12_OrganizationUI.Models;
using System.Collections.ObjectModel;

namespace Theme12_OrganizationUI.ViewModel
{
    class SalaryPageVM
    {
        public SalaryPageVM()
        {
            Payrolls = DataWorker.GetPayrolls();
        }

        /// <summary>
        /// Все зарплатные ведомости
        /// </summary>
        public ObservableCollection<Payroll> Payrolls { get; set; }
    }
}
