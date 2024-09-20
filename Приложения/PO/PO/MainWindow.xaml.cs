using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace PO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {


        public static class Global
        {
            public static string Login { get; set; }
            public static string Password { get; set; }
        }




        string connectionString = "Server=.\\SQLEXPRESS;Database=database_PO;User id=sa;Password=Pa$$w0rd;Encrypt=false";
        //string connectionString = "Server=DESKTOP-7AS9I4L\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        //string connectionString = "Server=DESKTOP-RUBV4AP\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredPhoneNumber = login.Text;
            string enteredPassword = pass.Password;

            Global.Login = enteredPhoneNumber;
            Global.Password = enteredPassword;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sqlQuery = $"SELECT Password FROM [User] WHERE Number_of_phone = '{Global.Login}'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        string hashedEnteredPassword = HashPassword(enteredPassword);
                        string hashedPasswordFromDB = (string)command.ExecuteScalar();

                        if (hashedPasswordFromDB != null && hashedPasswordFromDB.Equals(hashedEnteredPassword))
                        {
                            Panel mainWindow = new Panel();
                            this.Close();
                            mainWindow.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Неверный номер телефона или пароль");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
