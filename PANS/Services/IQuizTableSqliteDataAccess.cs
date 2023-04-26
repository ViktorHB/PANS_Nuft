using PANS.Entities;
using System.Collections.Generic;

namespace PANS.Services
{
    public interface IQuizTableSqliteDataAccess : IConnectionString
    {
        /// <summary>
        /// Gets data from the table
        /// </summary>
        /// <returns>Data</returns>
        List<QuizData> GetData();

        /// <summary>
        /// Inserts data into the table
        /// </summary>
        /// <param name="techData">Data <see cref="QuizData"/></param>
        void InsertData(QuizData techData);

        /// <summary>
        /// Removes data from the table
        /// </summary>
        void Delete();
    }
}
