using eUniverzitet.Shared.Data;
using eUniverzitet.Shared.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eUniverzitet.Web.Controllers
{
    public class TestniPodaciController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<Korisnik> _usermanager;

        public TestniPodaciController(ApplicationDbContext db, UserManager<Korisnik> usermanager)
        {
            this.db = db;
            _usermanager = usermanager;
        }

        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            await DbInicijalizator.Generisi(db, _usermanager);

         
            return View(db);
        }
    }
}