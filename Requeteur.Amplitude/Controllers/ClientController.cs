using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Requeteur.Amplitude.Data;
using Requeteur.Amplitude.Models;
using System.IO;

namespace Requeteur.Amplitude.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientRepository _client;
        private readonly AgenceRepository _agences;
        //public List<Client> Clients { get; set; } = new List<Client>();

        public ClientController(IConfiguration config)
        {
            string conn = config.GetConnectionString("OracleConnection");
            _client = new ClientRepository(conn);
            _agences = new AgenceRepository(conn);
        }
        public IActionResult Index()
        {
            var agenceLib = HttpContext.Session.GetString("AgenceLib");
            var agence = _agences.GetAll().FirstOrDefault(x => x.LIB == agenceLib);
            //ViewBag.AgenceLib = agenceLib; // Ou ViewData["AgenceLib"] = agenceLib;
            var Clients = _client.GetAll(agence);
            return View(Clients);
        }
        public IActionResult Export()
        {
            var agenceLib = HttpContext.Session.GetString("AgenceLib");
            var agence = _agences.GetAll().FirstOrDefault(x => x.LIB == agenceLib);
            var Clients = _client.GetAll(agence);
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Clients");

            // Entêtes
            worksheet.Cell(1, 1).Value = "CodAge";
            worksheet.Cell(1, 2).Value = "IdClient";
            worksheet.Cell(1, 3).Value = "NumeroMembre";
            worksheet.Cell(1, 4).Value = "TypeClient";
            worksheet.Cell(1, 5).Value = "Nom";
            worksheet.Cell(1, 6).Value = "Prenom";
            worksheet.Cell(1, 7).Value = "RaisonSociale";
            worksheet.Cell(1, 8).Value = "Sigle";
            worksheet.Cell(1, 9).Value = "AdresseMail";
            worksheet.Cell(1, 10).Value = "Sexe";
            worksheet.Cell(1, 11).Value = "DateDeNaissance";
            worksheet.Cell(1, 12).Value = "VilleDeNaissance";
            worksheet.Cell(1, 13).Value = "NumeroPieceIdent";
            worksheet.Cell(1, 14).Value = "DateDelivrancePiece";
            worksheet.Cell(1, 15).Value = "LieuDelivrancePiece";
            worksheet.Cell(1, 16).Value = "DateDeMission";

            // Données
            for (int i = 0; i < Clients.Count; i++)
            {
                var clt = Clients[i];
                worksheet.Cell(i + 2, 1).Value = clt.CodAge;
                worksheet.Cell(i + 2, 2).Value = clt.IdClient;
                worksheet.Cell(i + 2, 3).Value = clt.NumeroMembre;
                worksheet.Cell(i + 2, 4).Value = clt.TypeClient;
                worksheet.Cell(i + 2, 5).Value = clt.Nom;
                worksheet.Cell(i + 2, 6).Value = clt.Prenom;
                worksheet.Cell(i + 2, 7).Value = clt.RaisonSociale;
                worksheet.Cell(i + 2, 8).Value = clt.Sigle;
                worksheet.Cell(i + 2, 9).Value = clt.AdresseMail;
                worksheet.Cell(i + 2, 10).Value = clt.Sexe;
                worksheet.Cell(i + 2, 11).Value = clt.DateDeNaissance;
                worksheet.Cell(i + 2, 12).Value = clt.VilleDeNaissance;
                worksheet.Cell(i + 2, 13).Value = clt.NumeroPieceIdent;
                worksheet.Cell(i + 2, 14).Value = clt.DateDelivrancePiece;
                worksheet.Cell(i + 2, 15).Value = clt.LieuDelivrancePiece;
                worksheet.Cell(i + 2, 16).Value = clt.DateDeMission;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Clients.xlsx");
        }
        public IActionResult Details(string id)
        {
            var agenceLib = HttpContext.Session.GetString("AgenceLib");
            var agen = _agences.GetAll().FirstOrDefault(x => x.LIB == agenceLib);
            var clt = _client.Client_By_ID(agen, id);
            if (clt == null)
            {
                return NotFound();
            }
            return View(clt);
        }

        public IActionResult Search(string search)
        {
            var agenceLib = HttpContext.Session.GetString("AgenceLib");
            var agence = _agences.GetAll().FirstOrDefault(x => x.LIB == agenceLib);
            if (string.IsNullOrEmpty(search))
            {
                return RedirectToAction("Index");
            }
            var clients = _client.Client_By_Search(agence, search);
            if (clients == null)
            {
                return View("Index", new List<Client> ());
            }
            return View("Index",clients);
        }
    }
}
