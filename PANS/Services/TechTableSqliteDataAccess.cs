using Dapper;
using Microsoft.Data.Sqlite;
using PANS.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace Server
{
    /// <summary>
    /// Class which provides logic to access to the message data
    /// </summary>
    internal class TechTableSqliteDataAccess : ITechTableSqliteDataAccess
    {
        private readonly string _dbFilePath;

        /// <summary>
        /// creates instance of <see cref="TechTableSqliteDataAccess"/>
        /// </summary>
        /// <param name="dbFilePath"></param>
        public TechTableSqliteDataAccess(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        /// <summary>
        /// Gets data from the table
        /// </summary>
        /// <returns>Data</returns>
        public List<TechData> GetData()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TechData>("SELECT * FROM Tech", new DynamicParameters());
                return output.ToList();
            }
        }


        /// <summary>
        /// Inserts data into the table
        /// </summary>
        /// <param name="techData">Data <see cref="TechData"/></param>
        public void InsertData(TechData techData)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Tech (author, date, data) VALUES (@Author, @Date, @Data)", techData);
            }
        }

        /// <summary>
        /// Removes data from the table
        /// </summary>
        public void Delete()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SqliteConnection>("DELETE FROM Tech", new DynamicParameters());
            }
        }

        public string LoadConnectionString() => $"Data Source={_dbFilePath};Version=3;";
    }
}
