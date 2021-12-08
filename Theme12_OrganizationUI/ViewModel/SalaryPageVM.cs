using TheBank.Models;
using System.Collections.ObjectModel;

namespace TheBank.ViewModel
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
