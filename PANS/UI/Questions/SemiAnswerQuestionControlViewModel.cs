using PANS.Entities;
using PANS.Services;
using System.Windows;

namespace PANS.UI.Questions
{
    internal class SemiAnswerQuestionControlViewModel : QuestionControlViewModelBase
    {
        private string _question;
        private string _answer3;
        private string _answer2;
        private string _answer1;
        private string _correctAnswer;
        private string _answer4;
        private bool _isAnswer1Checked;
        private bool _isAnswer2Checked;
        private bool _isAnswer3Checked;
        private bool _isAnswer4Checked;

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

        public string Answer2
        {
            get => _answer2;
            set
            {
                _answer2 = value;
                OnPropertyChanged(nameof(Answer2));
            }
        }

        public string Answer3
        {
            get => _answer3;
            set
            {
                _answer3 = value;
                OnPropertyChanged(nameof(Answer3));
            }
        }

        public string Answer4
        {
            get => _answer4;
            set
            {
                _answer4 = value;
                OnPropertyChanged(nameof(Answer4));
            }
        }

        public bool IsAnswer1Checked
        {
            get => _isAnswer1Checked;
            set
            {
                _isAnswer1Checked = value;
                OnPropertyChanged(nameof(IsAnswer1Checked));
            }
        }

        public bool IsAnswer2Checked
        {
            get => _isAnswer2Checked;
            set
            {
                _isAnswer2Checked = value;
                OnPropertyChanged(nameof(IsAnswer2Checked));
            }
        }
        public bool IsAnswer3Checked
        {
            get => _isAnswer3Checked;
            set
            {
                _isAnswer3Checked = value;
                OnPropertyChanged(nameof(IsAnswer3Checked));
            }
        }

        public bool IsAnswer4Checked
        {
            get => _isAnswer4Checked;
            set
            {
                _isAnswer4Checked = value;
                OnPropertyChanged(nameof(IsAnswer4Checked));
            }
        }

        public SemiAnswerQuestionControlViewModel(QuizData quizData, KnowledgeElementTableSqliteDataAccess access) : base(access)
        {
            CorrectAnswer = quizData.Correct_Answer;
            Question = quizData.Question;
            LearningElementId = quizData.Id_LE;
            var answers = quizData.Answers.Split('/');
            if (answers.Length > 3)
            {
                Answer1 = answers[0];
                Answer2 = answers[1];
                Answer3 = answers[2];
                Answer4 = answers[3];
            }
        }

        public override double Estimation { get; set; }
        public sealed override int LearningElementId { get; set; }

        public override void Check(int userId)
        {
            var arr = CorrectAnswer.Split('/');
            double estimation = 0;
            foreach (var ans in arr)
            {
                if (ans.Equals(Answer1))
                {
                    if (IsAnswer1Checked)
                    {
                        estimation += 0.5;
                    }
                }
                if (ans.Equals(Answer2))
                {
                    if (IsAnswer2Checked)
                    {
                        estimation += 0.5;
                    }
                }
                if (ans.Equals(Answer3))
                {
                    if (IsAnswer3Checked)
                    {
                        estimation += 0.5;
                    }
                }
                if (ans.Equals(Answer4))
                {
                    if (IsAnswer4Checked)
                    {
                        estimation += 0.5;
                    }
                }
            }

            Estimation = estimation;
            base.Check(userId);

            MessageBox.Show($"Correct answers :\n {CorrectAnswer}");
        }
    }
}
