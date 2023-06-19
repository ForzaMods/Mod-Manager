using MahApps.Metro.Controls;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Mod_Manager_V2.Windows
{
    public partial class ErrorReporting : MetroWindow
    {
        public bool CRS = false;
        public ErrorReporting()
        {
            InitializeComponent();
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Install_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient httpClient = new WebClient())
            {
                httpClient.DownloadFile("https://cdn.discordapp.com/attachments/1034577082933592124/1097626438196269137/RenderScenarios.zip", MainWindow.BaseDir + @"\media\RenderScenarios.zip");
                Hide();
                MessageBox.Show("Downloaded");
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mw.MWModPages.Visibility = Visibility.Hidden;
            MainWindow.mw.SettingsFrame.Visibility = Visibility.Visible;
            Hide();
        }

        private void TitleLabel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
