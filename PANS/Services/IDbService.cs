using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// An interface which provides contract to manage database files
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Creates database file in the directory
        /// </summary>
        /// <param name="path">Database path</param>
        Task CreateDbFile();

        /// <summary>
        /// Creates database file in the directory
        /// </summary>
        Task FillTableTech();

        /// <summary>
        /// Creates database file in the directory
        /// </summary>
        Task FillTableQuiz();
    }
}