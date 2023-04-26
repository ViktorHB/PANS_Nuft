using System.Windows;
using PANS.Entities;
using PANS.Services;

namespace PANS.UI.Questions
{
    internal abstract class QuestionControlViewModelBase : ViewModelBase
    {
        private readonly KnowledgeElementTableSqliteDataAccess _knowledgeElementTableSqliteDataAccess;

        public QuestionControlViewModelBase(KnowledgeElementTableSqliteDataAccess knowledgeElementTableSqliteDataAccess)
        {
            _knowledgeElementTableSqliteDataAccess = knowledgeElementTableSqliteDataAccess;
        }
        public abstract double Estimation { get; set; }
        public abstract int LearningElementId { get; set; }

        public virtual void Check(int userId)
        {
            _knowledgeElementTableSqliteDataAccess.InsertData(new KnowledgeElement
            {
                Element_Id = LearningElementId,
                Level = Estimation,
                User_Id = userId
            });
        }
    }
}
