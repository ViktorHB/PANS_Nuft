using PANS.Entities;
using System;
using PANS.Services;

namespace PANS.UI.Questions
{
    internal class QuestionControlFactory
    {
        private readonly KnowledgeElementTableSqliteDataAccess _knowledgeElementTableSqliteDataAccess;

        public QuestionControlFactory(KnowledgeElementTableSqliteDataAccess knowledgeElementTableSqliteDataAccess)
        {
            _knowledgeElementTableSqliteDataAccess = knowledgeElementTableSqliteDataAccess;
        }
        public QuestionControlViewModelBase GetQuestion(QuizData quizData)
        {
            switch (quizData.Type)
            {
                case "1":
                    return new OneAnswerQuestionControlViewModel(quizData, _knowledgeElementTableSqliteDataAccess);
                case "2":
                    return new SemiAnswerQuestionControlViewModel(quizData, _knowledgeElementTableSqliteDataAccess);
                case "3":
                    return new TextAnswerQuestionControlViewModel(quizData, _knowledgeElementTableSqliteDataAccess);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
