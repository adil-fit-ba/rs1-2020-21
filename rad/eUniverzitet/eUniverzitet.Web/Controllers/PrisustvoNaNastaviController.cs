using System.Linq;
using eUniverzitet.BL.Data;
using eUniverzitet.BL.EntityModels;
using eUniverzitet.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eUniverzitet.Web.Controllers
{
    public class PrisustvoNaNastaviController : Controller
    {
        private ApplicationDbContext db;

        public PrisustvoNaNastaviController(ApplicationDbContext db)
        {
            this.db = db;
        }


        public IActionResult Uredi(int PrisustvoNaNastaviID)
        {
            PrisustvoNaNastaviUrediVM m = db.PrisustvoNaNastavi
                .Where(s=>s.ID == PrisustvoNaNastaviID)
                .Select(s=>new PrisustvoNaNastaviUrediVM
                {
                       ImeStudent = s.Student.Korisnik.Ime + " " + s.Student.Korisnik.Prezime,
                       IsPrisutan = s.IsPrisutan,
                       komentar = s.Komentar,
                       NazivPredmet = s.Predmet.Naziv,
                       PrisustvoNaNastaviID=s.ID
                })
                .Single();

            return View(m);
        }  
        
        public IActionResult Snimi(PrisustvoNaNastaviUrediVM x)
        {
            PrisustvoNaNastavi p = db.PrisustvoNaNastavi
                .Include(s => s.Student.Korisnik)
                .Single(s => s.ID == x.PrisustvoNaNastaviID);

            p.IsPrisutan = x.IsPrisutan;
            p.Komentar = x.komentar;
            db.SaveChanges();

            TempData["PorukaWarning"] = "Uspješno ste evidentirali evidenciju za studenta " + p.Student.Korisnik.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)


            return Redirect("/PrisustvoNaNastavi/?StudentID=" + p.StudentID);
        }


        public IActionResult Index(int StudentID)
        {
            var m = new PrisustvoNaNastaviPrikazVM();


            Student q = db.Student.Include(s=>s.Korisnik).Single(s => s.ID == StudentID);
            m.ImeStudenta = q.Korisnik.Ime + " " + q.Korisnik.Prezime;

            m.Zapisi = db.PrisustvoNaNastavi.Where(s => s.StudentID == StudentID)
                .Select(s => new PrisustvoNaNastaviPrikazVM.Zapis
                {
                    Datum = s.Datum,
                    Predmet = s.Predmet.Naziv           ,
                     Komentar = s.Komentar,
                     Prisutan = s.IsPrisutan   ,
                     PrisustvoNaNastaviID = s.ID
                })
                .ToList();

            return View(m);
        }


    }
}