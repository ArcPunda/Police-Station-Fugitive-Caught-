using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PNP
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            Filepathers.IsHitTestVisible = false;
            string line = "";
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Locale.csv");
            StreamReader sr = new StreamReader(path);
            line = sr.ReadLine();

            while (line != null)
            {
                Filepathers.Text = line;
                Variables.FilePath = line;
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void Selecting_Click(object sender, RoutedEventArgs e)
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Locale.csv");
            File.WriteAllBytes(path, new byte[] { 0 });
            FolderBrowserDialog diag = new FolderBrowserDialog();
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Variables.FilePath = diag.SelectedPath;
            }
            else
            {
                Variables.FilePath = "";
            }
            StreamWriter sw = new StreamWriter(path.ToString());
            sw.WriteLine(Variables.FilePath);
            Filepathers.Text = Variables.FilePath;
            sw.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
