using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Panel.xaml
    /// </summary>
    public partial class Panel : Window
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void Editing_Click(object sender, RoutedEventArgs e)
        {
            Editing Panel = new Editing();
      
            this.Close();
            Panel.ShowDialog();
        }

        private void Creation_Click(object sender, RoutedEventArgs e)
        {
            Create Panel = new Create();
          
            this.Close();
            Panel.ShowDialog();
        }

       
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Panel = new MainWindow();

            this.Close();
            Panel.ShowDialog();
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            infospisok Panel = new infospisok();

            this.Close();
            Panel.ShowDialog();

        }
    }
}
