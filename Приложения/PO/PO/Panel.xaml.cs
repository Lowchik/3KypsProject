using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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


namespace PO
{

    /// <summary>
    /// Логика взаимодействия для Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        public static class Global2
        {
            public static string FN { get; set; }
          
        }

      
   
       
        
        public Panel()
        {
            InitializeComponent();
           
        }
          string connectionString = "Server=.\\SQLEXPRESS;Database=database_PO;User id=sa;Password=Pa$$w0rd;Encrypt=false";
        //string connectionString = "Server=DESKTOP-7AS9I4L\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        //string connectionString = "Server=DESKTOP-RUBV4AP\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";

        private void l1_Initialized(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();               
                string sqlExpression1 = $"SELECT FirstName, LastName FROM [User] WHERE Number_of_phone = '{MainWindow.Global.Login}' ";
                using (SqlCommand command = new SqlCommand(sqlExpression1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l1.Content = reader.GetString(0);
                            
                            l1.Content += " " + reader.GetString(1);

                            Global2.FN = l1.Content.ToString();
                        }
                    }
                }
            }
        }
        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
           
            MainWindow Panel = new MainWindow();
            this.Close();
            Panel.ShowDialog();
        }

       

        private void MenuAbonenta_Click(object sender, RoutedEventArgs e)
        {
            MenuAbonenta Panel = new MenuAbonenta();
            this.Close();
            Panel.ShowDialog();
        }

        private void l2_Initialized(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlExpression1 = $"SELECT Name_of_role FROM[User] INNER JOIN Roles ON Roles.Id_Roles = [User].Role  WHERE Number_of_phone = '{MainWindow.Global.Login}'";




                using (SqlCommand command = new SqlCommand(sqlExpression1, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l2.Content = reader.GetString(0);



                            Global2.FN = l2.Content.ToString();
                        }
                    }
                }
            }
        }
    }
}
