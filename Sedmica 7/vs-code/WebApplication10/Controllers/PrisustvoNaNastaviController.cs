﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication10.EF;
using WebApplication10.EntityModels;
using WebApplication10.Models;

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

    }
}