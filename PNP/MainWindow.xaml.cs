using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PNP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            string line = "";
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Locale.csv");
            StreamReader sr = new StreamReader(path);
            line = sr.ReadLine();

            while (line != null)
            {
                Variables.FilePath = line;
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            new Window2().Show();
            this.Close();
        }

        private void Setttings_Click(object sender, RoutedEventArgs e)
        {
            new Window3().Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Search().Show();
            this.Close();
        }
    }
}
