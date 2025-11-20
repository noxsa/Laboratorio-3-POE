using System.Collections.Generic;
using System.Data.SqlClient;
using RepoDb;

namespace laboratorio3
{
    public static class DbRepo
    {
        public static IEnumerable<T> GetAll<T>() where T : class
        {
            using var conn = new SqlConnection(Config.ConnectionString);
            conn.Open();
            return conn.QueryAll<T>();
        }

        public static int Insert<T>(T entity) where T : class
        {
            using var conn = new SqlConnection(Config.ConnectionString);
            conn.Open();
            return (int)conn.Insert(entity);
        }

        public static bool Update<T>(T entity) where T : class
        {
            using var conn = new SqlConnection(Config.ConnectionString);
            conn.Open();
            return conn.Update(entity);
        }
    }
}
