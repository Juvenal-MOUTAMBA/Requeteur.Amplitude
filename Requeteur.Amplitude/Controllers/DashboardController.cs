using Microsoft.AspNetCore.Mvc;

namespace Requeteur.Amplitude.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var agence = HttpContext.Session.GetString("Agence");
            if (string.IsNullOrEmpty(agence))
            {
                // Si non connecté, renvoyer vers la page de connexion
                return RedirectToAction("Index", "Connexion");
            }

            ViewBag.Agence = agence;
            return View();
        }
    }
}
