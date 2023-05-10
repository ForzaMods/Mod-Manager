using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace modmanager
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void DownloadButton(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("https://4.bp.blogspot.com/-z4sMggeD4dg/UO2TB9INuFI/AAAAAAAAdwc/Kg5dqlKKHrQ/s1600/funny-cat-pictures-032-025.jpg", @"C:\\Users\\" + Environment.UserName + "\\Documents\\ForzaModManager\\funnecat.jpg");
                }
            }
            catch (Exception ex)
            {
                Window2 window2 = new Window2();
                window2.errorcode.Content = ex.Message;
                window2.Show();
            }
        }
    }
}
