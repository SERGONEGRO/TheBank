using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheBank.ViewModel;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для SalaryPage.xaml
    /// </summary>
    public partial class SalaryPage : Page
    {
        public SalaryPage()
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new SalaryPageVM();

        }
    }
}
