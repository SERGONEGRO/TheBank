using TheBank.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace TheBank.Data
{
    class ApplicationContext : DbContext
    {
        //путь к файлу сохранения "Департаменты"
        private readonly string path = @"TheBANK.json";

        /// <summary>
        /// Дерево департаментов
        /// </summary>
        public ObservableCollection<Department> DepartmentsTree { get; set; }

        /// <summary>
        /// список клиентов
        /// </summary>
        public List<Client> ClientsList { get; set; }

        /// <summary>
        /// список вкладов
        /// </summary>
        public List<Deposit> DepositList { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ApplicationContext() : base("DefaultConnection")
        {
            //инициализируем дерево департаментов
            DepartmentsTree = new ObservableCollection<Department>();
            //инициализируем дерево клиентов
            ClientsList = new List<Client>();
            //инициализируем дерево вкладов
            DepositList = new List<Deposit>();

            //загружаем БД
            Load();
        }

        #region МЕТОДЫ СОХРАНЕНИЯ
        /// <summary>
        /// Сохраняем БД
        /// </summary>
        public void Save()
        {
            //проверяем наличие файла сохранения
            if (!File.Exists(path))
            {
                //если такого нет - создаем
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write))) { }
            }

            //корневой элемент
            JObject Root = new JObject();

                                /*----департменты-------*/
            //дочерние департаменты 
            JArray JDepartments = new JArray();
            //создаем новый объект-департамент
            JObject JDepartment = new JObject();
            //заполняем его
            FillDepartment(JDepartment, DepartmentsTree[0]);
            //добавляем в массив
            JDepartments.Add(JDepartment);

                                /*--------Клиенты------*/
            JArray JClients = new JArray();
            //создаем новый объект-клиент
            //JObject JClient = new JObject();
            //заполняем его
            FillClients(JClients, ClientsList);
            //добавляем в массив
            //JClients.Add(JClient);

            //помещаем элемент "департаменты" в корневой
            Root["DEPARTMENTS"] = JDepartments;
            //помещаем элемент "Клиенты" в корневой
            Root["CLIENTS"] = JClients;

            //сериализуем
            string json = Root.ToString();

            //сохраняем
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Метод заполнения одного департамента
        /// </summary>
        private void FillDepartment(JObject targetObject, Department sourceDepartment)
        {
            //Элемент "название департамента"
            targetObject["NAME"] = sourceDepartment.Name;

            //дочерние департаменты 
            JArray JDepartments = new JArray();

            //заполняем массив департаментов
            for (int i = 0; i < sourceDepartment.Departments.Count; i++)
            {
                //создаем новый объект-департамент
                JObject JDepartment = new JObject();
                //заполняем его
                FillDepartment(JDepartment, sourceDepartment.Departments[i]);
                //добавляем в массив
                JDepartments.Add(JDepartment);
            }

            //элемент "департаменты"
            targetObject["DEPARTMENTS"] = JDepartments;

            //сотрудники
            JArray JEmployees = new JArray();

            //заполняем массив сотрудников
            for (int i = 0; i < sourceDepartment.Employees.Count; i++)
            {
                //создаем новый объект-сотрудник
                JObject JEmployee = new JObject();
                //заполняем его
                FillEmployee(JEmployee, sourceDepartment.Employees[i]);
                //добавляем в массив
                JEmployees.Add(JEmployee);
            }

            //элемент "сотрудники"
            targetObject["EMPLOYEES"] = JEmployees;

        }

        /// <summary>
        /// Метод заполнения одного сотрудника
        /// </summary>
        private void FillEmployee(JObject jEmployee, Employee employee)
        {
            //заполняем все
            jEmployee["ID"] = employee.Id;
            jEmployee["FIRSTNAME"] = employee.FirstName;
            jEmployee["LASTNAME"] = employee.LastName;
            jEmployee["DATEOFBIRTH"] = employee.DateOfBirth;
            jEmployee["DEPARTMENTNAME"] = employee.DepartmentName;
            jEmployee["CATHEGORY"] = employee.Cathegory.ToString();
            jEmployee["RATE"] = employee.Rate;
            jEmployee["HOURS"] = employee.Hours;
            //jEmployee["PROJECTSCOUNT"] = employee.ProjectsCount;
        }

        /// <summary>
        /// Метод заполнения массива клиентов
        /// </summary>
        private void FillClients(JArray JClients, List<Client> clients)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                //создаем новый объект-клиент
                JObject JClient = new JObject();
                //заполняем его
                FillClient(JClient, clients[i]);
                //добавляем в массив
                JClients.Add(JClient);
            }
        }
        /// <summary>
        /// Метод заполнения одного Клиента
        /// </summary>
        private void FillClient(JObject jClient, Client client)
        {
            //заполняем все
            jClient["ID"] = client.Id;
            jClient["FIRSTNAME"] = client.FirstName;
            jClient["LASTNAME"] = client.LastName;
            jClient["DATEOFBIRTH"] = client.DateOfBirth;
            jClient["ISVIP"] = client.IsVIP;
        }
        #endregion

        #region МЕТОДЫ ЗАГРУЗКИ
        /// <summary>
        /// Загружаем БД
        /// </summary>
        private void Load()
        {
            //если нет такого файла
            if (!File.Exists(path))
            {
                //создаем новый департамент
                Department newDepartment = new Department("The Negro Bank");

                //добавляем его в дерево департаментов
                DepartmentsTree.Add(newDepartment);

                //сохраняем БД
                Save();
            }
            else
            {
                //читаем файл
                string json = File.ReadAllText(path);

                //выгружаем данные
                var JDepartments = JObject.Parse(json)["DEPARTMENTS"].ToArray();
                //добавляем его в дерево департаментов
                DepartmentsTree.Add(ParseDepartment(JDepartments[0], ""));

                //выгружаем клиентов
                var JClients = JObject.Parse(json)["CLIENTS"].ToArray();
                foreach (var item in JClients)
                {
                    ClientsList.Add(ParseClient(item));
                }
            }

        }

        /// <summary>
        /// Выгружает клиента из JSON
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Client ParseClient(JToken jclient)
        {
            //объявляем нового сотрудника
            Client client = new Client();
            //заполняем его поля
            client.Id = (int)jclient["ID"];
            client.FirstName = jclient["FIRSTNAME"].ToString();
            client.LastName = jclient["LASTNAME"].ToString();
            client.DateOfBirth = (DateTime)jclient["DATEOFBIRTH"];

            if ((string)jclient["ISVIP"] == "true")  client.IsVIP = true;  else client.IsVIP = false;
            
            return client;
        }

        /// <summary>
        /// Выгружает один департамент из JSON
        /// </summary>
        private Department ParseDepartment(JToken jDepartment, string parentName)
        {
            //получаем название департамента
            string name = jDepartment["NAME"].ToString();

            //создаем новый департамент
            Department dataDepartment = new Department(name, parentName);

            //выгружаем список департаментов
            var JDepartments = jDepartment["DEPARTMENTS"].ToArray();

            //получаем все дочерние департаменты
            foreach (var item in JDepartments)
            {
                dataDepartment.Departments.Add(ParseDepartment(item, dataDepartment.Name));
            }

            //если в элементе есть список сотрудников (его нет в корневом) 
            if (jDepartment["EMPLOYEES"] != null)
            {
                //выгружаем список сотрудников
                var JEmployees = jDepartment["EMPLOYEES"].ToArray();

                //получаем всех сотрудников
                foreach (var item in JEmployees)
                {
                    //парсим сотрудника в департамент
                    dataDepartment.Employees.Add(ParseEmployee(item));
                }
            }
            //возвращаем заполненный департамент
            return dataDepartment;
        }

        /// <summary>
        /// Выгружает одного сотрудника из JSON
        /// </summary>
        private Employee ParseEmployee(JToken jEmployee)
        {
            //объявляем нового сотрудника
            Employee employee;

            string cathegory = jEmployee["CATHEGORY"].ToString();

            //выясняем категорию сотрудника (класс) и инициализируем
            if (cathegory == "Менеджер") { employee = new Manager(); }
            else if (cathegory == "Специалист") { employee = new Worker(); }
            else { employee = new Intern(); }

            //заполняем его поля
            employee.Id = (int)jEmployee["ID"];
            employee.FirstName = jEmployee["FIRSTNAME"].ToString();
            employee.LastName = jEmployee["LASTNAME"].ToString();
            employee.DateOfBirth = (DateTime)jEmployee["DATEOFBIRTH"];
            employee.DepartmentName = jEmployee["DEPARTMENTNAME"].ToString();
            employee.Rate = (int)jEmployee["RATE"];
            employee.Hours = (int)jEmployee["HOURS"];
           
            if ((string)jEmployee["CATHEGORY"] == "Менеджер") { employee.Cathegory = Cathegory.Менеджер; }
            else if ((string)jEmployee["CATHEGORY"] == "Специалист") { employee.Cathegory = Cathegory.Специалист; }
            else if ((string)jEmployee["CATHEGORY"] == "Интерн") { employee.Cathegory = Cathegory.Интерн; }

            return employee;
        }
        #endregion

    }
}
