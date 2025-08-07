using Microsoft.AspNetCore.Mvc;
using Requeteur.Amplitude.Data;
using Requeteur.Amplitude.Models;

namespace Requeteur.Amplitude.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientRepository _client;
        private readonly AgenceRepository _agences;

        public ClientController(IConfiguration config)
        {
            string conn = config.GetConnectionString("OracleConnection");
            _client = new ClientRepository(conn);
            _agences = new AgenceRepository(conn);
        }
        public IActionResult Index()
        {
            var agenceLib = HttpContext.Session.GetString("AgenceLib");
            var agence = _agences.GetAll().FirstOrDefault(x=>x.LIB==agenceLib);
            //ViewBag.AgenceLib = agenceLib; // Ou ViewData["AgenceLib"] = agenceLib;
            var clients = _client.GetAll(agence);
            return View(clients);
        }
    }
}
