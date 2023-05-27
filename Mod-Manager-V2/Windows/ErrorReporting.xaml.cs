using MahApps.Metro.Controls;
using System.Windows;

namespace Mod_Manager_V2.Windows
{
    public partial class ErrorReporting : MetroWindow
    {
        public ErrorReporting()
        {
            InitializeComponent();
        }

        private void Exit_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
