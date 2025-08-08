namespace Requeteur.Amplitude.Data
{
    using Dapper;
    using DocumentFormat.OpenXml.Drawing.Charts;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
    using Oracle.ManagedDataAccess.Client;
    using Requeteur.Amplitude.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Net;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

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
                string sql = "select age as CodAge,mucocertstat.BKCLI.cli as IdClient,idext as NumeroMembre,tcli as TypeClient,"+" "+
                "nom as Nom,pre as Prenom,rso as RaisonSociale,sig as Sigle,email as AdresseMail, sext as Sexe,"+ " " +
                "TO_CHAR(dna, 'DD/MM/YYYY') as DateDeNaissance, viln as VilleDeNaissance,nid as NumeroPieceIdent," + " " +
                "TO_CHAR(did, 'DD/MM/YYYY') as DateDelivrancePiece,lid as LieuDelivrancePiece, TO_CHAR(Vald, 'DD/MM/YYYY') as DateDeMission" + " " +
                "from mucocertstat.BKCLI left join mucocertstat.bkemacli on mucocertstat.bkemacli.cli = mucocertstat.BKCLI.cli  left" + " " +
                "join mucocertstat.bkicli on mucocertstat.bkicli.cli = mucocertstat.BKCLI.cli where age = '" +agence.AGE+"' order by idext asc";
                return db.Query<Client>(sql).AsList();
            }
        }
        //public  Client Client_By_ID(Agence agence,string cli)
        //{
        //    using (IDbConnection db = new OracleConnection(_connectionString))
        //    {
        //        string sql = "SELECT    cli, nom, tcli,lib, pre, sext,TO_CHAR(dna,'DD-MM-YYYY'), viln, depn, payn, locn, tid, nid, did, lid,vid, sit, reg, capj, dcapj, sitj, dsitj,"+""+
        //              "nidf,age,ges,qua,tax,catl,seg,lter,catn,sec,lienbq,aclas,maclas,emtit,lang,nat,res,ichq,dichq,icb,dicb,epu,utic,uti,dou,dmo," + "" +
        //              "ord,catr,nomrest,regn,rrc,dvrrc,uti_vrrc,idext,opetr FROM CSTATSEL.bkcli where age = '" + agence.AGE+"' and(cli = '"+cli+"' or nom = '"+cli+"')";
        //        #pragma warning disable CS8603 // Existence possible d'un retour de référence null.
        //        return db.Query<Client>(sql).FirstOrDefault();
        //        #pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        //    }
        //}
        public Client Client_By_ID(Agence agence, string cli)
        {
            using (IDbConnection db = new OracleConnection(_connectionString))
            {
                string sql = "select age as CodAge,mucocertstat.BKCLI.cli as IdClient,idext as NumeroMembre,tcli as TypeClient," + " " +
                "nom as Nom,pre as Prenom,rso as RaisonSociale,sig as Sigle,email as AdresseMail, sext as Sexe," + " " +
                "TO_CHAR(dna, 'DD/MM/YYYY') as DateDeNaissance, viln as VilleDeNaissance,nid as NumeroPieceIdent," + " " +
                "TO_CHAR(did, 'DD/MM/YYYY') as DateDelivrancePiece,lid as LieuDelivrancePiece, TO_CHAR(Vald, 'DD/MM/YYYY') as DateDeMission" + " " +
                "from mucocertstat.BKCLI left join mucocertstat.bkemacli on mucocertstat.bkemacli.cli = mucocertstat.BKCLI.cli  left" + " " +
                "join mucocertstat.bkicli on mucocertstat.bkicli.cli = mucocertstat.BKCLI.cli where age = '" + agence.AGE + "' and (mucocertstat.BKCLI.cli = '" + cli+"' or nom = '"+cli+"')";
                return db.Query<Client>(sql).FirstOrDefault();
            }
        }
    }
}
