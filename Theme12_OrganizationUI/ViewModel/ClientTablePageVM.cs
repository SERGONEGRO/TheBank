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
            Clients.Add(new Client());
            clients[0].Id = 1;   //test
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
        #region Команды
        /// <summary>
        /// Удалить Клиента (команда)
        /// </summary>
        RelayCommand removeClientCommand;

        /// <summary>
        /// Удалить Клиента (свойство)
        /// </summary>
        public RelayCommand RemoveClientCommand
        {
            get
            {
                //если поле не инициализировано - инициализируем
                if (removeClientCommand == null)
                {
                    removeClientCommand = new RelayCommand(obj =>
                    {
                        //получаем выделенного Клиента
                        Client client = tableLV.SelectedItem as Client;
                        //пытаемся удалить его
                        if (DataWorker.RemoveClient(client))
                        {
                            MessageBox.Show("Сотрудник успешно удален");
                        }
                    });
                }
                return removeClientCommand;
            }
        }
        #endregion
    }


}
