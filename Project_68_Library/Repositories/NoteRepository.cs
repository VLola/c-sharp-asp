using System.Data.SqlClient;
using Project_68_Library.Models;
using System.Data;
using Dapper.Contrib.Extensions;

namespace Project_68_Library.Repositories
{
    public static class NoteRepository
    {
        //private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Project_68;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static string connectionString = @"Data Source=SQL8001.site4now.net;Initial Catalog=db_a8dfee_valik;User Id=db_a8dfee_valik_admin;Password=Vampirlola2020";
        public static IEnumerable<Note> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.GetAll<Note>();
            }
        }
        public static Note Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Get<Note>(id);
            }
        }
        public static long Insert(Note note)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Insert(note);
            }
        }
        public static bool Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Delete(new Note() { Id = id });
            }
        }

    }
}
