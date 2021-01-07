using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eUniverzitet.Web.Controllers
{
   // [Authorize]
  //  [Autorizacija( false,  true)]
    public class StudentController : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<Korisnik> _userManager;

        public StudentController(ApplicationDbContext db, UserManager<Korisnik> usermanager)
        {
            this._db = db;
            this._userManager = usermanager;
        }

        [HttpPost]
        public  IActionResult Snimi(StudentDodajVM x)
        {
  
            
            Student student;

            if (x.id == 0) //insert
            {
                student = new Student();
                student.Korisnik = new Korisnik
                {
                    
                };
                _db.Add(student);
                TempData["PorukaInfo"] = "Uspješno ste dodali studenta " + x.ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)
            }
            else
            {                                //update
                student = _db.Student.Include(s=>s.Korisnik).Single(s => s.ID == x.id);
                TempData["PorukaInfo"] = "Uspješno ste updateovali studenta " + x.ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)
            }

            student.Korisnik.Ime = x.ime;
            student.Korisnik.Email = x.email;
            student.Korisnik.EmailConfirmed = true;
            student.Korisnik.UserName = x.email;
            student.Korisnik.Prezime = x.prezime;
            student.OpstinaPrebivalistaID = x.opstinaPrebivalistaID;
            student.OpstinaRodjenjaID = x.opstinaRodjenjaID;


            if (x.slikaStudentaNew != null)
            {
                string ekstenzija = Path.GetExtension(x.slikaStudentaNew.FileName);
                string contentType = x.slikaStudentaNew.ContentType;

                var filename = $"{Guid.NewGuid()}{ekstenzija}";
                string folder = "wwwroot/uploads/";
                bool exists = System.IO.Directory.Exists(folder);
                if (!exists)
                    System.IO.Directory.CreateDirectory(folder);
                
                x.slikaStudentaNew.CopyTo(new FileStream(folder + filename, FileMode.Create));
                student.SlikaStudenta = filename;
            }
           
            if (x.id == 0)
                _ = _userManager.CreateAsync(student.Korisnik, "Mostar2020!").Result;

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
                .Select(a => new SelectListItem
                {
                    Text = a.Naziv,
                    Value = a.ID.ToString()
                })
                .ToList();

            StudentDodajVM s;
            if (StudentID == 0)
                s = new StudentDodajVM() { };
            else
                s = _db.Student
                    .Where(w => w.ID == StudentID)
                    .Select(a => new StudentDodajVM
                    {
                        id = a.ID,
                        ime = a.Korisnik.Ime,
                        prezime = a.Korisnik.Prezime,
                        email = a.Korisnik.Email,
                        opstinaPrebivalistaID = a.OpstinaPrebivalistaID,
                        opstinaRodjenjaID = a.OpstinaRodjenjaID,
                        slikaStudentaCurrent = a.SlikaStudenta
                        //opstine = opstine
                    }).Single();

            s.opstine = opstine;

            return View("Uredi", s);
        }

        public IActionResult Index(string q)
        {
            //select * from Student 
            List<StudentPrikazVM.Row> studenti = _db.Student
                .Where(s => q == null || (s.Korisnik.Ime + " " + s.Korisnik.Prezime).StartsWith(q) ||
                            (s.Korisnik.Prezime + " " + s.Korisnik.Ime).StartsWith(q))
                .Select(x => new StudentPrikazVM.Row
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