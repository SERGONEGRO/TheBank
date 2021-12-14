using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheBank.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Cобытие "свойство изменилось"
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Обработчик события "свойство изменилось"
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Проверяем заполнение текстового поля
        /// </summary>
        public bool CheckTextBoxContent(TextBox tb, TextBlock messageTB, string warningText)
        {
            //Если пользователь ничего не ввел
            if (tb.Text == null || tb.Text.Trim() == "")
            {
                //подсвечиваем блок красным
                tb.BorderBrush = Brushes.Red;
                //выводим предупреждение
                ShowWarning(messageTB, warningText);
                //возвращаем результат
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяем наличие выбранного элемента в выпадающем списке
        /// </summary>
        public bool CheckComboBox(ComboBox cb, TextBlock messageTB, string warningText)
        {
            //Если пользователь ничего не ввел
            if (cb.SelectedItem == null)
            {
                //подсвечиваем блок красным
                cb.BorderBrush = Brushes.Red;
                //выводим предупреждение
                ShowWarning(messageTB, warningText);
                //возвращаем результат
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяем выбор даты
        /// </summary>
        public bool CheckDatePicker(DatePicker dp, TextBlock messageTB, string warningText)
        {
            //Если пользователь ничего не ввел
            if (dp.SelectedDate == null)
            {
                //подсвечиваем блок красным
                dp.BorderBrush = Brushes.Red;
                //выводим предупреждение
                ShowWarning(messageTB, warningText);
                //возвращаем результат
                return false;
            }

            return true;
        }

        /// <summary>
        /// Выводит предупреждение
        /// </summary>
        public void ShowWarning(TextBlock messageTB, string warningText)
        {
            //перекрашиваем сообщение
            messageTB.Foreground = Brushes.Red;
            //выводим сообщение
            messageTB.Text = warningText;
        }
    }
}
