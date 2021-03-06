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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheBank.ViewModel;
using TheBank.Models;
using System.Collections.ObjectModel;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для ClientTablePage.xaml
    /// </summary>
    public partial class ClientTablePage : Page
    {
        public ClientTablePage()
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new ClientTablePageVM(this);
        }

        /// <summary>
        /// Конструктор для обновления данных
        /// </summary>
        public ClientTablePage(ObservableCollection<Client> clients)
        {
            InitializeComponent();

            //подключаем VM
            DataContext = new ClientTablePageVM(this, clients);

        }
    }
}
