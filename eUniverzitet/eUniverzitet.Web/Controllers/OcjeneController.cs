using System.Linq;
using eUniverzitet.Shared.Data;
using eUniverzitet.Shared.EntityModels;
using eUniverzitet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace eUniverzitet.Web.Controllers
{
    public class OcjeneController : Controller
    {
        private ApplicationDbContext db;
        IHubContext<MyHub> _hubContext;
        public OcjeneController(ApplicationDbContext db, IHubContext<MyHub> hubContext)
        {
            this.db = db;
            _hubContext = hubContext;
        }

        public IActionResult Prikaz(int StudentID)
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

            string korisnikIme = ocjene.Student.Korisnik.Ime;
            
            _hubContext.Clients.All.SendAsync("prijemPoruke", korisnikIme, "evidentirana nova ocjena: " + ocjene.OcjenaBrojacna);
            
            
            //    return RedirectToAction("Prikaz", new { StudentID=ocjene.StudentID });
            return Redirect("/Ocjene/Prikaz?StudentID=" + ocjene.StudentID);

        }
    }
}