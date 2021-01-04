using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.BL.Data;
using eUniverzitet.BL.EntityModels;
using eUniverzitet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eUniverzitet.Web.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _db;
        private UserManager<Korisnik> _userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<Korisnik> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string DodajNastavnika(string ime, string prezime)
        {
            string email = ime + "." + prezime + "@edu.fit.ba";
            var korisnik = new Korisnik
            {
                Email = email,
                UserName = email,
                Ime = ime,
                Prezime = prezime,
                EmailConfirmed = true,
            };
            IdentityResult result = _userManager.CreateAsync(korisnik, "Mostar2020!").Result;

            if (!result.Succeeded)
            {
                return "errors: "+ string.Join('|', result.Errors);
            }
                          
            Nastavnik nastavnik = new Nastavnik
            {
                Korisnik = korisnik,
                Zvanje = "prof dr"
            };
            _db.Nastavnik.Add(nastavnik);
            _db.SaveChanges();
            return "Nastavnik je uspješno dodat";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
