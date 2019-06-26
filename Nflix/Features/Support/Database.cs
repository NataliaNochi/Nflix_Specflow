using Npgsql;
using System;

namespace Nflix.Features.Support
{
    public class Database : IDisposable
    {
        private readonly NpgsqlConnection conn;

        public Database()
        {
            conn = new NpgsqlConnection("Server=192.168.99.100;User Id=postgres;Password=qaninja;Database=nflix;");
            conn.Open();
        }

        public void Execute_Sql(string command)
        {
            NpgsqlCommand sql_cmd = new NpgsqlCommand(command, conn);
            sql_cmd.ExecuteNonQuery();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }
}
