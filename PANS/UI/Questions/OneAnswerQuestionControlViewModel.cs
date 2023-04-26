using PANS.Entities;
using PANS.Services;
using System.Windows;

namespace PANS.UI.Questions
{
    internal class OneAnswerQuestionControlViewModel : QuestionControlViewModelBase
    {
        private string _question;
        private string _answer3;
        private string _answer2;
        private string _answer1;
        private string _correctAnswer;
        private bool _isAnswer1Checked;
        private bool _isAnswer2Checked;
        private bool _isAnswer3Checked;

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

        public OneAnswerQuestionControlViewModel(QuizData quizData, KnowledgeElementTableSqliteDataAccess access) : base(access)
        {
            CorrectAnswer = quizData.Correct_Answer;
            Question = quizData.Question;
            LearningElementId = quizData.Id_LE;
            var answers = quizData.Answers.Split('/');
            if (answers.Length > 2)
            {
                Answer1 = answers[0];
                Answer2 = answers[1];
                Answer3 = answers[2];
            }
        }

        public override double Estimation { get; set; }
        public sealed override int LearningElementId { get; set; }

        public override void Check(int userId)
        {
            if (!IsAnswer1Checked && !IsAnswer2Checked && !IsAnswer3Checked)
            {
                MessageBox.Show("Please make a choice");
                return;
            }

            if (IsAnswer1Checked)
            {
                if (Answer1.Equals(CorrectAnswer))
                {
                    MessageBox.Show("Correct");
                    Estimation = 1;
                }
            }

            if (IsAnswer2Checked)
            {
                if (Answer2.Equals(CorrectAnswer))
                {
                    MessageBox.Show("Correct");
                    Estimation = 1;
                }
            }

            if (IsAnswer3Checked)
            {
                if (Answer3.Equals(CorrectAnswer))
                {
                    MessageBox.Show("Correct");
                    Estimation = 1;
                }
            }

            if (Estimation == 0)
            {
                MessageBox.Show("Wrong");
            }
            base.Check(userId);
        }
    }
}
