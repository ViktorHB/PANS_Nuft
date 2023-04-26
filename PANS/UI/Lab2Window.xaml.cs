using System.Windows;

namespace PANS.UI
{
    /// <summary>
    /// Interaction logic for Lab2Window.xaml
    /// </summary>
    public partial class Lab2Window : Window
    {
        public Lab2Window(ILab2WindowViewModel lab2WindowViewModel)
        {
            InitializeComponent();
            DataContext = lab2WindowViewModel;
            InteractivityControl.DataContext = lab2WindowViewModel.InteractivityControlViewModel;
            DataBaseControl.DataContext = lab2WindowViewModel.DataBaseControlViewModelViewModel;
            FunctionGraphControl.DataContext = lab2WindowViewModel.FunctionGraphControlViewModel;
        }
    }
}