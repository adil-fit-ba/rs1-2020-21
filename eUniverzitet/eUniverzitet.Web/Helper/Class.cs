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


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool ucenik, bool nastavnici)
        {
            _ucenik = ucenik;
            _nastavnici = nastavnici;
        }
        private readonly bool _ucenik;
        private readonly bool _nastavnici;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            //Preuzimamo DbContext preko app services
            ApplicationDbContext db = filterContext.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            //Preuzimamo userManager preko app services
            UserManager<Korisnik> userManager = filterContext.HttpContext.RequestServices.GetService<UserManager<Korisnik>>();
            //TrenutniKorisnik
            Korisnik k = await userManager.GetUserAsync(ClaimsPrincipal.Current);


            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
                return;
            }

         

            //studenti mogu pristupiti 
            if (_ucenik && k.Student != null)
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //nastavnici mogu pristupiti 
            if (_nastavnici && db.Nastavnik != null)
            {
                await next();//ok - ima pravo pristupa
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}
