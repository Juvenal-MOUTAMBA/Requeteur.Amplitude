using Microsoft.AspNetCore.Mvc;
using Requeteur.Amplitude.Data;

namespace Requeteur.Amplitude.Controllers
{
    public class AgenceController : Controller
    {
        private readonly AgenceRepository _repo;

        public AgenceController(IConfiguration config)
        {
            string conn = config.GetConnectionString("OracleConnection");
            _repo = new AgenceRepository(conn);
        }

        public IActionResult Index()
        {
            var agences = _repo.GetAll();
            return View(agences);
        }

        public IActionResult Details(string id)
        {
            var agence = _repo.GetById(id);
            if (agence == null) return NotFound();
            return View(agence);
        }
    }
}
