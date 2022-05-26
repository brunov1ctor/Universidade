using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace Universidade.Data{
    public class DbSession : IDisposable
    {
        public IDbConnection Connection{get;}

        public DbSession(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration
            .GetConnectionString("DefaultConnection"));

            Connection.Open();
        }
        public void Dispose() => Connection?.Dispose();
    }

}