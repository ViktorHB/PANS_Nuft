using System.Windows;
using PANS.Entities;
using PANS.Services;

namespace PANS.UI.Questions
{
    internal class TextAnswerQuestionControlViewModel : QuestionControlViewModelBase
    {
        private string _question;
        private string _answer1;
        private string _correctAnswer;

        public string CorrectAnswer
        {
            get => _correctAnswer;
            set
            {
                _correctAnswer = value;
                OnPropertyChanged(nameof(CorrectAnswer));
            }
        }

        public string Question
        {
            get => _question;
            set
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        public string Answer1
        {
            get => _answer1;
            set
            {
                _answer1 = value;
                OnPropertyChanged(nameof(Answer1));
            }
        }

        public TextAnswerQuestionControlViewModel(QuizData quizData, KnowledgeElementTableSqliteDataAccess access) : base(access)
        {
            CorrectAnswer = quizData.Correct_Answer;
            Question = quizData.Question;
            LearningElementId = quizData.Id_LE;
        }

        public override double Estimation { get; set; }
        public sealed override int LearningElementId { get; set; }

        public override void Check(int userId)
        {
            if (!string.IsNullOrEmpty(Answer1))
            {
                if (CorrectAnswer.Equals(Answer1))
                {
                    Estimation = 1;
                    MessageBox.Show("Correct");
                }
            }

            base.Check(userId);
            MessageBox.Show("Wrong\nCorrect answer is\n" + CorrectAnswer);
        }
    }
}
