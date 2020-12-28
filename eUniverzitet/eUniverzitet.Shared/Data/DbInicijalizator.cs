using System;
using System.Collections.Generic;
using System.Net.Mail;
using eUniverzitet.Shared.EntityModels;
using Microsoft.AspNetCore.Identity;

namespace eUniverzitet.Shared.Data
{
    public static class Ext
    {
        public static T GetRandomElement<T>(this List<T> list)
        {
            int x = new Random().Next(0, list.Count);
            return list[x];
        }
    }

    public class DbInicijalizator
    {
        public static string GetRandomString(int lenght = 3)
        {
            return Guid.NewGuid().ToString().Substring(0, lenght);
        }

        public static async System.Threading.Tasks.Task Generisi(ApplicationDbContext db,
            UserManager<Korisnik> _userManager)
        {

            var Opstine = new List<Opstina>();
            var Predmeti = new List<Predmet>();
            var Studenti = new List<Student>();
            var Nastavnici = new List<Nastavnik>();

            for (int i = 0; i < 5; i++)
            {
                Opstine.Add(new Opstina {Naziv = "Opstina" + GetRandomString()});
                Predmeti.Add(new Predmet {Naziv = "Predmet" + GetRandomString()});

            }

            for (int i = 0; i < 20; i++)
            {
                string email = "mail" + GetRandomString(3) + "@fit.ba";
                Korisnik korisnik = new Korisnik
                {
                    Ime = GetRandomString(4),
                    Prezime = GetRandomString(4),
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(korisnik, "Test.2020");

                if (result.Succeeded)
                {
                    Studenti.Add(new Student()
                    {
                        BrojIndeksa = GetRandomString(5),

                        OpstinaPrebivalista = Opstine.GetRandomElement(),
                        OpstinaRodjenja = Opstine.GetRandomElement(),
                        Korisnik = korisnik,

                    });
                }
            }


            for (int i = 0; i < 20; i++)
            {
                string email = "mail" + GetRandomString(3) + "@fit.ba";
                Korisnik korisnik = new Korisnik
                {
                    Ime = GetRandomString(4),
                    Prezime = GetRandomString(4),
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(korisnik, "Test.2020");

                if (result.Succeeded)
                {
                    Nastavnici.Add(new Nastavnik
                    {
                        Zvanje = "prof dr",

                        Korisnik = korisnik
                    });
                }
            }

            for (int i = 0; i < 100; i++)
            {
                db.Add(new Ocjene
                {
                    Datum = DateTime.Now,
                    OcjenaBrojacna = (i % 5) + 6,
                    Nastavnik = Nastavnici.GetRandomElement(),
                    Predmet = Predmeti.GetRandomElement(),
                    Student = Studenti.GetRandomElement()
                });
            }

            db.AddRange(Opstine);
            db.AddRange(Predmeti);
            db.AddRange(Studenti);
            db.SaveChanges();

            int j = 0;
            foreach (var x in db.Student)
            {
                j++;
                db.PrisustvoNaNastavi.Add(new PrisustvoNaNastavi
                {
                    Datum = DateTime.Now,
                    Predmet = Predmeti.GetRandomElement(),
                    Student = x,
                });
            }


            db.SaveChanges();
        }
    }
}
