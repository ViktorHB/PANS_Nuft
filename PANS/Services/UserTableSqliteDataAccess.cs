using Dapper;
using Microsoft.Data.Sqlite;
using PANS.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace PANS.Services
{
    internal class UserTableSqliteDataAccess
    {
        private readonly string _dbFilePath;

        public UserTableSqliteDataAccess(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        public List<UserData> GetData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UserData>("SELECT * FROM User", new DynamicParameters());
                return output.ToList();
            }
        }

        public void InsertData(UserData data)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO User (login, password, firstname, SecondName, KnowledgeLevel) VALUES (@login, @password, @firstname, @SecondName, @KnowledgeLevel)", data);
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
