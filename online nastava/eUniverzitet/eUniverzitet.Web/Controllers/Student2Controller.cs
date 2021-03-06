﻿using System;
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
//[Autorizacija( false,  true)]
    public class Student2Controller : Controller
    {
        private ApplicationDbContext _db;
        private UserManager<Korisnik> _userManager;

        public Student2Controller(ApplicationDbContext db, UserManager<Korisnik> usermanager)
        {
            this._db = db;
            this._userManager = usermanager;
        }

        [HttpPost]
        public IActionResult SnimiImePrezime([FromBody] StudentPrikazVM.Row x)
        {


            Student student = _db.Student.Include(s => s.Korisnik).Single(s => s.ID == x.id);
            student.Korisnik.Ime = x.ime;
            student.Korisnik.Prezime = x.prezime;

            _db.SaveChanges();
            return Ok();
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
            _db.SaveChanges();//delete Student where Id=...

            TempData["PorukaWarning"] = "Uspješno ste obrisali studenta " + s.Korisnik.Ime; //transport podataka iz akcije 1 u (akciju 2 + njegov view)

            return Ok();
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

            return Ok(s);
        }

        
        public IActionResult Index(string q, int currentPage=1, int itemsPerPage=10)
        {
            //select * from Student 
            IQueryable<Student> queryable = _db.Student
                .Where(s => q == null || (s.Korisnik.Ime + " " + s.Korisnik.Prezime).StartsWith(q) ||
                            (s.Korisnik.Prezime + " " + s.Korisnik.Ime).StartsWith(q));
            
            List<StudentPrikazVM.Row> studenti = queryable
                .Skip((currentPage-1)*itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new StudentPrikazVM.Row
                {
                    id = x.ID,
                    brojIndeksa = x.BrojIndeksa,
                    ime = x.Korisnik.Ime,
                    prezime = x.Korisnik.Prezime,
                    email = x.Korisnik.Email,
                    opstinaRodjenja = x.OpstinaRodjenja.Naziv,
                    opstinaPrebivalista = x.OpstinaPrebivalista.Naziv,
                })
                .ToList();

            StudentPrikazVM m = new StudentPrikazVM();
            m.studenti = studenti;
            m.q = q;
            m.total = queryable.Count();

            return Ok(m);
        }
    }
}