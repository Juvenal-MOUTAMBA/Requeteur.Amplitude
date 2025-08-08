using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Requeteur.Amplitude.Data;
using Requeteur.Amplitude.Models;
using System.Diagnostics;

namespace Requeteur.Amplitude.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AgenceRepository _repo;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            string conn = config.GetConnectionString("OracleConnection");
            _repo = new AgenceRepository(conn);
        }
        public IActionResult Index()
        {
            var agences = _repo.GetAll();
            ViewBag.Agences = new SelectList(agences, "AGE", "LIB");
            ViewBag.AgenceChoisie = HttpContext.Session.GetString("Agence");
            ViewBag.AgenceLib = HttpContext.Session.GetString("AgenceLib");

            return View();
        }

        [HttpPost]
        public IActionResult ChoisirAgence(string AGE)
        {
            var agences = _repo.GetAll();
            var agence = agences.FirstOrDefault(a => a.AGE == AGE);
            if (agence != null)
            {
                HttpContext.Session.SetString("Agence", agence.AGE);
                HttpContext.Session.SetString("AgenceLib", agence.LIB);
            }

            return RedirectToAction("Index");
            //if (!string.IsNullOrEmpty(AGE))
            //{
            //    HttpContext.Session.SetString("Agence", AGE);
            //}
            //return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
