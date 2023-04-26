using Microsoft.Expression.Interactivity.Core;
using PANS.Entities;
using PANS.Services;
using PANS.UI.Questions;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PANS.UI
{
    public class Lab3WindowViewModel : ViewModelBase
    {
        private readonly Random _random = new Random();
        private readonly IDbService _dbService;
        private readonly IQuizTableSqliteDataAccess _dataAccess;
        private readonly KnowledgeElementTableSqliteDataAccess _knowledgeElementTableSqliteDataAccess;
        private readonly Queue<QuestionControlViewModelBase> _questions;
        private readonly QuestionControlFactory _questionControlFactory;
        private QuestionControlViewModelBase _currentQuestion;
        private double _finalResult = 0;
        private const int StudentId = 1;

        private bool _isNextButtonEnable = false;
        public bool IsNextButtonEnable
        {
            get => _isNextButtonEnable;
            set
            {
                _isNextButtonEnable = value;
                OnPropertyChanged(nameof(IsNextButtonEnable));
            }
        }

        private bool _isSubmitEnable = false;
        public bool IsSubmitEnable
        {
            get => _isSubmitEnable;
            set
            {
                _isSubmitEnable = value;
                OnPropertyChanged(nameof(IsSubmitEnable));
            }
        }

        public ICommand NextCommand { get; set; }
        public ICommand FinishCommand { get; set; }

        public SemiAnswerQuestionControl SemiAnswerQuestionControl;
        public OneAnswerQuestionControl OneAnswerQuestionControl;
        public TextAnswerQuestionControl TextAnswerQuestionControl;

        public Lab3WindowViewModel(IDbService dbService, IQuizTableSqliteDataAccess dataAccess,
            KnowledgeElementTableSqliteDataAccess knowledgeElementTableSqliteDataAccess)
        {
            _questionControlFactory = new QuestionControlFactory(knowledgeElementTableSqliteDataAccess);
            _dbService = dbService;
            _dataAccess = dataAccess;
            _knowledgeElementTableSqliteDataAccess = knowledgeElementTableSqliteDataAccess;
            Initializer();
            _questions = new Queue<QuestionControlViewModelBase>();
            FinishCommand = new ActionCommand(FinishCommandAction);
            NextCommand = new ActionCommand(NextCommandAction);
        }

        private async Task Initializer()
        {
            await Task.Run(() => { _dbService.FillTableQuiz(); });
            await Task.Delay(500);
            GetQuestionsForQuiz();
            SetQuestion();
        }

        private void SetQuestion()
        {
            if (_currentQuestion != null)
            {
                _finalResult += _currentQuestion.Estimation;
            }
            if (_questions.Count == 0 && _currentQuestion != null)
            {
                IsNextButtonEnable = false;
                IsSubmitEnable = false;
                MessageBox.Show($"Your score\n {_finalResult}");
                return;
            }
            _currentQuestion = _questions.Dequeue();

            switch (_currentQuestion.GetType().Name)
            {
                case "SemiAnswerQuestionControlViewModel":
                    SemiAnswerQuestionControl.Visibility = Visibility.Visible;
                    OneAnswerQuestionControl.Visibility = Visibility.Collapsed;
                    TextAnswerQuestionControl.Visibility = Visibility.Collapsed;
                    SemiAnswerQuestionControl.DataContext = _currentQuestion;
                    break;
                case "OneAnswerQuestionControlViewModel":
                    SemiAnswerQuestionControl.Visibility = Visibility.Collapsed;
                    OneAnswerQuestionControl.Visibility = Visibility.Visible;
                    TextAnswerQuestionControl.Visibility = Visibility.Collapsed;
                    OneAnswerQuestionControl.DataContext = _currentQuestion;
                    break;
                case "TextAnswerQuestionControlViewModel":
                    SemiAnswerQuestionControl.Visibility = Visibility.Collapsed;
                    OneAnswerQuestionControl.Visibility = Visibility.Collapsed;
                    TextAnswerQuestionControl.Visibility = Visibility.Visible;
                    TextAnswerQuestionControl.DataContext = _currentQuestion;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IsNextButtonEnable = false;
            IsSubmitEnable = true;
        }

        private void GetQuestionsForQuiz()
        {
            var qFromDb = _dataAccess.GetData();

            var shortest = qFromDb.GroupBy(item => item.Type)
                .Select(grp => grp.OrderBy(item => item.Type)
                    .First().Type);

            foreach (var type in shortest)
            {
                FillQuestions(type, qFromDb);
            }
        }

        private void FillQuestions(string type, List<QuizData> qFromDb)
        {
            var qType1 = qFromDb.Where(x => x.Type.Equals(type)).ToArray();

            if (qType1.Length > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    _questions.Enqueue(_questionControlFactory.GetQuestion(qType1[_random.Next(0, qType1.Length)]));
                }
            }
        }

        private void NextCommandAction()
        {
            IsSubmitEnable = true;
            IsNextButtonEnable = false;
            SetQuestion();
        }

        private void FinishCommandAction()
        {
            _currentQuestion.Check(StudentId);
            IsNextButtonEnable = true;
            IsSubmitEnable = false;
        }
    }
}