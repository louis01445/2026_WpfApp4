using System.Windows;
using System.Windows.Input;

namespace _2026_WpfApp4
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void SaveCommands_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void NewCommands_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();

        }
    }
}
