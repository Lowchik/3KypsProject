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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string connectionString = "Server=.\\SQLEXPRESS;Database=database_PO;User id=sa;Password=Pa$$w0rd;Encrypt=false";
        //string connectionString = "Server=DESKTOP-7AS9I4L\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                string enteredPhoneNumber = Login.Text;
                string enteredPassword = password.Password;

                 Login.Text = enteredPhoneNumber;
                 password.Password = enteredPassword;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string sqlQuery = $"SELECT Password FROM [User] WHERE Number_of_phone = '{Login.Text}'";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            string hashedEnteredPassword = HashPassword(enteredPassword);
                            string hashedPasswordFromDB = (string)command.ExecuteScalar();

                            if (hashedPasswordFromDB != null && hashedPasswordFromDB.Equals(hashedEnteredPassword))
                            {
                                Panel MainWindow = new Panel();
                                this.Close();
                                MainWindow.ShowDialog();
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
