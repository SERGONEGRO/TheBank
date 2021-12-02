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
using Theme12_OrganizationUI.ViewModel;

namespace Theme12_OrganizationUI.View
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsPage.xaml
    /// </summary>
    public partial class DepartmentsPage : Page
    {
        public DepartmentsPage()
        {
            InitializeComponent();
            DataContext = new DepartmentsPageVM(this);
        }
    }
}
