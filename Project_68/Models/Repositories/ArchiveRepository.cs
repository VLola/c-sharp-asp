using System.Data.SqlClient;
using System.Data;
using Dapper.Contrib.Extensions;

namespace Project_68.Models.Repositories
{
    public class ArchiveRepository
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Project_68;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static IEnumerable<Archive> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.GetAll<Archive>();
            }
        }
        public static long Insert(Archive archive)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Insert(archive);
            }
        }
    }
}
