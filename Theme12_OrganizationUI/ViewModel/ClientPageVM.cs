using TheBank.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TheBank.ViewModel
{
    class ClientPageVM : ViewModel
    {

        #region КОНСТРУКТОРЫ
        /// <summary>
        /// Конструктор (новый клиент)
        /// </summary>
        /// <param name="page"></param>
        public ClientPageVM(Page page)
        {
            this.page = page;
            isNew = true;
            Initialize();
        }

        /// <summary>
        /// Конструктор (существующий клиент)
        /// </summary>
        /// <param name="page"></param>
        public ClientPageVM(Page page, Client client)
        {
            this.page = page;
            isNew = false;
            this.client = client;
            FillViewsAndSources();
            debugBttn.Visibility = Visibility.Hidden;
        }
        #endregion

        #region ВЬЮ-ЭЛЕМЕНТЫ
        /// <summary>
        /// Текстблок: Место для вывода сообщения
        /// </summary>
        TextBlock messageTB;

        /// <summary>
        /// Кнопка рандомного заполнения полей
        /// </summary>
        Button debugBttn;

        /// <summary>
        /// Граница блока: категория должности
        /// </summary>
        Border isVIPBorder;
        #endregion

        #region ТЕКСТБОКСЫ
        /// <summary>
        /// Текстбокс: Фамилия
        /// </summary>
        TextBox lastnameTB;

        /// <summary>
        /// Текстбокс: Имя
        /// </summary>
        TextBox firstnameTB;

        /// <summary>
        /// Текстбокс: Id
        /// </summary>
        TextBlock idTB;

        #endregion

        #region РАДИОКНОПКИ

        /// <summary>
        /// Радиокнопка: VIP
        /// </summary>
        RadioButton VIP;

        /// <summary>
        /// Радиокнопка: NOVIP
        /// </summary>
        RadioButton NOVIP;

       
        #endregion

        #region ДАТЫ
        /// <summary>
        /// Поле даты: День рождения
        /// </summary>
        DatePicker birthDP;


        #endregion


        #region ПРОЧИЕ ПОЛЯ
        /// <summary>
        /// Вьюшка
        /// </summary>
        readonly Page page;

        /// <summary>
        /// Новый клиент
        /// </summary>
        readonly bool isNew;

        /// <summary>
        /// Сотрудник
        /// </summary>
        readonly Client client;

        /// <summary>
        /// рандомайзер
        /// </summary>
        readonly Random random = new Random();
        #endregion

        #region КОМАНДЫ

        /// <summary>
        /// Команда: Сохранить клиента
        /// </summary>
        RelayCommand saveClientCommand;

        /// <summary>
        /// Свойство: Сохранить клиента
        /// </summary>
        public RelayCommand SaveClientCommand
        {
            get
            {
                if (saveClientCommand == null)
                {
                    saveClientCommand = new RelayCommand(obj =>
                    {
                        //проверяем заполнение обязательных полей
                        if (CheckFillings())
                        {
                            //запрашиваем подтверждение
                            string MBText;
                            if (isNew) MBText = $"Вы точно хотите создать нового клиента?";
                            else MBText = $"Вы точно хотите отредактировать клиента?";
                            string MBCaption = "Требуется подтверждение";
                            MessageBoxResult result = MessageBox.Show(MBText, MBCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question);

                            //если подтверждение получено
                            if (result == MessageBoxResult.OK)
                            {
                                //объявляем нового сотрудника
                                Client newClient = new Client();

                                //определяем VIP
                                if (VIP.IsChecked == true) newClient.IsVIP = true;
                                else newClient.IsVIP = false;

                                //заполняем поля нового сотрудника
                                FillClientFields(newClient);

                                //если создаем нового сотрудника
                                if (isNew) DataWorker.AddClient(newClient);
                                //если редактируем существующего сотрудника
                                else
                                {
                                    newClient.Id = Convert.ToInt32(idTB.Text);
                                    DataWorker.EditClient(newClient);
                                }
                                //выводим сообщение о результате
                                if (isNew) MessageBox.Show("Новый клиент создан");
                                else MessageBox.Show("Клиент успешно отредактирован");
                            }
                        }
                    });
                }
                return saveClientCommand;
            }
        }

        /// <summary>
        /// Команда: Заполнить поля рандомно
        /// </summary>
        RelayCommand randomClientCommand;

        /// <summary>
        /// Свойство: Заполнить поля рандомно
        /// </summary>
        public RelayCommand RandomClientCommand
        {
            get
            {
                if (randomClientCommand == null)
                {
                    randomClientCommand = new RelayCommand(obj =>
                    {
                        //снимаем подсветку с полей
                        RemoveBacklight();
                        //Заполняем поля рандомными значениями
                        FillRandom();
                    });
                }
                return randomClientCommand;
            }
        }
        #endregion

        #region МЕТОДЫ
        /// <summary>
        /// Заполняет обязательные поля рандомными значениями (кроме департамента, ставки и категории должности)
        /// </summary>
        private void FillRandom()
        {
            lastnameTB.Text = GenerateString("Иванов", "Петров", "Сидоров", "Мухоморов", "Коняев", "Афанасьев", "Самсонов", "Кузякин");
            firstnameTB.Text = GenerateString("Иван", "Владислав", "Кирилл", "Арсений", "Семён", "Матвей", "Александр", "Степан");
            birthDP.SelectedDate = GenerateDate();
        }

        /// <summary>
        /// Генерирует число, заданной длины
        /// </summary>
        private string Generateint(int v)
        {
            string str = "";
            for (int i = 0; i < v; i++)
            {
                str += random.Next(10);
            }
            return str;
        }

        /// <summary>
        /// Генерирует рандомную строку
        /// </summary>
        /// <returns></returns>
        private string GenerateString(params string[] parameters)
        {
            //возвращаем случайную строку
            return parameters[random.Next(parameters.Length)];
        }

        /// <summary>
        /// Генерирует рандомную дату
        /// </summary>
        /// <returns></returns>
        private DateTime GenerateDate()
        {
            var startDate = new DateTime(1985, 1, 1);
            Console.WriteLine(startDate.ToString("yyyy.dd.MM"));
            var newDate = startDate.AddDays(random.Next(9999));
            Console.WriteLine(newDate.ToString("yyyy.dd.MM"));
            return newDate;
        }

        /// <summary>
        /// Заполняет поля клиента данными из формы
        /// </summary>
        /// <param name="newClient"></param>
        private void FillClientFields(Client newClient)
        {
            //заполняем личные данные
            newClient.LastName = lastnameTB.Text;
            newClient.FirstName = firstnameTB.Text;

            newClient.DateOfBirth = (DateTime)birthDP.SelectedDate;

            //заполняем служебные данные

            if (VIP.IsChecked == true)
            {
                newClient.IsVIP = true;
            }
            else newClient.IsVIP = false;

        }

        /// <summary>
        /// Заполняет поля вьюшки и источники данных
        /// </summary>
        private void FillViewsAndSources()
        {
            Initialize();

            //заполняем текстбоксы
            lastnameTB.Text = client.LastName;
            firstnameTB.Text = client.FirstName;
            
            //заполняем радиокнопки: Категория должности (менеджер, специалист, Интерн)
            if (client.IsVIP == true) { VIP.IsChecked = true; }
            else NOVIP.IsChecked = true;

            //заполняем даты
            birthDP.SelectedDate = client.DateOfBirth;

            //Id
            idTB.Text = client.Id.ToString();
        }

        /// <summary>
        /// инициализирует элементы и источники данных
        /// </summary>
        private void Initialize()
        {
            //текстблок
            messageTB = page.FindName("messageTB") as TextBlock;

            //кнопка
            debugBttn = page.FindName("debugBttn") as Button;

            //границы
            isVIPBorder = page.FindName("isVIPBorder") as Border;
            
            //текстбоксы
            lastnameTB = page.FindName("lastnameTB") as TextBox;
            firstnameTB = page.FindName("firstnameTB") as TextBox;
            idTB = page.FindName("idTB") as TextBlock;

            //радиокнопки

            VIP = page.FindName("VIP") as RadioButton;
            NOVIP = page.FindName("NOVIP") as RadioButton;
           
            //даты
            birthDP = page.FindName("birthDP") as DatePicker;

        }

        /// <summary>
        /// Проверяет заполнение обязательных полей
        /// </summary>
        /// <returns></returns>
        private bool CheckFillings()
        {
            //отключаем подсветку выделенных элементов
            RemoveBacklight();

            if (!CheckTextBoxContent(lastnameTB, messageTB, "Необходимо указать фамилию!")
                || !CheckTextBoxContent(firstnameTB, messageTB, "Необходимо указать имя!")
                || !CheckDatePicker(birthDP, messageTB, "Необходимо указать дату рождения!")
                )
            {
                return false;
            }

            return true;
        }

        

        /// <summary>
        /// Отключает подсветку выделенных элементов
        /// </summary>
        private void RemoveBacklight()
        {
            //Убираем сообщение
            messageTB.Text = "";

            lastnameTB.BorderBrush = Brushes.Gray;
            firstnameTB.BorderBrush = Brushes.Gray;
            birthDP.BorderBrush = Brushes.Gray;
            isVIPBorder.BorderBrush = Brushes.Transparent;
        }

        #endregion
    }
}
