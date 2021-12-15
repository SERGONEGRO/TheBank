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
using TheBank.Models;
using TheBank.ViewModel;

namespace TheBank.View
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public ClientPage()
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new ClientPageVM(this);
        }

        /// <summary>
        /// Для редактирования существующего клиента
        /// </summary>
        /// <param name="employee"></param>
        public ClientPage(Client client)
        {
            InitializeComponent();
            //подключаем VM
            DataContext = new ClientPageVM(this, client);
        }
    }

}
