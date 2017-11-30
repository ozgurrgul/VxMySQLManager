using System;
using MySql.Data.MySqlClient;

namespace VxMySQLManager.VxEntityManager
{
    public class Db : IDisposable
    {
        public readonly MySqlConnection Connection;

        public Db(string databaseName)
        {
            Connection = new MySqlConnection($"server=127.0.0.1;" +
                                             $"user id=root;" + // your mysql username
                                             $"password=1;" + // your mysql password
                                             $"port=3306;database={databaseName};");
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}