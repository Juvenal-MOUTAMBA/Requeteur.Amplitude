using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Requeteur.Amplitude.Data;
using Requeteur.Amplitude.Models;

namespace Requeteur.Amplitude.Controllers
{
    public class ConnexionController : Controller
    {
        // GET: ConnexionController
        private readonly AgenceRepository _repo;
        public Agence Agence { get; set; }

        public ConnexionController(IConfiguration config)
        {
            string conn = config.GetConnectionString("OracleConnection");
            _repo = new AgenceRepository(conn);
        }
        public ActionResult Index()
        {
            var agences = _repo.GetAll();
            return View(agences);
        }
       
        public IActionResult Quitter()
        {
            return Content("🔒 Application fermée ou déconnexion simulée.");
        }
        public IActionResult Login( Agence agence)
        {
            if (agence == null || string.IsNullOrEmpty(agence.LIB))
            {
                Agence = _repo.GetById(agence.AGE);
                ModelState.AddModelError("", "Nom d'agence et mot de passe requis.");
                return View("Index", _repo.GetAll());
                
            }
            return View("Home", Agence);
        }
    }
}
