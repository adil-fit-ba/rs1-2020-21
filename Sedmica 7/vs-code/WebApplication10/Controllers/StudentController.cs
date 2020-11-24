using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication10.EF;
using WebApplication10.EntityModels;
using WebApplication10.Helper;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class StudentController : Controller
    {
        private MojDbContext db = new MojDbContext();

      

        //public IActionResult Snimi(int studentID, string Imeee, string Prezime, int OpstinaRodjenjaID, int OpstinaPrebivalistaID)
        public IActionResult Snimi(StudentDodajVM x)
        {

            MojDbContext db = new MojDbContext();

            Student student;

            if (x.ID == 0)      //insert
            {
                student = new Student();
                db.Add(student);
                TempData["PorukaInfo"] = "Uspješno ste dodali studenta " + student.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)
            }
            else
            {                                //update
                student = db.Student.Find(x.ID);
                TempData["PorukaInfo"] = "Uspješno ste updateovali studenta " + x.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)
            }

            student.Ime = x.Ime;
            student.Prezime = x.Prezime;
            student.OpstinaPrebivalistaID = x.OpstinaPrebivalistaID;
            student.OpstinaRodjenjaID = x.OpstinaRodjenjaID;

            db.SaveChanges(); //insert into Student value (...) ili update Student

            
            return Redirect("/Student/Poruka");
        }

        public IActionResult Poruka()
        {     

            return View("Poruka");
        }


        public IActionResult Obrisi(int StudentID)
        {
            MojDbContext db = new MojDbContext();
            Student s = db.Student.Find(StudentID);

            db.Remove(s);
            db.SaveChanges();//delete Student where id=...

            TempData["PorukaInfo"] = "Uspješno ste obrisali studenta " + s.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)

            return Redirect("/Student/Poruka");
        }
       
        public IActionResult Uredi(int StudentID)
        {
            MojDbContext db = new MojDbContext();
            List<SelectListItem> opstine = db.Opstina
                .OrderBy(a => a.Naziv)
                .Select(a=>new SelectListItem
                {
                    Text = a.Naziv,
                    Value = a.ID.ToString()
                })
                .ToList();

          //  ViewData["opstine"] = opstine;

            StudentDodajVM s;
            if (StudentID == 0)
                s = new StudentDodajVM() {};
            else
                s = db.Student
                    .Where(w => w.ID == StudentID)
                    .Select(a => new StudentDodajVM
                    {
                        ID = a.ID,
                        Ime = a.Ime,
                        Prezime = a.Prezime,
                        OpstinaPrebivalistaID = a.OpstinaPrebivalistaID,
                        OpstinaRodjenjaID = a.OpstinaRodjenjaID,
                        //opstine = opstine
                    }).Single();

            s.opstine = opstine;
            //  ViewData["student"] = s;

            return View("Uredi", s);
        }


        public IActionResult Prikaz(string q)
        {
            //string q = HttpContext.Request.QueryString["q"];

            MojDbContext db = new MojDbContext();

            //select * from Student 
            List<StudentPrikazVM.Row> studenti = db.Student
                .Where(s => q == null || (s.Ime + " " + s.Prezime).StartsWith(q) ||
                            (s.Prezime + " " + s.Ime).StartsWith(q))
             //   .Include("OpstinaRodjenja")
             //   .Include(s => s.OpstinaPrebivalista)
                .Select(x=>new StudentPrikazVM.Row
                {
                    ID = x.ID,
                    BrojIndeksa = x.BrojIndeksa,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    OpstinaRodjenja = x.OpstinaRodjenja.Naziv,
                    OpstinaPrebivalista = x.OpstinaPrebivalista.Naziv,
        })
                .ToList();



      //      ViewData["q"] = q;
     //       ViewData["studedsfdsnti"] = studenti;
             StudentPrikazVM m = new StudentPrikazVM();
             m.studenti = studenti;
             m.q = q;

            return View(m);
        }
    }
}