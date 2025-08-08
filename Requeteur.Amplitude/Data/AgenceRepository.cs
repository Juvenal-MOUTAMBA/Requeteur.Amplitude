namespace Requeteur.Amplitude.Data
{
    using Dapper;
    using Oracle.ManagedDataAccess.Client;
    using Requeteur.Amplitude.Models;
    using System.Collections.Generic;
    using System.Data;
    public class AgenceRepository
    {
        private readonly string _connectionString;

        public AgenceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Agence> GetAll()
        {
            using (IDbConnection db = new OracleConnection(_connectionString))
            {
                string sql = "SELECT AGE, LIB FROM mucocertstat.bkage ORDER BY AGE";
                return db.Query<Agence>(sql).AsList();
            }
        }

        public Agence GetById(string age)
        {
            using (IDbConnection db = new OracleConnection(_connectionString))
            {
                string sql = "SELECT AGE, LIB FROM mucocertstat.bkage WHERE AGE = :AGE";
                return db.QueryFirstOrDefault<Agence>(sql, new { AGE = age });
            }
        }
    }
}
