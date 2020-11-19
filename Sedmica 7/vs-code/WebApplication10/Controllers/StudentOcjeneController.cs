using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EntityModels;
using WebApplication10.EF;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class StudentOcjeneController : Controller
    {
        MojDbContext db = new MojDbContext();
        public IActionResult Prikaz(int StudentID)
        {
            var m = db.Ocjene.Where(s => s.StudentID == StudentID)
                .Select(s => new StudentOcjenePrikazVM
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
            StudentOcjenaUrediVM m = db.Ocjene
                .Where(s=>s.ID== OcjenaID)
                .Select(ocjene=>new StudentOcjenaUrediVM
            {
                     NazivPredmet = ocjene.Predmet.Naziv,
                     ImeStudent = ocjene.Student.Ime + " " + ocjene.Student.Prezime,
                Ocjena = ocjene.OcjenaBrojacna,
                OcjenaID = ocjene.ID
            }).Single();
           

            return View(m);
        }

        public IActionResult Snimi(StudentOcjenaUrediVM x)
        {
            Ocjene ocjene = db.Ocjene.Find(x.OcjenaID);
            ocjene.OcjenaBrojacna = x.Ocjena;
            db.SaveChanges();

        //    return RedirectToAction("Prikaz", new { StudentID=ocjene.StudentID });
        return Redirect("/StudentOcjene/Prikaz?StudentID=" + ocjene.StudentID);
        }
    }
}