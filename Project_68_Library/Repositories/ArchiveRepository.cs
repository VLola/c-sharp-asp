using System.Data.SqlClient;
using Project_68_Library.Models;
using System.Data;
using Dapper.Contrib.Extensions;

namespace Project_68_Library.Repositories
{
    public class ArchiveRepository
    {
        //private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Project_68;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static string connectionString = @"Data Source=SQL8001.site4now.net;Initial Catalog=db_a8dfee_valik;User Id=db_a8dfee_valik_admin;Password=Vampirlola2020";
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
