using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TheBank.Models;
using TheBank.View;
using System.Collections.ObjectModel;

namespace TheBank.ViewModel
{
    class ClientTablePageVM:ViewModel
    {
        public ClientTablePageVM(Page page)
        {
            Clients = DataWorker.GetAllClients();
            //tableLV = page.FindName("tableLV") as ListView;
            
        }

        /// <summary>
        /// Текущая отображаемая страница (свойство)
        /// </summary>
        public Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                NotifyPropertyChanged("CurrentPage");
            }
        }

        /// <summary>
        /// Текущая отображаемая страница
        /// </summary>
        Page currentPage;

        /// <summary>
        /// Таблица клиентов
        /// </summary>
        ListView tableLV;

        /// <summary>
        /// Все клиенты
        /// </summary>
        ObservableCollection<Client> clients;

        /// <summary>
        /// Все клиенты (свойство)
        /// </summary 
        public ObservableCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
                NotifyPropertyChanged("Clients");
            }
        }
    }

    
}
