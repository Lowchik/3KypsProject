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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для infospisok.xaml
    /// </summary>
    public partial class infospisok : Window
    {
        string connectionString = "Server=.\\SQLEXPRESS;Database=database_PO;User id=sa;Password=Pa$$w0rd;Encrypt=false";
        //string connectionString = "Server=DESKTOP-7AS9I4L\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        public infospisok()
        {
            InitializeComponent();
            LoadComboBoxData();
            LoadDataGrid();
        }
        private void LoadComboBoxData()
        {
          

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            if (connection.State == ConnectionState.Open)
            {
                string sqlExpression1 = $"Select * From Type_of_equipment";
                SqlCommand command1 = new SqlCommand(sqlExpression1, connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        ComboBox.Items.Add(reader1.GetString(1));
                    }
                    reader1.Close();
                }

            }
            connection.Close();

        }
        private int StringComboBoxToInt()
        {
            try
            {
                int ud = -1;
               
                string sqlExpression1 = $"Select * from Type_of_equipment where Name_of_type = '{ComboBox.Text}'";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sqlExpression1, connection);
                connection.Open();
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
                    throw new Exception("Ошибка с выбором типа оборудования");
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
        public class Equipment
        {
            public int Id_Equipment { get; set; }
            public int Type_of_equipment { get; set; }
            public string List_of_number { get; set; }
            public int Port { get; set; }
            public string Name { get; set; }
            public string MAC { get; set; }
            public int Inventory_number { get; set; }
            public string Ip { get; set; }
            public string Serial_number { get; set; }
            public string Adress_of_build { get; set; }
            public string Place_of_build { get; set; }
            public string Connection_point { get; set; }
        }
        private void LoadDataGrid()
        {
            try
            {
                List<Equipment> equipmentList = new List<Equipment>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sqlQuery = $"DECLARE @Id_Type INT; SELECT @Id_Type = Id_Type FROM Type_of_equipment WHERE Name_of_type = '{ComboBox.Text}'; SELECT * FROM Equipment WHERE Type_Of_Equipment = @Id_Type;";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int IdType = StringComboBoxToInt();
                                Equipment equipment = new Equipment
                                {

                                    Id_Equipment = reader.GetInt32(0),
                                    Type_of_equipment = reader.GetInt32(1),
                                    List_of_number = reader.GetString(2),
                                    Port = reader.GetInt32(3),
                                    Name = reader.GetString(4),
                                    MAC = reader.GetString(5),
                                    Inventory_number = reader.GetInt32(6),
                                    Ip = reader.GetString(7),
                                    Serial_number = reader.GetString(8),
                                    Adress_of_build = reader.GetString(9),
                                    Place_of_build = reader.GetString(10),
                                    Connection_point = reader.GetString(11)
                                };

                                equipmentList.Add(equipment);
                            }
                        }
                    }
                }

                Read.ItemsSource = equipmentList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных из базы данных: {ex.Message}");
            }
        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Equipment edit = (Equipment)Read.SelectedItem;



            t1.Text = edit.Id_Equipment.ToString();
            t2.Text = edit.List_of_number.ToString();
            t3.Text = edit.Port.ToString();
            t4.Text = edit.Name.ToString();
            t5.Text = edit.MAC.ToString();
            t6.Text = edit.Inventory_number.ToString();
            t7.Text = edit.Ip.ToString();
            t8.Text = edit.Serial_number.ToString();
            t9.Text = edit.Adress_of_build.ToString();
            t10.Text = edit.Place_of_build.ToString();
            t11.Text = edit.Connection_point.ToString();



        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Panel infospisok = new Panel();

            this.Close();
            infospisok.ShowDialog();
        }

   

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }
    }
}
