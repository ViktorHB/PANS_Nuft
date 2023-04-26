using System.Windows;

namespace PANS.UI
{
    /// <summary>
    /// Interaction logic for Lab3Window.xaml
    /// </summary>
    public partial class Lab3Window : Window
    {
        public Lab3Window(Lab3WindowViewModel la3WindowViewModel)
        {
            InitializeComponent();
            DataContext = la3WindowViewModel;
            la3WindowViewModel.OneAnswerQuestionControl = OneAnswerQuestion;
            la3WindowViewModel.SemiAnswerQuestionControl = SemiAnswerQuestion;
            la3WindowViewModel.TextAnswerQuestionControl = TextAnswerQuestion;
        }
    }
}