using Dapper;
using Microsoft.Data.Sqlite;
using PANS.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace PANS.Services
{
    internal class LearningElementTableSqliteDataAccess
    {
        private readonly string _dbFilePath;

        public LearningElementTableSqliteDataAccess(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        public List<LearningElement> GetData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LearningElement>("SELECT * FROM LearningElement", new DynamicParameters());
                return output.ToList();
            }
        }

        public void InsertData(LearningElement data)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO LearningElement (name) VALUES (@name)", data);
            }
        }

        public void Delete()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SqliteConnection>("DELETE FROM User", new DynamicParameters());
            }
        }

        public string LoadConnectionString() => $"Data Source={_dbFilePath};Version=3;";
    }
}