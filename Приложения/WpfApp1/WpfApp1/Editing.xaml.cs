using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Editing.xaml
    /// </summary>
    public partial class Editing : Window
    {
        string connectionString = "Server=.\\SQLEXPRESS;Database=database_PO;User id=sa;Password=Pa$$w0rd;Encrypt=false";
        //string connectionString = "Server=DESKTOP-7AS9I4L\\SQLEXPRESS;Database=database_PO;Trusted_Connection=True;";
        public Editing()
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
        bool check = false;
        public class Equipment
        {
            public int Id_Equipment { get; set; }
            public string Name_of_type { get; set; }
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
                    string sqlQuery = "SELECT Id_Equipment, Name_of_type, List_of_number, Port, Name, MAC, Inventory_number, Ip, Serial_number, Adress_of_build, Place_of_build, Connection_point " +
                       "FROM Equipment " +
                       "INNER JOIN Type_of_equipment ON Type_of_equipment.Id_Type = Equipment.Type_Of_Equipment ";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               
                                Equipment equipment = new Equipment
                                {

                                    Id_Equipment = reader.GetInt32(0),
                                    Name_of_type = reader.GetString(1),
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
        private void SearchMACADRES()
        {
            try
            {
                string MAC = t4.Text.Replace(" ", "");

                if (MAC.Length != 12 && MAC.Length != 17) 
                {
                    throw new Exception("Неправильный формат MAC адресса");
                }

              
                MAC = MAC.Replace("-", "");

                if (!System.Text.RegularExpressions.Regex.IsMatch(MAC, "^[0-9A-Fa-f]+$"))
                {
                    throw new Exception("Неправильный формат MAC адресса");
                }

              
                string search = string.Join("-", Enumerable.Range(0, MAC.Length / 2).Select(i => MAC.Substring(i * 2, 2)));
                t4.Text = search;
            }
            catch 
            {
                throw new Exception("Неправильный формат MAC адресса");
            }
        }


        private void CheckIp()
        {
            string ip = t6.Text;
            if (!ip.Contains("."))
            {
                throw new Exception("Ошибка при написании ip адресса");
            }
            int po = 0;
            for (int n = 0; n < ip.Length; n++)
            {
                if (ip[n] == '.')
                {
                    po++;
                }
            }
            if (po != 3)
            {
                throw new Exception("Ошибка при написании ip адресса");
            }
            string[] cusoc = ip.Split(new char[] { '.' });
            int i = 0;
            foreach (string s in cusoc)
            {
                try
                {
                    int j = Convert.ToInt32(s);
                    if (j > 255 || j < 0)
                    {
                        throw new Exception("Ошибка при написании ip адресса");
                    }
                }
                catch
                {
                    throw new Exception("Ошибка при написании ip адресса");
                }
                i++;
            }
        }
        private int ports {  get; set; }
        private void EditInsert()
        {
            string sqlExpression = @"UPDATE Equipment 
                         SET Type_of_equipment = @Te, 
                             List_of_number = @Ln, 
                             Port = @P, 
                             Name = @Na, 
                             MAC = @M, 
                             Inventory_number = @In, 
                             Ip = @Ip, 
                             Serial_number = @Sn, 
                             Adress_of_build = @Ab, 
                             Place_of_build = @Pb, 
                             Connection_point = @Cp
                         WHERE Id_Equipment = @ID";


                string sqlQuery = "SELECT max(Id_Port) FROM Ports ";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command1 = new SqlCommand(sqlQuery, connection))
                    {
                        SqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                ports = reader1.GetInt32(0);
                            }
                            reader1.Close();
                        }
                    }
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);

                        int IdType = StringComboBoxToInt();
                        if (IdType == -1)
                        {
                            MessageBox.Show("Ошибка при выборе типа оборудования");
                        }
                        else
                        {
                        command.Parameters.AddWithValue("@ID", l1.Content);
                        command.Parameters.AddWithValue("@Te", IdType);
                        }
                        
                    if (t1.Text == "" && !check)
                        {
                            MessageBox.Show("Поле лист номер не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Ln", t1.Text);
                        }

                        if (int.Parse(t2.Text) > ports || int.Parse(t2.Text) <= 0)
                        {
                            MessageBox.Show($"Введите допустимое количество портов от 1 до {ports}");
                        }

                        if (t2.Text == "" && !check)
                        {
                            MessageBox.Show("Нужно ввести ID Порта");
                            check = true;
                        }
                        else
                        {

                            command.Parameters.AddWithValue("@P", t2.Text);
                        }
                        if (t3.Text == "" && !check)
                        {
                            MessageBox.Show("Поле название не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Na", t3.Text);
                        }
                        SearchMACADRES();
                        if (t4.Text == "" && !check)
                        {
                            MessageBox.Show("Поле MAC адресс не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@M", t4.Text);
                        }
                        if (t5.Text == "" && !check)
                        {
                            MessageBox.Show("Поле инвентарный номер не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@In", t5.Text);
                        }
                        CheckIp();

                        if (t6.Text == "" && !check)
                        {
                            MessageBox.Show("Поле Ip адрес не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Ip", t6.Text);
                        }
                        if (t7.Text == "" && !check)
                        {
                            MessageBox.Show("Поле Серийный номер не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Sn", t7.Text);
                        }
                        if (t8.Text == "" && !check)
                        {
                            MessageBox.Show("Поле Адрес установки не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Ab", t8.Text);
                        }
                        if (t9.Text == "" && !check)
                        {
                            MessageBox.Show("Поле Место установки не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Pb", t9.Text);
                        }
                        if (t10.Text == "" && !check)
                        {
                            MessageBox.Show("Поле Точка установки не может быть пустой");
                            check = true;
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@Cp", t10.Text);
                        }
                        check = false;


                        int number = command.ExecuteNonQuery();
                        MessageBox.Show($"Добавлено объектов: {number}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    LoadDataGrid();
                }
            
         }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Panel Editing = new Panel();

            this.Close();
            Editing.ShowDialog();
        }

        

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EditInsert();  
        }
            private void Read_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }
        
        private void Read_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Equipment edit = (Equipment)Read.SelectedItem;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                
                {
                    
              
                    l1.Content = edit.Id_Equipment.ToString();
                    ComboBox.Text = edit.Name_of_type.ToString();
                    t1.Text = edit.List_of_number.ToString();
                    t2.Text = edit.Port.ToString();
                    t3.Text = edit.Name.ToString();
                    t4.Text = edit.MAC.ToString();
                    t5.Text = edit.Inventory_number.ToString();
                    t6.Text = edit.Ip.ToString();
                    t7.Text = edit.Serial_number.ToString();
                    t8.Text = edit.Adress_of_build.ToString();
                    t9.Text = edit.Place_of_build.ToString();
                    t10.Text = edit.Connection_point.ToString();
                }
            }

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
           
            
                if (Read.SelectedItem != null)
                {
                   
                    Equipment selectedEquipment = (Equipment)Read.SelectedItem;

                   
                    string sqlExpression = "DELETE FROM Equipment WHERE Id_Equipment = @ID";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.AddWithValue("@ID", selectedEquipment.Id_Equipment);

                        try
                        {
                          
                            int number = command.ExecuteNonQuery();

                        
                            LoadDataGrid();

                           
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

        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    
}
