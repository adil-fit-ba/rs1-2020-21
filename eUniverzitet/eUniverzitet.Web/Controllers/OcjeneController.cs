using System.Linq;
using eUniverzitet.Shared.Data;
using eUniverzitet.Shared.EntityModels;
using eUniverzitet.Web.Helper;
using eUniverzitet.Web.Models;
using eUniverzitet.Web.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace eUniverzitet.Web.Controllers
{
    [Autorizacija(ucenik: false, nastavnici: true)]
    public class OcjeneController : Controller
    {
        private ApplicationDbContext db;
        IHubContext<MyHub> _hubContext;
        public OcjeneController(ApplicationDbContext db, IHubContext<MyHub> hubContext)
        {
            this.db = db;
            _hubContext = hubContext;
        }

        public IActionResult Index(int StudentID)
        {
            var m = db.Ocjene.Where(s => s.StudentID == StudentID)
                .Select(s => new OcjenePrikazVM
                {
                    OcjenaID = s.ID,
                    BrojcanaOcjena = s.OcjenaBrojacna,
                    Datum = s.Datum,
                    NazivPredmeta = s.Predmet.Naziv
                })
                .ToList();

            return View(m);
        }


        public IActionResult Uredi(int OcjenaID)
        {
            OcjenaUrediVm m = db.Ocjene
                .Where(s => s.ID == OcjenaID)
                .Select(ocjene => new OcjenaUrediVm
                {
                    NazivPredmet = ocjene.Predmet.Naziv,
                    ImeStudent = ocjene.Student.Korisnik.Ime + " " + ocjene.Student.Korisnik.Prezime,
                    Ocjena = ocjene.OcjenaBrojacna,
                    OcjenaID = ocjene.ID
                }).Single();


            return View(m);
        }

        public IActionResult Snimi(OcjenaUrediVm x)
        {
            Ocjene ocjene = db.Ocjene.Include(s=>s.Student.Korisnik) .Single(s => s.ID == x.OcjenaID);
            ocjene.OcjenaBrojacna = x.Ocjena;
            db.SaveChanges();
            TempData["PorukaWarning"] = "Uspješno ste evidentirali ocjenu za studenta " + ocjene.Student.Korisnik.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)

            Korisnik k = ocjene.Student.Korisnik;
            string poruka = "Vama je upravo evidentirana nova ocjena: " + ocjene.OcjenaBrojacna;
            
            _hubContext.Clients.Group(k.UserName).SendAsync("prijemPoruke", k.Ime, poruka);
            //_hubContext.Clients.All.SendAsync("prijemPoruke", k.Ime, poruka);
            
            
            //    return RedirectToAction("Prikaz", new { StudentID=ocjene.StudentID });
            return Redirect("/Ocjene/?StudentID=" + ocjene.StudentID);

        }
    }
}