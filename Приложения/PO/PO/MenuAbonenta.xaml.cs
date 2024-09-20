using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using static PO.Panel;
using System.Reflection;
using System.Windows.Controls.Primitives;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using System.Security.AccessControl;
using System.Collections;
using static PO.MenuAbonenta;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media.Animation;
using System.Threading;
using System.Windows.Threading;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Timers;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace PO
{
    /// <summary>
    /// Логика взаимодействия для MenuAbonenta.xaml
    /// </summary>
    public partial class MenuAbonenta : Window
    {
        bool Done = false;
     
        
        int level;
     
        string connectionString = "Server=.\\SQLEXPRESS;Database=database_PO;User id=sa;Password=Pa$$w0rd;Encrypt=false";
        //string connectionString = "Server=DESKTOP-7AS9I4L\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        //  string connectionString = "Server=DESKTOP-RUBV4AP\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        public MenuAbonenta()
        {
            InitializeComponent();
            LoadDataGrid();
            ListBoxEvent();
            CRMDAtaDrid();
            LoadComboBoxData();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.2);
            timer.Tick += timer_Tick;
            timer.Start();
            SupporrtingUser();
            AppoimentStaff();
            LoadListBoxik();



        }

        void timer_Tick(object sender, EventArgs e)
        {
            SearchTextBox_TextChanged();
            SearchTextBox_TextChanged2();
           

            int skipCount = 0;
            l1.Content = ComboBox.Text;
            foreach (var child in GridTextBox.Children)
            {
                if (child is TextBox textBox)
                {
                    if (skipCount < 1)
                    {
                        skipCount++;
                        continue;
                    }

                    {
                        if (string.IsNullOrWhiteSpace(textBox.Text) || ComboBox.Text == "" || Done == false)
                        {
                            Done_Button.IsEnabled = false;
                            break;
                        }
                        else
                        {
                            Done_Button.IsEnabled = true;
                        }
                    }
                }
            }

        }
    
      

        public class Abonents
        {

            public int Id_Abonent { get; set; }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Name_Service { get; set; }
            public string Personal_account { get; set; }
            public string Number_Of_phone { get; set; }
            public string eMail { get; set; }
            public string ContractNumber { get; set; }
            public string TypeOfAgreement { get; set; }
            public string Reas_for_term_of_contr { get; set; }

        }

        private void ListBoxEvent()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Name_event,time FROM Events";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ListBoxEvents.Items.Add(reader[0].ToString());
                            ListBoxEvents.Items.Add(reader[1].ToString());

                        }

                    }
                }
            }

        }

        private void LoadComboBoxData()
        {

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                string sqlExpression1 = $"select FirstName, SecondName, LastName, Post from Passport inner join Staff on Passport.Id_Passport = Staff.Passport";
                SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        string fullName = $"{reader1.GetString(0)} {reader1.GetString(1)} {reader1.GetString(2)} || {reader1.GetString(3)}";
                       
                        ComboBox.Items.Add(fullName);
                    }
                    reader1.Close();
                }

            }
            connection.Close();

        }
        private void LoadDataGrid()
        
        {

            List<Abonents> equipmentList = new List<Abonents>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Id_Abonent, FirstName ,LastName, Name_Service, Personal_account, Number_Of_phone, eMail, ContractNumber,TypeOfAgreement,Reason_for_termination_of_the_contract FROM Abonent " +
                    "INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport " +
                    "INNER JOIN Service ON Service.Id_Services = Abonent.Service";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Abonents equipment = new Abonents
                            {

                                Id_Abonent = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Name_Service = reader.GetString(3),
                                Personal_account = reader.GetString(4),
                                Number_Of_phone = reader.GetString(5),
                                eMail = reader.GetString(6),
                                ContractNumber = reader.GetString(7),
                                TypeOfAgreement = reader.GetString(8),
                                Reas_for_term_of_contr = reader.GetString(9),

                            };

                            equipmentList.Add(equipment);

                        }

                    }
                }
            }

            Read.ItemsSource = equipmentList;


        }
        public class Applicatoins
        {
            public int id_Applicatoins { get; set; }
            public string Worker { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string SecondName { get; set; }
            public string Number_Of_Phone { get; set; }
            public string application_place { get; set; }
            public string status { get; set; }
            public string Type_of_application { get; set; }
            public string description_of_problem { get; set; }
            public string date_create { get; set; }
            public string date_close { get; set; }


        }
        public class SupUser
        {
            public TimeSpan TimeSupport { get; set; }
            public DateTime Data_Support { get; set; }
            public string statuss { get; set; }

        }
        public class BillingAbonnent
        {
            public string fiirsName { get; set; }
            public string seecondName { get; set; }
            public string personalAccount { get; set; }
            public string Nametarif { get; set; }
            public int costPackage { get; set; }
            public string deptInformation { get; set; }
        }// в новый проект 
        public class HistoryBillingAbonnent
        {
            public string datePayment { get; set; }
            public int sumaPayment { get; set; }
            public int balance { get; set; }
            public string historyPayment { get; set; }
        }// в новый проект 
        private void BillAbonent()
        {
            List<BillingAbonnent> AbonentList = new List<BillingAbonnent>();
            List<HistoryBillingAbonnent> AbonentsList = new List<HistoryBillingAbonnent>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT FirstName,SecondName, Personal_account,Name_Tarif,Cost_package,Debt_information FROM Abonent INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            BillingAbonnent Abonent = new BillingAbonnent
                            {
                                fiirsName = reader.GetString(0),
                                seecondName = reader.GetString(1),
                                personalAccount = reader.GetString(2),
                                Nametarif = reader.GetString(3),
                                costPackage = reader.GetInt32(4),
                                deptInformation = reader.GetString(5),



                            };

                            AbonentList.Add(Abonent);

                        }

                    }
                }
                DataGrid_one.ItemsSource = AbonentList;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Date_payment, Suma_payment, Balance, History_payment FROM Abonent";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            HistoryBillingAbonnent Abonents = new HistoryBillingAbonnent
                            {

                                datePayment = reader.GetDateTime(0).ToString("dd.MM.yyyy"),
                                sumaPayment = reader.GetInt32(1),
                                balance = reader.GetInt32(2),
                                historyPayment = reader.GetString(3),



                            };

                            AbonentsList.Add(Abonents);

                        }

                    }
                }
                DataGrid_two.ItemsSource = AbonentsList;
            }



        }// в новый проект  
        private void SerchBillingAbonent()
        {
            List<BillingAbonnent> AbonentList = new List<BillingAbonnent>();

            List<HistoryBillingAbonnent> AbonentsList = new List<HistoryBillingAbonnent>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlExpression = "SELECT FirstName,SecondName,Personal_account,Name_Tarif,Cost_package,Debt_information FROM Abonent INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport WHERE FirstName = @FirstName AND SecondName = @SecondName";

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {

                    string firstName = FamiliyaSerch.Text;
                    string secondName = NameSerch.Text;

                    if (!firstName.All(char.IsLetter) || !secondName.All(char.IsLetter))
                    {
                        MessageBox.Show("Ошибка: Неправильно введены данные. Пожалуйста, введите только буквы.");
                        return;
                    }

                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@SecondName", secondName);



                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Абонент с указанным именем и фамилией не найден.");
                            return;
                        }
                        while (reader.Read())
                        {
                            BillingAbonnent Abonent = new BillingAbonnent();

                            Abonent.fiirsName = reader.GetString(0);
                            Abonent.seecondName = reader.GetString(1);
                            Abonent.personalAccount = reader.GetString(2);
                            Abonent.Nametarif = reader.GetString(3);
                            Abonent.costPackage = reader.GetInt32(4);
                            Abonent.deptInformation = reader.GetString(5);

                            AbonentList.Add(Abonent);
                        }
                    }
                }
            }

            DataGrid_one.ItemsSource = AbonentList;


        
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlExpression = "SELECT  Date_payment, Suma_payment, Balance, History_payment FROM Abonent INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport WHERE FirstName = @FirstName AND SecondName = @SecondName";

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {

                    string firstName = FamiliyaSerch.Text;
                    string secondName = NameSerch.Text;


                    if (!firstName.All(char.IsLetter) || !secondName.All(char.IsLetter))
                    {
                        MessageBox.Show("Ошибка: Неправильно введены данные. Пожалуйста, введите только буквы.");
                        return;
                    }

                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@SecondName", secondName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Абонент с указанным именем и фамилией не найден.");
                            return;
                        }
                        while (reader.Read())
                        {
                            HistoryBillingAbonnent Abonents = new HistoryBillingAbonnent();

                            Abonents.datePayment = reader.GetDateTime(0).ToString("dd.MM.yyyy");
                            Abonents.sumaPayment = reader.GetInt32(1);
                            Abonents.balance = reader.GetInt32(2);
                            Abonents.historyPayment = reader.GetString(3);

                            AbonentsList.Add(Abonents);
                        }
                    }
                }
            }

            DataGrid_two.ItemsSource = AbonentsList;

        }// в новый проект
        public class otchetDa 
        {
            public string fiirstName { get; set; }
            public string seeconndName { get; set; }
            public string personallAccount { get; set; }
            public string Nametariff { get; set; }
            public string Name_service { get; set; }
            public int costPackagee { get; set; }
            public string deptInformationn { get; set; }
            public string datePaymentt { get; set; }
            public int sumaPaymentt { get; set; }
            public int balancee { get; set; }
            public string historyPaymentt { get; set; }
            public string Statuso { get; set; }
            public string data_createe { get; set; }
            public string data_close { get; set; }


        }

        private void SerchByDate()
        {
            List<otchetDa> AbList = new List<otchetDa>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string fromDate = fromTextBox.Text; 
                string beforeDate = beforeTextBox.Text; 

              
                fromDate = fromDate.Replace(".", "-");
                beforeDate = beforeDate.Replace(".", "-");

                string sqlExpression = $"SELECT FirstName, SecondName, Personal_account, Name_Tarif,Name_Service, Cost_package, Debt_information, Date_payment, Suma_payment, Balance, History_payment, Status, Date_of_create, Date_of_close\r\nFROM Abonent \r\nINNER JOIN Passport  ON Id_Passport = Passport\r\nINNER JOIN Applications  ON Id_applications = Id_Abonent INNER JOIN Service  ON Service = Id_Services WHERE Date_of_create BETWEEN '{fromDate}' AND '{beforeDate}';";

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                       
                        while (reader.Read())
                        {
                            otchetDa das = new otchetDa();

                            das.fiirstName = reader.GetString(0);
                            das.seeconndName = reader.GetString(1);
                            das.personallAccount = reader.GetString(2);
                            das.Nametariff = reader.GetString(3);
                            das.Name_service = reader.GetString(4);
                            das.costPackagee = reader.GetInt32(5);
                            das.deptInformationn = reader.GetString(6);
                            das.datePaymentt = reader.GetDateTime(7).ToString("dd.MM.yyyy");
                            das.sumaPaymentt = reader.GetInt32(8);
                            das.balancee = reader.GetInt32(9);
                            das.historyPaymentt = reader.GetString(10);
                            das.Statuso = reader.GetString(11);
                            das.data_createe = reader.GetDateTime(12).ToString("dd.MM.yyyy");
                            das.data_close = reader.GetDateTime(13).ToString("dd.MM.yyyy");


                            AbList.Add(das);
                        }
                    }
                }
            }

            DataGrid_three.ItemsSource = AbList;
        }


        private void SupporrtingUser()
        {
            List<SupUser> SuperList = new List<SupUser>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Time_Support, Data_Support, Status FROM Support_User JOIN Applications ON Support_User.Id_Applications = Applications.Id_applications ORDER BY Time_Support;";

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SupUser suppor = new SupUser();
                            suppor.TimeSupport = reader.GetTimeSpan(0);
                            suppor.Data_Support = reader.GetDateTime(1);
                            suppor.statuss = reader.GetString(2);


                            DayOfWeek dayOfWeek = suppor.Data_Support.DayOfWeek;


                            string timeAndStatus = $"{suppor.TimeSupport.ToString(@"hh\:mm")} - {suppor.statuss}"; // Создание текста времени и статуса и настройка его свойств

                            Button button = new Button();
                            button.Content = timeAndStatus;
                            button.Background = Brushes.Aquamarine;
                            button.Width = 220;
                            button.FontSize = 20;
                            button.Height = 50;

                            button.Click += (sender, e) => Button_Click(sender, e);

                            switch (dayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    StackPanelMonday.Children.Add(button);
                                    break;
                                case DayOfWeek.Tuesday:
                                    StackPanelTuesday.Children.Add(button);
                                    break;
                                case DayOfWeek.Wednesday:
                                    StackPanelWednesday.Children.Add(button);
                                    break;
                                case DayOfWeek.Thursday:
                                    StackPanelThursday.Children.Add(button);
                                    break;
                                case DayOfWeek.Friday:
                                    StackPanelFriday.Children.Add(button);
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
            }
        }

        private void AppoimentStaff()
        {
         

        }

        private List<string> Workers()
        {
            List<string> workerList = new List<string>(); 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery2 = "select FirstName, SecondName, LastName, Post " +
                                   "FROM Applications " +
                                   "INNER JOIN Staff ON Staff.Id_staff = Applications.Staff " +
                                   "INNER JOIN Passport ON Passport.Id_Passport = Staff.Passport ";

                using (SqlCommand command = new SqlCommand(sqlQuery2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string worker = $"{reader.GetString(0)} {reader.GetString(1)} {reader.GetString(2)} || {reader.GetString(3)}";
                            workerList.Add(worker);
                        }
                    }
                }
            }
            return workerList;
        }
        private List<string> Workers2()
        {
            List<string> workerList = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery2 = "select FirstName, SecondName, LastName, Post from Passport inner join Staff on Passport.Id_Passport = Staff.Passport ";
                                   

                using (SqlCommand command = new SqlCommand(sqlQuery2, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string worker = $"{reader.GetString(0)} {reader.GetString(1)} {reader.GetString(2)} || {reader.GetString(3)}";
                            workerList.Add(worker);
                        }
                    }
                }
            }
            return workerList;
        }
        private void CRMDAtaDrid()
        {
            List<string> workerList = Workers();
            List<Applicatoins> equList = new List<Applicatoins>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int i = 0;
                string sqlQuery = "SELECT Id_applications,Post,FirstName,LastName,SecondName,Number_Of_Phone,Application_place,Status,Type_of_application, Description_of_problem, Date_of_create,Date_of_close FROM Applications\r\nINNER JOIN Staff ON Staff.Id_staff = Applications.Staff\r\nINNER JOIN Abonent ON Abonent.Id_Abonent = Applications.Abonent\r\nINNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport ";
               
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Applicatoins app = new Applicatoins();
                            app.id_Applicatoins = reader.GetInt32(0);

                            app.Worker =  workerList[i] ;
                            
                            app.FirstName = reader.GetString(2);
                            app.LastName = reader.GetString(3);
                            app.SecondName = reader.GetString(4);
                            app.Number_Of_Phone = reader.GetString(5);
                            app.application_place = reader.GetString(6);
                            app.status = reader.GetString(7);
                            app.Type_of_application = reader.GetString(8);
                            try
                            {
                                app.description_of_problem = reader.GetString(9);
                            }
                            catch
                            {
                                app.description_of_problem = "-";
                            }
                            app.date_create = reader.GetDateTime(10).ToString("dd.MM.yyyy");
                            app.date_close = reader.GetDateTime(11).ToString("dd.MM.yyyy");



                            equList.Add(app);
                            i++;

                        }

                    }
                }
            }

            CRMDataGrid.ItemsSource = equList;
        }
        bool check = false;
        public class User
        {
           
            public string FirstName { get; set; }

            public string SecondName { get; set; }

        }
        private void SearchTextBox_TextChanged2()
        {
           
            string searchName = Name_TextBox.Text.Trim();
            if (!string.IsNullOrEmpty(Name_TextBox.Text))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = $"SELECT SecondName FROM Passport WHERE FirstName  = '{Familiya_TextBox.Text}' and SecondName LIKE @searchName";
                        SqlCommand command1 = new SqlCommand(sqlQuery, connection);
                       command1.Parameters.AddWithValue("@searchName", "%" + searchName + "%");
                        SqlDataReader reader1 = command1.ExecuteReader();
                        ObservableCollection<User> users2 = new ObservableCollection<User>();
                        while (reader1.Read())
                        {
                            users2.Add(new User { FirstName = reader1.GetString(0) });
                        }

                        userListView2.ItemsSource = users2;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                userListView2.ItemsSource = null;
           
            }
        }
        private void SearchTextBox_TextChanged()
        {

            string searchName = Familiya_TextBox.Text.Trim();
           
            if (!string.IsNullOrEmpty(searchName))
            {
            
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sqlQuery = "SELECT  FirstName FROM Passport WHERE FirstName  LIKE @searchName";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@searchName", "%" + searchName + "%");
                        SqlDataReader reader = command.ExecuteReader();
                        ObservableCollection<User> users = new ObservableCollection<User>();

                        while (reader.Read())
                        {
                            users.Add(new User { SecondName  = reader.GetString(0) });
                        }

                        userListView.ItemsSource = users;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                userListView.ItemsSource = null;
           
            }
        }
        private void Familiya_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            userListView.Visibility = Visibility.Visible;
        }
        private void Name_TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

            userListView2.Visibility = Visibility.Visible;
        }


        private bool IsMouseOverListViewOrTextBox()
        {
            return userListView.IsMouseOver || Familiya_TextBox.IsMouseOver;
        }
        private bool IsMouseOverListViewOrTextBox2()
        {
            return userListView2.IsMouseOver || Name_TextBox.IsMouseOver;
        }
        private void Menu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseOverListViewOrTextBox())
            {

                userListView.Visibility = Visibility.Hidden;
            }
            if (!IsMouseOverListViewOrTextBox2())
            {

                userListView2.Visibility = Visibility.Hidden;
            }
        }
        private int StringComboBoxToInt()
        {
            try
            {
                int ud = -1;
          
                string sqlExpression1 = $"DECLARE @searchString NVARCHAR(255) = '{ComboBox.Text}'; SELECT Id_Passport FROM Passport WHERE  FirstName = SUBSTRING(@searchString, 1, CHARINDEX(' ', @searchString) - 1)  AND SecondName = SUBSTRING(@searchString, CHARINDEX(' ', @searchString) +1, CHARINDEX(' ', @searchString)-1)";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ud = reader.GetInt32(0);
                    }
                    reader.Close();
                }
                else
                {
                    throw new Exception("Ошибка с выбором сотрудника");
                }
                connection.Close();
                return ud;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}");
                return 0;
            }

        }
        private int StringComboBoxToInt(string text, string text2, string table, string column )
        {
            try
            {
                int ud = -1;

                string sqlExpression1 = $"DECLARE @searchString NVARCHAR(255) = '{text+" "+text2}'; SELECT {column} FROM {table} WHERE  FirstName = SUBSTRING(@searchString, 1, CHARINDEX(' ', @searchString) - 1)  AND SecondName = SUBSTRING(@searchString, CHARINDEX(' ', @searchString) +1, CHARINDEX(' ', @searchString)-1)";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression1, connection);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ud = reader.GetInt32(0);
                    }
                    reader.Close();
                }
                else
                {
                    throw new Exception("Ошибка с выбором сотрудника");
                }
                connection.Close();
                return ud;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Возникла ошибка: {ex.Message}");
                return 0;
            }

        }
        private void EditInsert()
        {
            string sqlQueryy = "UPDATE Applications SET Staff = @Te, Application_place = @Application_place, Status = @Status, Type_of_application = @Type_of_application, Description_of_problem = @Description_of_problem, Date_of_create = @Date_of_create, Date_of_close = @Date_of_close WHERE Id_applications = @Id_applications";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int IdType = StringComboBoxToInt();
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQueryy, connection);

                   if (Id_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле ID не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Id_applications", Id_TextBox.Text);
                    }
                    if (IdType == -1)
                    {
                        MessageBox.Show("Ошибка при выборе сотрудника");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Te", IdType);
                    }

                    if (Place_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Адрес установки не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Application_place", Place_TextBox.Text);
                    }

                    if (Status_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Статус не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Status", Status_TextBox.Text);
                    }

                    if (Problem_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Тип проблемы не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Type_of_application", Problem_TextBox.Text);
                    }

                    if (Coment_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Коментарий не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Description_of_problem", Coment_TextBox.Text);
                    }

                    if (Create_TexbBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Дата создания не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Date_of_create", Create_TexbBox.Text);
                    }

                    if (Closee_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Дата закрытия не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Date_of_close", Closee_TextBox.Text);
                    }

                    int number = command.ExecuteNonQuery();
                    MessageBox.Show($"Добавлено объектов: {number}");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка при изменении данных: " + ex.Message);
                }
                CRMDAtaDrid();
            }
        }
        private void Search()
        {
            List<Applicatoins> equList = new List<Applicatoins>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

               
                string firstName = Familiya.Text;
                if (!firstName.All(char.IsLetter))
                {
                    MessageBox.Show("Фамилия должна содержать только буквы.");
                    return;
                }

                
                string sqlExpression = $"SELECT Id_applications,Post,FirstName,LastName,SecondName,Number_Of_Phone,Application_place,Status,Type_of_application, Description_of_problem, Date_of_create,Date_of_close\r\nFROM Applications \r\nINNER JOIN Staff ON Staff.Id_staff = Applications.Staff \r\nINNER JOIN Abonent ON Abonent.Id_Abonent = Applications.Abonent \r\nINNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport \r\nWHERE FirstName = @FirstName and Number_Of_phone = @NumberPhone";

                using (SqlCommand command = new SqlCommand(sqlExpression, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", Familiya.Text);
                    command.Parameters.AddWithValue("@NumberPhone", NumberPhone.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Абонент с указанной фамилией и номером телефона не найден.");
                            return;
                        }

                        while (reader.Read())
                        {
                            Applicatoins app = new Applicatoins();
                            app.id_Applicatoins = reader.GetInt32(0);
                            app.Worker = reader.GetString(1);
                            app.FirstName = reader.GetString(2);
                            app.LastName = reader.GetString(3);
                            app.SecondName = reader.GetString(4);
                            app.Number_Of_Phone = reader.GetString(5);
                            app.application_place = reader.GetString(6);
                            app.status = reader.GetString(7);
                            app.Type_of_application = reader.GetString(8);
                            try
                            {
                                app.description_of_problem = reader.GetString(9);
                            }
                            catch
                            {
                                app.description_of_problem = "-";
                            }
                            app.date_create = reader.GetDateTime(10).ToString("dd.MM.yyyy");
                            app.date_close = reader.GetDateTime(11).ToString("dd.MM.yyyy");

                            equList.Add(app);
                        }
                    }
                }
            }

            CRMDataGrid.ItemsSource = equList;
        }

        private void l2_Initialized(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlExpression1 = $"SELECT Role, FirstName, LastName, NamePhoto FROM [User] WHERE Number_of_phone = '{MainWindow.Global.Login}' ";
                using (SqlCommand command = new SqlCommand(sqlExpression1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            level = reader.GetInt32(0);
                            l2.Content = reader.GetString(1);

                            l2.Content += " " + reader.GetString(2);

                            Global2.FN = l2.Content.ToString();
                        }
                    }
                }
            }


            switch (level)
            {
                case 1:
                    Active.IsEnabled = false;
                  
                    Support.IsEnabled = false;
                    break;
                case 2:
                    Active.IsEnabled = false;
                  
                    Support.IsEnabled = false;
                    Billing.IsEnabled = false;
                    break;
                case 3:
                    Active.IsEnabled = false;
                    Billing.IsEnabled = false;
                    break;
                case 4:
                    Active.IsEnabled = false;
                    Billing.IsEnabled = false;
                    break;
                case 5:
                  
                    Support.IsEnabled = false;
                    CRM.IsEnabled = false;
                    break;
                case 6:
                    break;
                case 7:
                    Support.IsEnabled = false;
                    Billing.IsEnabled = false;
                    break;
            }
        }
       
        private void ImageUser()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlExpression = $"SELECT NamePhoto FROM [User] WHERE Number_of_phone = '{MainWindow.Global.Login}' ";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Photo.Source = new BitmapImage(new Uri($"userphoto/{reader.GetString(0).Trim()}.png", UriKind.Relative));                     
                    }                   
                }
                else
                {
                    MessageBox.Show($"Ошибка подключения");
                    this.Close();
                }
            }
        }
     
        private void ButtonAbonent_Click(object sender, RoutedEventArgs e)
        {

            Abonent.Visibility = System.Windows.Visibility.Visible;
            CRMM.Visibility = System.Windows.Visibility.Hidden;
            BillingButton.Visibility = System.Windows.Visibility.Hidden;
            GridTextBox.Visibility = System.Windows.Visibility.Hidden;
            UserSupport.Visibility = System.Windows.Visibility.Hidden;
            AppointmentStaff.Visibility = System.Windows.Visibility.Hidden;

        }

    

        private void VCERadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            List<Abonents> equipmentList = new List<Abonents>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
         
                string sqlQuery = "SELECT Id_Abonent, FirstName ,LastName, Name_Service, Personal_account, Number_Of_phone, eMail, ContractNumber,TypeOfAgreement,Reason_for_termination_of_the_contract FROM Abonent " +
                    "INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport " +
                    "INNER JOIN Service ON Service.Id_Services = Abonent.Service ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Abonents cr = new Abonents();

                            cr.Id_Abonent = reader.GetInt32(0);
                            cr.FirstName = reader.GetString(1);
                            cr.LastName = reader.GetString(2);
                            cr.Name_Service = reader.GetString(3);
                            cr.Personal_account = reader.GetString(4);
                            cr.Number_Of_phone = reader.GetString(5);
                            cr.eMail = reader.GetString(6);
                            cr.ContractNumber = reader.GetString(7);
                            cr.TypeOfAgreement = reader.GetString(8);
                            cr.Reas_for_term_of_contr = reader.GetString(9);

                            equipmentList.Add(cr);
                        }
                    }
                }
            }
            Read.ItemsSource = equipmentList;
        }

        private void Active1_Checked(object sender, RoutedEventArgs e)
        {
            List<Abonents> equipmentList = new List<Abonents>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
           
                string sqlQuery = "SELECT Id_Abonent, FirstName ,LastName, Name_Service, Personal_account, Number_Of_phone, eMail, ContractNumber,TypeOfAgreement,Reason_for_termination_of_the_contract FROM Abonent " +
                    "INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport " +
                    "INNER JOIN Service ON Service.Id_Services = Abonent.Service  where Reason_for_termination_of_the_contract = 'нет' ";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Abonents cr = new Abonents();

                            cr.Id_Abonent = reader.GetInt32(0);
                            cr.FirstName = reader.GetString(1);
                            cr.LastName = reader.GetString(2);
                            cr.Name_Service = reader.GetString(3);
                            cr.Personal_account = reader.GetString(4);
                            cr.Number_Of_phone = reader.GetString(5);
                            cr.eMail = reader.GetString(6);
                            cr.ContractNumber = reader.GetString(7);
                            cr.TypeOfAgreement = reader.GetString(8);
                            cr.Reas_for_term_of_contr = reader.GetString(9);

                            equipmentList.Add(cr);
                        }
                    }
                }
            }
            Read.ItemsSource = equipmentList;
        }

        private void Control_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void nonActive_Checked(object sender, RoutedEventArgs e)
        {
            List<Abonents> equipmentList = new List<Abonents>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
       
                string sqlQuery = "SELECT Id_Abonent, FirstName ,LastName, Name_Service, Personal_account, Number_Of_phone, eMail, ContractNumber,TypeOfAgreement,Reason_for_termination_of_the_contract FROM Abonent " +
                   "INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport " +
                   "INNER JOIN Service ON Service.Id_Services = Abonent.Service where Reason_for_termination_of_the_contract = 'Переезд'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Abonents cr = new Abonents();

                            cr.Id_Abonent = reader.GetInt32(0);
                            cr.FirstName = reader.GetString(1);
                            cr.LastName = reader.GetString(2);
                            cr.Name_Service = reader.GetString(3);
                            cr.Personal_account = reader.GetString(4);
                            cr.Number_Of_phone = reader.GetString(5);
                            cr.eMail = reader.GetString(6);
                            cr.ContractNumber = reader.GetString(7);
                            cr.TypeOfAgreement = reader.GetString(8);
                            cr.Reas_for_term_of_contr = reader.GetString(9);
                            
                            equipmentList.Add(cr);
                        }
                    }
                }
            }
            Read.ItemsSource = equipmentList;
        }

        private void ListBoxEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
 
        }

        private void CRM_Button_Click(object sender, RoutedEventArgs e)
        {
            CRMM.Visibility = System.Windows.Visibility.Visible;
          
            Abonent.Visibility = System.Windows.Visibility.Hidden;
            BillingButton.Visibility = System.Windows.Visibility.Hidden;
            GridTextBox.Visibility = System.Windows.Visibility.Visible;
            UserSupport.Visibility = System.Windows.Visibility.Hidden;
            AppointmentStaff.Visibility = System.Windows.Visibility.Hidden;
        }

        private void CRMDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Done = false;
            ComboBox.Visibility = Visibility.Visible;
            ID.Content = "ID";
            
            Id_TextBox.Visibility=Visibility.Visible;
            Applicatoins edit = (Applicatoins)CRMDataGrid.SelectedItem;
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                {
                    ComboBox.Text = edit.Worker.ToString();
                    Id_TextBox.Text= edit.id_Applicatoins.ToString();
                    Familiya_TextBox.Text =edit.FirstName.ToString();
                    Name_TextBox.Text =edit.SecondName.ToString();
                    Place_TextBox.Text = edit.application_place.ToString();
                    Status_TextBox.Text =edit.status.ToString();
                    Problem_TextBox.Text = edit.Type_of_application.ToString();
                    Coment_TextBox.Text = edit.description_of_problem.ToString();
                    Create_TexbBox.Text = edit.date_create.ToString();
                    Closee_TextBox.Text = edit.date_close.ToString();
                  
                }
            }
        }

    

        private void Editing_Button_Click_1(object sender, RoutedEventArgs e)
        {
            ID.Visibility = Visibility.Visible;
            Done = false;
            EditInsert();
            Id_TextBox.Visibility = Visibility.Visible;
            ID.Content = "ID";
        }

      

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            Search();            
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            CRMDataGrid.Columns.Clear();
            CRMDAtaDrid();
            Familiya.Text = string.Empty;
            NumberPhone.Text = string.Empty;
        }
       
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BillingsButton_Click(object sender, RoutedEventArgs e)
        {
            BillingButton.Visibility = System.Windows.Visibility.Visible;
            otchet.Visibility = System.Windows.Visibility.Visible;
            CRMM.Visibility = System.Windows.Visibility.Hidden;
            Abonent.Visibility = System.Windows.Visibility.Hidden;
            GridTextBox.Visibility = System.Windows.Visibility.Hidden;
            UserSupport.Visibility = System.Windows.Visibility.Hidden;
            AppointmentStaff.Visibility = System.Windows.Visibility.Hidden;
            ViewOtchet.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Photo_Initialized(object sender, EventArgs e)
        {
            ImageUser();
        }
       
        private void ClearTextBox()
        {
            Id_TextBox.Text = string.Empty;
            Familiya_TextBox.Text = string.Empty;
            Name_TextBox.Text = string.Empty;
            Place_TextBox.Text = string.Empty;
            Status_TextBox.Text = string.Empty;
            Problem_TextBox.Text = string.Empty;
            Coment_TextBox.Text = string.Empty;
            Create_TexbBox.Text = string.Empty;
            Closee_TextBox.Text = string.Empty;
        }
        private void ReadOnlyF()
        {
            Id_TextBox.IsReadOnly = false;
            Familiya_TextBox.IsReadOnly = false;
            Name_TextBox.IsReadOnly = false;
            Place_TextBox.IsReadOnly = false;
            Status_TextBox.IsReadOnly = false;
            Problem_TextBox.IsReadOnly = false;
            Coment_TextBox.IsReadOnly = false;
            Create_TexbBox.IsReadOnly = false;
            Closee_TextBox.IsReadOnly = false;
        }
        private void ReadOnlyT()
        {
            Id_TextBox.IsReadOnly = true;
            Familiya_TextBox.IsReadOnly = true;
            Name_TextBox.IsReadOnly = true;
            Place_TextBox.IsReadOnly = true;
            Status_TextBox.IsReadOnly = true;
            Problem_TextBox.IsReadOnly = true;
            Coment_TextBox.IsReadOnly = true;
            Create_TexbBox.IsReadOnly = true;
            Closee_TextBox.IsReadOnly = true;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            {
                {
                     Done =true ;
                    ComboBox.Visibility = Visibility.Visible ;
                    Id_TextBox.Visibility = Visibility.Hidden ;
                    ClearTextBox();
                    ReadOnlyF();
                    ID.Visibility = Visibility.Hidden ;
                    ID.Content = "Сотрудник";
                 
                }                
            }
        }

        private void Done_Button_Click(object sender, RoutedEventArgs e)
        {
           
            string sqlQueryy =  "INSERT INTO Applications (Id_applications, Staff, Abonent, Application_place, Status, Type_of_application, Description_of_problem, Date_of_create, Date_of_close) VALUES ((SELECT ISNULL(MAX(Id_applications), 0) + 1 FROM Applications), @Staff, @ID, @Application_place, @Status, @Type_of_application, @Description_of_problem, @Date_of_create, @Date_of_close)";
            int IdType = StringComboBoxToInt();
            int IdType2 = StringComboBoxToInt(Familiya_TextBox.Text, Name_TextBox.Text, "Passport", "Id_Passport") ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQueryy, connection);
                   
                    if (ComboBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле ID не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Staff", IdType);
                    }
                    if (Familiya_TextBox.Text == "" || Name_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поля Имя и Фамилия  не могут быть пустыми");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@ID", IdType2);
                    }
                    if (Place_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Адрес установки не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Application_place", Place_TextBox.Text);
                    }

                    if (Status_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Статус не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Status", Status_TextBox.Text);
                    }

                    if (Problem_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Тип проблемы не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Type_of_application", Problem_TextBox.Text);
                    }

                    if (Coment_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Коментарий не может быть пустой");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Description_of_problem", Coment_TextBox.Text);
                    }                
                    if (Create_TexbBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Дата создания не может быть пустым");
                        check = true;
                    }
                    else
                    {
                        
                        command.Parameters.AddWithValue("@Date_of_create", Create_TexbBox.Text);
                    }

                    if (Closee_TextBox.Text == "" && !check)
                    {
                        MessageBox.Show("Поле Дата закрытия не может быть пустым");
                        check = true;
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@Date_of_close", Closee_TextBox.Text);
                    }


                    int number = command.ExecuteNonQuery();
                    MessageBox.Show($"Добавлено объектов: {number}");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка при изменении данных: " + ex.Message);
                }
                CRMDAtaDrid();

            }
           
        }
        private void ClearCRM_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CRMDataGrid.SelectedItem != null)
            {

                Applicatoins selectedEquipment = (Applicatoins)CRMDataGrid.SelectedItem;


                string sqlExpression = "DELETE FROM Applications WHERE Id_applications = @ID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.Parameters.AddWithValue("@ID", selectedEquipment. id_Applicatoins);

                    try
                    {

                        int number = command.ExecuteNonQuery();


                        CRMDAtaDrid();


                        MessageBox.Show($"Удалено объектов: {number}");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show($"Ошибка удаления объекта: {ex.Message}");
                    }
                }
            }
            else
            {

                MessageBox.Show("Выберите объект для удаления.");
            }
        }

        private void userListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {            
                if (userListView.SelectedItem != null)
                {
                    User selectedUser = (User)userListView.SelectedItem;            
                 //   MessageBox.Show($"Selected user: {selectedUser.SecondName}");
                    Familiya_TextBox.Text = selectedUser.SecondName;
                }            
        }
   
        private void userListView2_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (userListView2.SelectedItem != null)
            {
                User selectedUser = (User)userListView2.SelectedItem;
              //  MessageBox.Show($"Selected user: {selectedUser.FirstName}");
                Name_TextBox.Text = selectedUser.FirstName;
            }
        }

        private void LogotipTHCButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Activi_Button_Click(object sender, RoutedEventArgs e)
        {
            AppointmentStaff.Visibility = System.Windows.Visibility.Visible;
            UserSupport.Visibility = System.Windows.Visibility.Hidden;
            Abonent.Visibility = System.Windows.Visibility.Hidden;
            CRMM.Visibility = System.Windows.Visibility.Hidden;
            BillingButton.Visibility = System.Windows.Visibility.Hidden;
            GridTextBox.Visibility = System.Windows.Visibility.Hidden;
           
        }

        private void SupportingUserButton_Click(object sender, RoutedEventArgs e)
        {
            UserSupport.Visibility = System.Windows.Visibility.Visible;
            Abonent.Visibility = System.Windows.Visibility.Hidden;
            CRMM.Visibility = System.Windows.Visibility.Hidden;
            BillingButton.Visibility = System.Windows.Visibility.Hidden;
            GridTextBox.Visibility = System.Windows.Visibility.Hidden;
            AppointmentStaff.Visibility = System.Windows.Visibility.Hidden;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            Button clickedButton = sender as Button;

            InfoApplications.Items.Clear();
            if (clickedButton != null)
            {
               
                string timeText = clickedButton.Content.ToString();             
                string[] parts = timeText.Split('-');
                string time = parts[0]; 
                string status = parts[1].Trim();

              
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                  
                    string query = "SELECT FirstName,SecondName,LastName,Post,Number_Of_Phone,Application_place,Status,Type_of_application, Description_of_problem, Date_of_create,Date_of_close\r\nFROM Applications\r\nINNER JOIN Staff ON Staff.Id_staff = Applications.Staff \r\nINNER JOIN Abonent ON Abonent.Id_Abonent = Applications.Abonent \r\nINNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport \r\nWHERE EXISTS (\r\n    SELECT 1\r\n    FROM Support_User\r\n     \r\n    WHERE Applications.Id_applications = Support_User.Id_Applications AND Support_User.Time_Support = @time)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@time", time);


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                string dateOfCreate = ((DateTime)reader["Date_of_create"]).ToString("dd.MM.yyyy");
                                string dateOfClose = ((DateTime)reader["Date_of_close"]).ToString("dd.MM.yyyy");

                                string info = $"Время: {time},Статус:{status}\n";
                                info += $"ФИО:{reader["FirstName"]} {reader["SecondName"]} {reader["LastName"]}\nПост:{reader["Post"]}\nНомер телефона:{reader["Number_Of_Phone"]}\nМесто установки:{reader["Application_place"]}\nСтатус:{reader["Status"]}\nТип проблемы:{reader["Type_of_application"]}\nКомментарий:{reader["Description_of_problem"]}\nДата создания:{dateOfCreate}\nДата закрытия:{dateOfClose}";

                                InfoApplications.Items.Add(info);
                            }
                            else
                            {
                                MessageBox.Show("Информация не найдена.");
                            }
                        }
                    }
             
                }
                
            }
            
        }

        private void CSV_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_Initialized(object sender, EventArgs e)
        {           
                           List<string> workerList = Workers2();
                            List.ItemsSource =  workerList;                                                             
        }

        private void ListBox_Initialized_1(object sender, EventArgs e)
        {
            List<string> timeValues = new List<string>
    { 
        
        "00:00",
        "01:00",
        "02:00",
        "03:00",
        "04:00",
        "05:00",
        "06:00",
        "07:00",
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00",
        "16:00",
        "17:00",
        "18:00",
        "19:00",
        "20:00",
        "21:00",
        "22:00",
        "23:00",
    };
            list2.ItemsSource = timeValues;
        }

        private void DateList_Initialized(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            List<string> dateList = new List<string>();
            for (int i = -10; i <= 10; i++)
            {
                string dateWithoutTime = currentDate.AddDays(i).ToString("dd.MM.yyyy");
                dateList.Add(dateWithoutTime);
            }
            DateList.ItemsSource = dateList;
        }

        private void ReasonList_Initialized(object sender, EventArgs e)
        {
            List<string> Reasons = new List<string>() ;           
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlExpression1 = $"select Name from Subspecies_of_services";
            SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
            SqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    string fullName = $"{reader1.GetString(0)}";

                    ReasonList.Items.Add(fullName);
                }
                reader1.Close();
            }

       
        }
    }
        public class nVP
        {
            public string Date {get; set;}
            public string Time { get; set; }
            public string Reason { get; set; }
            public string Worker { get; set; }
        }
       private void LoadListBoxik()
        {
            {
                DateTime Date0;
                TimeSpan Time0;
                List<nVP> nvpList = new List<nVP>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = "SELECT Date, Time, Reason, Worker FROM Staff_Assignment";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Date0 = reader.GetDateTime(0);
                                Time0 = reader.GetTimeSpan(1);
                                string formattedDate = Date0.ToString("dd.MM.yyyy") ;
                                string formattedTime = Time0.ToString(@"hh\:mm");
                                nVP nVPl = new nVP
                                {                                   
                                    Date = formattedDate,
                                    Time = formattedTime,
                                    Reason = reader.GetString(2),
                                    Worker = reader.GetString(3)


                                };

                                nvpList.Add(nVPl);

                            }

                        }
                    }
                }

                Created.ItemsSource = nvpList;


            }
        }
        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        private void DateList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
             str1 = DateList.SelectedItem.ToString();
           
             list2.IsEnabled = true;           
             DateList.IsEnabled = false;
        }

        private void list2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
         {
             str2 = list2.SelectedItem.ToString();
             ReasonList.IsEnabled = true;
             list2.IsEnabled = false; 
        }

        private void ReasonList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
         {

             str3 =  ReasonList.SelectedItem.ToString();          
             ReasonList.IsEnabled = false;
             List.IsEnabled= true;
            
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             str4 = List.SelectedItem.ToString();     
             List.IsEnabled = false;
             DateList.IsEnabled = true;
            InsertToStaffAns();
            LoadListBoxik();
         str1 = "";
         str2 = "";
         str3 = "";
         str4 = "";
        }
        private void InsertToStaffAns()
        {
            string sqlQueryy = "INSERT INTO Staff_Assignment (id_staff_assignment, Date, Time, Reason, Worker) VALUES ((SELECT ISNULL(MAX(id_staff_assignment), 0) + 1 FROM Staff_Assignment), @Date, @Time, @Reason, @Worker)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQueryy, connection);                
                        command.Parameters.AddWithValue("@Date", str1);                                       
                        command.Parameters.AddWithValue("@Time", str2);                                       
                        command.Parameters.AddWithValue("@Reason", str3);                                                       
                        command.Parameters.AddWithValue("@Worker", str4);
                    command.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка при изменении данных: " + ex.Message);
                }
                CRMDAtaDrid();

            }
        }
        private void POzadoljnocti_Checked(object sender, RoutedEventArgs e)
        {

            List<BillingAbonnent> AbonentList = new List<BillingAbonnent>();
            List<HistoryBillingAbonnent> AbonentsList = new List<HistoryBillingAbonnent>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT FirstName,SecondName,Personal_account,Name_Tarif,Cost_package,Debt_information FROM Abonent INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport WHERE Debt_information > '0 руб'";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            BillingAbonnent Abonent = new BillingAbonnent
                            {

                                fiirsName = reader.GetString(0),
                                seecondName = reader.GetString(1),
                                personalAccount = reader.GetString(2),
                                Nametarif = reader.GetString(3),
                                costPackage = reader.GetInt32(4),
                                deptInformation = reader.GetString(5),


                            };

                            AbonentList.Add(Abonent);

                        }

                    }
                }
                DataGrid_one.ItemsSource = AbonentList;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Date_payment, Suma_payment, Balance, History_payment FROM Abonent WHERE Debt_information > '0 руб'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            HistoryBillingAbonnent Abonents = new HistoryBillingAbonnent
                            {

                                datePayment = reader.GetDateTime(0).ToString("dd.MM.yyyy"),
                                sumaPayment = reader.GetInt32(1),
                                balance = reader.GetInt32(2),
                                historyPayment = reader.GetString(3),



                            };

                            AbonentsList.Add(Abonents);

                        }

                    }
                }
                DataGrid_two.ItemsSource = AbonentsList;
            }


        }// в новый проект

        private void POTarify_Checked_1(object sender, RoutedEventArgs e)
        {
            List<BillingAbonnent> AbonentList = new List<BillingAbonnent>();
            List<HistoryBillingAbonnent> AbonentsList = new List<HistoryBillingAbonnent>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT FirstName,SecondName,Personal_account,Name_Tarif,Cost_package,Debt_information FROM Abonent INNER JOIN Passport ON Passport.Id_Passport = Abonent.Passport ORDER BY Cost_Package  ASC";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            BillingAbonnent Abonent = new BillingAbonnent
                            {

                                fiirsName = reader.GetString(0),
                                seecondName = reader.GetString(1),
                                personalAccount = reader.GetString(2),
                                Nametarif = reader.GetString(3),
                                costPackage = reader.GetInt32(4),
                                deptInformation = reader.GetString(5),



                            };

                            AbonentList.Add(Abonent);

                        }

                    }
                }
                DataGrid_one.ItemsSource = AbonentList;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlQuery = "SELECT Date_payment, Suma_payment, Balance, History_payment FROM Abonent ORDER BY Cost_Package  ASC";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            HistoryBillingAbonnent Abonents = new HistoryBillingAbonnent
                            {
                                datePayment = reader.GetDateTime(0).ToString("dd.MM.yyyy"),
                                sumaPayment = reader.GetInt32(1),
                                balance = reader.GetInt32(2),
                                historyPayment = reader.GetString(3),



                            };

                            AbonentsList.Add(Abonents);

                        }

                    }
                }
                DataGrid_two.ItemsSource = AbonentsList;
            }
        }// в новый проект

        private void SearchAbonent_Button_Click(object sender, RoutedEventArgs e)
        {
            SerchBillingAbonent();
        }

        private void ViewPlateji_Button_Click(object sender, RoutedEventArgs e)
        {
            BillAbonent();
            otchet.Visibility = System.Windows.Visibility.Visible;
            ViewOtchet.Visibility = System.Windows.Visibility.Hidden;
            na.Visibility = System.Windows.Visibility.Visible;
            fa.Visibility = System.Windows.Visibility.Visible;
            seaabon.Visibility = System.Windows.Visibility.Visible;
            FamiliyaSerch.Visibility = System.Windows.Visibility.Visible;
            NameSerch.Visibility = System.Windows.Visibility.Visible;
            searching.Visibility = System.Windows.Visibility.Visible;


        }



    
        

        private void InfoApplications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewOtChet_Button_Click(object sender, RoutedEventArgs e)
        {
            otchet.Visibility = System.Windows.Visibility.Hidden;
            ViewOtchet.Visibility = System.Windows.Visibility.Visible;
            na.Visibility = System.Windows.Visibility.Hidden;
            fa.Visibility = System.Windows.Visibility.Hidden;
            seaabon.Visibility = System.Windows.Visibility.Hidden;
            FamiliyaSerch.Visibility = System.Windows.Visibility.Hidden;
            NameSerch.Visibility = System.Windows.Visibility.Hidden;
            searching.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SerchByDate();
        }
    }
    }
    

