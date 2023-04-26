using System.Windows;

namespace PANS.UI
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private readonly Lab3Window _lab3Window;

        public QuizWindow(Lab3Window lab3Window)
        {
            _lab3Window = lab3Window;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            _lab3Window.ShowDialog();
        }
    }
}