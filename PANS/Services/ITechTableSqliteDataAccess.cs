using PANS.Entities;
using PANS.Services;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// An interface which provides contract to access to the message data
    /// </summary>
    public interface ITechTableSqliteDataAccess : IConnectionString
    {
        /// <summary>
        /// Gets data from the table
        /// </summary>
        /// <returns>Data</returns>
        List<TechData> GetData();

        /// <summary>
        /// Inserts data into the table
        /// </summary>
        /// <param name="techData">Data <see cref="TechData"/></param>
        void InsertData(TechData techData);

        /// <summary>
        /// Removes data from the table
        /// </summary>
        void Delete();
    }
}