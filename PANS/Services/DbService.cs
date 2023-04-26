using Server;
using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace PANS.Services
{
    internal class DbService : IDbService
    {
        private static string _daPath;
        private readonly string _dirPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "___NUFT");

        public ITechTableSqliteDataAccess TechDataAccess;
        public ITechDataRepository TechDataRepository;
        public IQuizTableSqliteDataAccess QuizDataAccess;
        public IQuizDataRepository QuizDataRepository;

        public DbService(string daPath)
        {
            _daPath = daPath;
            CreateDbFile();
        }

        /// <inheritdoc cref="IDbService"/>
        public async Task CreateDbFile()
        {
            return;
            await Task.Run(async () =>
            {
                if (!Directory.Exists(_dirPath))
                {
                    Directory.CreateDirectory(_dirPath);
                }

                if (File.Exists(_daPath))
                {
                    File.Delete(_daPath);
                }
                SQLiteConnection.CreateFile(_daPath);
                await CreateTableTech();
                await CreateTableQuiz();
            });
        }

        private static async Task CreateTableTech()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var dbConnection = new SQLiteConnection("Data Source=" + _daPath + ";Version=3;"))
                    {
                        dbConnection.Open();
                        string sql = "CREATE TABLE Tech(" +
                                     "author VARCHAR(50)," +
                                     "date VARCHAR(20)," +
                                     "data VARCHAR(1200)" +
                                     ")";
                        using (var command = new SQLiteCommand(sql, dbConnection))
                        {
                            command.ExecuteNonQuery();
                        }
                        dbConnection.Close();
                    }
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async Task CreateTableQuiz()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var dbConnection = new SQLiteConnection("Data Source=" + _daPath + ";Version=3;"))
                    {
                        dbConnection.Open();
                        string sql = "CREATE TABLE Quiz(" +
                                     "question VARCHAR(50)," +
                                     "answers VARCHAR(1000)," +
                                     "type VARCHAR(2)," +
                                     "correct_answer VARCHAR(100)" +
                                     ")";
                        using (var command = new SQLiteCommand(sql, dbConnection))
                        {
                            command.ExecuteNonQuery();
                        }
                        dbConnection.Close();
                    }
                });

                await Task.Run(() =>
                {
                    FillTableQuiz();
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task FillTableTech()
        {
            await Task.Run(() =>
            {
                if (TechDataAccess.GetData().Count <= 0)
                {
                    foreach (var item in TechDataRepository.GetData())
                    {
                        TechDataAccess.InsertData(item);
                    }
                }
            });
        }

        public async Task FillTableQuiz()
        {
            await Task.Run(() =>
            {
                if (QuizDataAccess.GetData().Count <= 0)
                {
                    foreach (var item in QuizDataRepository.GetData())
                    {
                        QuizDataAccess.InsertData(item);
                    }
                }
            });
        }
    }
}