﻿using System.Data.SqlClient;
using System.Data;
using Dapper.Contrib.Extensions;

namespace Project_68.Models.Repositories
{
    public static class NoteRepository
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Project_68;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
        public static long Set(Note user)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Insert(user);
            }
        }
    }
}
