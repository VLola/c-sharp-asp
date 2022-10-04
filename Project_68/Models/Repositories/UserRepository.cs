﻿using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace Project_68.Models.Repositories
{
    public static class UserRepository
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Project_68;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static bool CheckPassword(string name, string password)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                foreach (var item in connection.GetAll<User>())
                {
                    if (item.Name == name && BCrypt.Net.BCrypt.Verify(password, item.Password)) return true;
                }
            }
            return false;
        }
        public static bool CheckUser(string name, string token)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                foreach (var item in connection.GetAll<User>())
                {
                    if (item.Name == name && item.Token == token) return true;
                }
            }
            return false;
        }
        public static bool CheckToken(string token)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                foreach (var item in connection.GetAll<User>())
                {
                    if (item.Token == token) return true;
                }
            }
            return false;
        }
        public static IEnumerable<User> GetAll()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.GetAll<User>();
            }
        }
        public static User Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Get<User>(id);
            }
        }
        public static long Insert(User user)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Insert(user);
            }
        }
        public static bool Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Delete(new User() { Id = id });
            }
        }
    }
}
