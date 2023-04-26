using Dapper;
using PANS.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace PANS.Services
{
    public class KnowledgeElementTableSqliteDataAccess
    {
        private readonly string _dbFilePath;

        public KnowledgeElementTableSqliteDataAccess(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        public List<KnowledgeElement> GetData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KnowledgeElement>("SELECT * FROM KnowledgeElement", new DynamicParameters());
                return output.ToList();
            }
        }

        public void InsertData(KnowledgeElement data)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO KnowledgeElement (element_Id, user_id, level) VALUES (@element_Id, @user_id, @level)", data);
            }
        }

        public void Delete()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SqliteConnection>("DELETE FROM KnowledgeElement", new DynamicParameters());
            }
        }

        public string LoadConnectionString() => $"Data Source={_dbFilePath};Version=3;";
    }
}
