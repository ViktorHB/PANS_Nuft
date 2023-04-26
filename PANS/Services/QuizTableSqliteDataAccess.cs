using Dapper;
using Microsoft.Data.Sqlite;
using PANS.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace PANS.Services
{
    internal class QuizTableSqliteDataAccess : IQuizTableSqliteDataAccess
    {
        private readonly string _dbFilePath;

        public QuizTableSqliteDataAccess(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        public List<QuizData> GetData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<QuizData>("SELECT * FROM Quiz", new DynamicParameters());
                return output.ToList();
            }
        }

        public void InsertData(QuizData quizData)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Quiz (question, answers, type, correct_answer) VALUES (@Question, @Answers, @Type, @Correct_Answer)", quizData);
            }
        }

        public void Delete()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SqliteConnection>("DELETE FROM Quiz", new DynamicParameters());
            }
        }

        public string LoadConnectionString() => $"Data Source={_dbFilePath};Version=3;";
    }
}
