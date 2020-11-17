﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Podaci.EntityModels;
using WebApplication10.EF;
using WebApplication10.EntityModels;

namespace Podaci
{
    public class DbInicijalizator
    {
        public static string GetRandomString(int lenght=3)
        {
            return Guid.NewGuid().ToString().Substring(0, lenght);
    }
        public static void Generisi()
        {
            MojDbContext db = new MojDbContext();

            var Opstine = new List<Opstina>();
            var Predmeti = new List<Predmet>();
            var Studenti = new List<Student>();

            for (int i = 0; i < 5; i++)
            {
                Opstine.Add(new Opstina { Naziv = "Opstina" + GetRandomString() });
                Predmeti.Add(new Predmet { Naziv = "Predmet" + GetRandomString() });
            
            }

            for (int i = 0; i < 20; i++)
            {
                Studenti.Add(new Student()
                {
                    BrojIndeksa = GetRandomString(5),
                    Ime = GetRandomString(4),
                    Prezime = GetRandomString(4),
                    OpstinaPrebivalista = Opstine[i%5],
                    OpstinaRodjenja = Opstine[i%5],
                });
            }

            for (int i = 0; i < 100; i++)
            {
                db.Add(new Ocjene
                {
                    Datum = DateTime.Now,
                    OcjenaBrojacna = (i % 5) + 6,
                    Predmet = Predmeti[i % 4],
                    Student = Studenti[i % 20]
                });
            }

            db.AddRange(Opstine);
            db.AddRange(Predmeti);
            db.AddRange(Studenti);


            db.SaveChanges();
        }
    }
}
