using TheBank.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TheBank.ViewModel
{
    class TimeTrackingPageVM : ViewModel
    {
        public TimeTrackingPageVM()
        {
            Staffs = DataWorker.GetStaffs();
        }

        /// <summary>
        /// Весь персонал (сотрудники с почасовой оплатой)
        /// </summary>
        public ObservableCollection<EditableStaff> Staffs { get; set; }

        /// <summary>
        /// Команда: Сохранить изменения
        /// </summary>
        RelayCommand saveStaffCommand;

        /// <summary>
        /// Свойство: Сохранить изменения
        /// </summary>
        public RelayCommand SaveStaffCommand
        {
            get
            {
                if (saveStaffCommand == null)
                {
                    saveStaffCommand = new RelayCommand(obj =>
                    {
                        //проверяем всех сотрудников в коллекции
                        foreach (var item in Staffs)
                        {
                            //если изменилось количество рабочих часов
                            if (item.OriginalHours != item.EditableHours)
                            {
                                //сохраняем этого сотрудника с изменениями
                                DataWorker.EditStaffHours(item.PersonnelNumber, item.EditableHours);
                            }
                        }
                        MessageBox.Show("Изменения сохранены.");
                    });
                }
                return saveStaffCommand;
            }
        }

    }
}
