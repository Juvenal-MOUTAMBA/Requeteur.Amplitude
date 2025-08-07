namespace Requeteur.Amplitude.Data
{
    using Dapper;
    using Oracle.ManagedDataAccess.Client;
    using Requeteur.Amplitude.Models;
    using System.Collections.Generic;
    using System.Data;
    public class ClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Client> GetAll(Agence agence)
        {
            using (IDbConnection db = new OracleConnection(_connectionString))
            {
                string sql = "SELECT    cli, nom, tcli,lib, pre, sext,dna, viln, " +
                    "depn, payn, locn, tid, nid, did, lid,vid, sit, reg, capj, dcapj, " +
                    "sitj, dsitj,nidf,age,ges,qua,tax,catl,seg,lter,catn,sec,lienbq," +
                    "aclas,maclas,emtit,lang,nat,res,ichq,dichq,icb,dicb,epu,utic,uti,dou,dmo," +
                    "ord,catr,nomrest,regn,rrc,dvrrc,uti_vrrc,idext,opetr FROM newcert.bkcli " +
                    "where age='"+ agence.AGE + "'";
                return db.Query<Client>(sql).AsList();
            }
        }
    }
}
