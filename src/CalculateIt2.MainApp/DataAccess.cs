using CalculateIt2.MainApp.Model;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;

namespace CalculateIt2.MainApp
{
    internal sealed class DataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString = "Data Source=CalculateIt2.db; Version=3;")
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<Question> GetQuestions()
        {
            using (var connection = new SQLiteConnection(this.connectionString))
            {
                return connection.Query<Question>("SELECT * FROM Questions");
            }
        }
    }
}
