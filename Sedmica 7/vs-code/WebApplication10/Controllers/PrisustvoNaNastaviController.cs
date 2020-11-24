using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EntityModels;
using WebApplication10.EF;
using WebApplication10.EntityModels;
using WebApplication10.Models;
using WebApplication10.Models.WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class PrisustvoNaNastaviController : Controller
    {
        private MojDbContext db = new MojDbContext();


        public IActionResult Prikaz(int StudentID)
        {
            var m = new PrisustvnoNaNastaviPrikazVM();


            Student q = db.Student.Find(StudentID);
            m.ImeStudenta = q.Ime + " " + q.Prezime;

            m.Zapisi = db.PrisustvoNaNastavi.Where(s => s.StudentID == StudentID)
                .Select(s => new PrisustvnoNaNastaviPrikazVM.Zapis
                {
                    Datum = s.Datum,
                    Predmet = s.Predmet.Naziv           ,
                    IsPrisutan = s.IsPrisutan,
                    Komentar =s.Komentar ,
                    PrisustvoNaNastaviID = s.ID
                })
                .ToList();

            return View(m);
        }


        public IActionResult Uredi(int PrisustvoNaNastaviID)
        {
            PrisustvoNaNastaviUrediVm m = db.PrisustvoNaNastavi
                .Where(s => s.ID == PrisustvoNaNastaviID)
                .Select(x => new PrisustvoNaNastaviUrediVm
                {
                    NazivPredmet = x.Predmet.Naziv,
                    ImeStudent = x.Student.Ime + " " + x.Student.Prezime,
                    komentar = x.Komentar,
                    IsPrisutan = x.IsPrisutan,
                    Datum = x.Datum,
                    PrisustvoNaNastaviID = x.ID
                }).Single();


            return View(m);
        }

        public IActionResult Snimi(PrisustvoNaNastaviUrediVm x)
        {
            var prisustvoNaNastavi = db.PrisustvoNaNastavi.Find(x.PrisustvoNaNastaviID);
            prisustvoNaNastavi.IsPrisutan = x.IsPrisutan;
            prisustvoNaNastavi.Komentar = x.komentar;
            db.SaveChanges();

            //    return RedirectToAction("Prikaz", new { StudentID=ocjene.StudentID });
            return Redirect("/PrisustvoNaNastavi/Prikaz?StudentID=" + prisustvoNaNastavi.StudentID);

        }
    }
}