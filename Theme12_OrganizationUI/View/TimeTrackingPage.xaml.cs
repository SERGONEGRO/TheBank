using Theme12_OrganizationUI.ViewModel;
using System.Windows.Controls;

namespace Theme12_OrganizationUI.View
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
