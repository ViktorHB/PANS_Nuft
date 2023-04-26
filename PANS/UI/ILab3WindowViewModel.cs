using System.Windows;

namespace PANS.UI
{
    public interface ILab3WindowViewModel
    {
        Visibility OneAnswerQuestionControlVisibility { get; set; }
        Visibility TextAnswerQuestionControlVisibility { get; set; }
        Visibility SemiAnswerQuestionControlVisibility { get; set; }
    }
}
