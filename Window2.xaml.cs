using System.Windows;
using System.Windows.Input;

namespace modmanager
{
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void DragWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseErrorReporting(object sender, RoutedEventArgs e)
        {
            if (ErrorReport.AllClose)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Window1 || window is MainWindow || window is Window2)
                    {
                        window.Close();
                    }
                }
            }
            else
            {
                Close();
            }
        }
    }
}
