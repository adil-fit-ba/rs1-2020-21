using System.Linq;
using eUniverzitet.BL.Data;
using eUniverzitet.BL.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eUniverzitet.Web.Helper
{
    public static class Autentifikacija
    {
        public static Korisnik LogiraniKorisnik(this HttpContext httpContext)
        {
            //Preuzimamo DbContext preko app services
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            //Preuzimamo userManager preko app services
            UserManager<Korisnik> userManager = httpContext.RequestServices.GetService<UserManager<Korisnik>>();

            if (httpContext.User == null)
                return null;

            //TrenutniKorisnikID
            string userId = userManager.GetUserId(httpContext.User);

            Korisnik k = db.Korisnik.Where(s => s.Id == userId)
                .Include(s => s.Nastavnik)
                .Include(s => s.Student)
                .SingleOrDefault();

            return k;
        }
    }
}
