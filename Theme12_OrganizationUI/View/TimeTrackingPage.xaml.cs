using TheBank.ViewModel;
using System.Windows.Controls;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для TimeTrackingPage.xaml
    /// </summary>
    public partial class TimeTrackingPage : Page
    {
        public TimeTrackingPage()
        {
            InitializeComponent();

            //подключаем VM
            DataContext = new TimeTrackingPageVM();
        }
    }
}
