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
                DragMove();
            }
        }

        private void CloseErrorReporting(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
