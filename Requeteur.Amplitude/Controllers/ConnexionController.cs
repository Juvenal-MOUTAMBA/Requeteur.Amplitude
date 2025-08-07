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

        public ConnexionController(IConfiguration config)
        {
            string conn = config.GetConnectionString("OracleConnection");
            _repo = new AgenceRepository(conn);
        }

        // GET: Connexion
        public ActionResult Index()
        {
            var agences = _repo.GetAll();
            return View(agences);
        }

        // POST: Connexion
        [HttpPost]
        public IActionResult Index(string Agence)
        {
            // 🔐 Vérifie si les identifiants sont valides (exemple simplifié)
            if (Agence != null)
            {
                // Stocke l’info de session (ou utilise Identity/Auth si configuré)
                HttpContext.Session.SetString("Agence", Agence);

                // Redirige vers le menu principal (ex. Dashboard)
                return RedirectToAction("Index", "Dashboard");
            }

            // Sinon, retourne la vue avec un message d’erreur
            ViewBag.Message = "Nom d'agence incorrect.";
            return View();
        }
        public IActionResult Quitter()
        {
            //return Content("🔒 Application fermée ou déconnexion simulée.");
            HttpContext.Session.Clear();

            // Redirige vers la page de connexion
            return RedirectToAction("Index", "Connexion");
        }
        [HttpPost]
        public IActionResult Connecter(string? agenceId)
        {
            if (agenceId == null)
            {
                ModelState.AddModelError("", "❌ Veuillez sélectionner une agence.");
                return View("Index", _repo.GetAll()); // Recharge les agences dans la vue
            }

            var agence = _repo.GetById(agenceId);
            if (agence == null)
            {
                ModelState.AddModelError("", "⚠️ Merci de sélectionner une agence.");
                return View("Index", _repo.GetAll());
            }

            ViewBag.Message = $"✅ Connexion à {agence.LIB} réussie.";
            return View("Index", _repo.GetAll());
        }
    }
}
