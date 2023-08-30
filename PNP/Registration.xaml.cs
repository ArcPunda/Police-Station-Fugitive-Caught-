using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public FilterInfoCollection fic = null;
        public VideoCaptureDevice vcd = null;
        public bool OnOff = false;
        public Window2()
        {
            InitializeComponent();
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo fi in fic)
                WbCams.Items.Add(fi.Name);
            WbCams.SelectedIndex = 0;
            vcd = new VideoCaptureDevice();
            Save_Pic.IsEnabled = false;
            if (Variables.FilePath == "")
            {
                System.Windows.MessageBox.Show("File path has not been set, go to the settings and set a location for data to be saved. Go Back To MainMenu to set it");
                Save_Pic.IsEnabled = false;
                Wbcam.IsEnabled = false;
            }
        }

        private void Wbcam_Click(object sender, RoutedEventArgs e)
        {
            if(OnOff == false)
            {
                vcd = new VideoCaptureDevice(fic[WbCams.SelectedIndex].MonikerString);
                vcd.NewFrame += Vcd_NewFrame;
                vcd.Start();
                OnOff = true;
                Save_Pic.IsEnabled = true;
            }
            else if(OnOff == true)
            {
                OnOff = false;
                Webcam.Source = null;
                Save_Pic.IsEnabled = false;
                vcd.SignalToStop();
            }

        }
        private void Vcd_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            this.Dispatcher.Invoke(() =>
            {
                Webcam.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        ((Bitmap)eventArgs.Frame.Clone()).GetHbitmap(),
                        IntPtr.Zero,
                        System.Windows.Int32Rect.Empty,
                        BitmapSizeOptions.FromWidthAndHeight((int)Webcam.Width, (int)Webcam.Height));
            });
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (vcd.IsRunning)
            {
                vcd.SignalToStop();
                vcd = null;
            }
        }

        public void ImageToFile(string filePath)
        {
            var image = Webcam.Source;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
                encoder.Save(fileStream);
            }
        }

        private void Save_Pic_Click(object sender, RoutedEventArgs e)
        {
                ImageToFile(Variables.FilePath + "/" + Blotter.Text + ".png");
                vcd.SignalToStop();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
