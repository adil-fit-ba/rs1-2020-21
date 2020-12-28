using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eUniverzitet.Shared.Data;
using eUniverzitet.Shared.EntityModels;
using eUniverzitet.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace eUniverzitet.Web.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool ucenik, bool nastavnici)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { ucenik, nastavnici };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        public MyAuthorizeImpl(bool ucenik, bool nastavnici)
        {
            _ucenik = ucenik;
            _nastavnici = nastavnici;
        }
        private readonly bool _ucenik;
        private readonly bool _nastavnici;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Preuzimamo DbContext preko app services
            ApplicationDbContext db = filterContext.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            
            //Preuzimamo userManager preko app services
            UserManager<Korisnik> userManager = filterContext.HttpContext.RequestServices.GetService<UserManager<Korisnik>>();
            
            //TrenutniKorisnik

           string userId = userManager.GetUserId(filterContext.HttpContext.User);

            Korisnik k = db.Korisnik.Where(s => s.Id == userId)
            .Include(s => s.Nastavnik)
            .Include(s => s.Student)
            .Single();

            //  Korisnik k = userManager.GetUserAsync(filterContext.HttpContext.User).Result;

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }
                filterContext.Result = new RedirectResult( "/");
                return;
            }

            KretanjePoSistemu.Save(k, filterContext);

            //studenti mogu pristupiti 
            if (_ucenik && k.Student != null)
            {
                return;//ok - ima pravo pristupa
            }

            //nastavnici mogu pristupiti 
            if (_nastavnici && k.Nastavnik != null)
            {
                return;//ok - ima pravo pristupa
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectResult("/Identity/Account/Login");
        }
    }
}
