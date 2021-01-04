using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using eUniverzitet.BL.Data;
using eUniverzitet.BL.EntityModels;
using eUniverzitet.Web.Helper;
using eUniverzitet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace eUniverzitet.Web.Controllers
{
   // [Authorize]
    [Autorizacija( false,  true)]
    public class StudentController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<Korisnik> _userManager;

        public StudentController(ApplicationDbContext db, UserManager<Korisnik> usermanager)
        {
            this._db = db;
            this._userManager = usermanager;
        }

        //public IActionResult Snimi(int studentID, string Imeee, string Prezime, int OpstinaRodjenjaID, int OpstinaPrebivalistaID)
        public  IActionResult Snimi(StudentDodajVM x)
        {
            Student student;

            if (x.ID == 0) //insert
            {
                student = new Student();
                student.Korisnik = new Korisnik
                {
                    
                };
                _db.Add(student);
                TempData["PorukaInfo"] = "Uspješno ste dodali studenta " + x.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)
            }
            else
            {                                //update
                student = _db.Student.Include(s=>s.Korisnik).Single(s => s.ID == x.ID);
                TempData["PorukaInfo"] = "Uspješno ste updateovali studenta " + x.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)
            }

            student.Korisnik.Ime = x.Ime;
            student.Korisnik.Email = x.Email;
            student.Korisnik.EmailConfirmed = true;
            student.Korisnik.UserName = x.Email;
            student.Korisnik.Prezime = x.Prezime;
            student.OpstinaPrebivalistaID = x.OpstinaPrebivalistaID;
            student.OpstinaRodjenjaID = x.OpstinaRodjenjaID;


            if (x.SlikaStudentaNew != null)
            {
                string ekstenzija = Path.GetExtension(x.SlikaStudentaNew.FileName);
                string contentType = x.SlikaStudentaNew.ContentType;

                var filename = $"{Guid.NewGuid()}{ekstenzija}";
                string folder = "wwwroot/uploads/";
                bool exists = System.IO.Directory.Exists(folder);
                if (!exists)
                    System.IO.Directory.CreateDirectory(folder);
                
                x.SlikaStudentaNew.CopyTo(new FileStream(folder + filename, FileMode.Create));
                student.SlikaStudenta = filename;
            }

           

            if (x.ID == 0)
                _ = _userManager.CreateAsync(student.Korisnik, "Test.2020").Result;

            _db.SaveChanges(); //insert into Student value (...) ili update Student

            
            return Redirect("/Student");
        }

       


        public IActionResult Obrisi(int StudentID)
        {

            Student s = _db.Student.Include(s=>s.Korisnik).Single(s => s.ID == StudentID);

            List<Ocjene> ocjeneZaBrisati = _db.Ocjene.Where(ss => ss.StudentID == s.ID).ToList();
            List<PrisustvoNaNastavi> prisustvoZaBrisati = _db.PrisustvoNaNastavi.Where(ss => ss.StudentID == s.ID).ToList();
            _db.RemoveRange(ocjeneZaBrisati);
            _db.RemoveRange(prisustvoZaBrisati);
            _db.SaveChanges();

            _db.Remove(s);
            _db.SaveChanges();//delete Student where id=...

            TempData["PorukaWarning"] = "Uspješno ste obrisali studenta " + s.Korisnik.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)

            return Redirect("/Student/");
        }
       
        public IActionResult Uredi(int StudentID)
        {
            List<SelectListItem> opstine = _db.Opstina
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
                s = _db.Student
                    .Where(w => w.ID == StudentID)
                    .Select(a => new StudentDodajVM
                    {
                        ID = a.ID,
                        Ime = a.Korisnik.Ime,
                        Prezime = a.Korisnik.Prezime,
                        Email = a.Korisnik.Email,
                        OpstinaPrebivalistaID = a.OpstinaPrebivalistaID,
                        OpstinaRodjenjaID = a.OpstinaRodjenjaID,
                        SlikaStudentaCurrent = a.SlikaStudenta
                        //opstine = opstine
                    }).Single();

            s.opstine = opstine;
            //  ViewData["student"] = s;

            return View("Uredi", s);
        }


        public IActionResult Index(string q)
        {
            //string q = HttpContext.Request.QueryString["q"];


            //select * from Student 
            List<StudentPrikazVM.Row> studenti = _db.Student
                .Where(s => q == null || (s.Korisnik.Ime + " " + s.Korisnik.Prezime).StartsWith(q) ||
                            (s.Korisnik.Prezime + " " + s.Korisnik.Ime).StartsWith(q))
             //   .Include("OpstinaRodjenja")
             //   .Include(s => s.OpstinaPrebivalista)
                .Select(x=>new StudentPrikazVM.Row
                {
                    ID = x.ID,
                    BrojIndeksa = x.BrojIndeksa,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    Email = x.Korisnik.Email,
                    OpstinaRodjenja = x.OpstinaRodjenja.Naziv,
                    OpstinaPrebivalista = x.OpstinaPrebivalista.Naziv,
        })
                .ToList();



             StudentPrikazVM m = new StudentPrikazVM();
             m.studenti = studenti;
             m.q = q;

            return View(m);
        }
    }
}