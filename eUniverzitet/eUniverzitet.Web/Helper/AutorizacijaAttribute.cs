using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eUniverzitet.BL.EntityModels;
using eUniverzitet.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace eUniverzitet.Web.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool dozvoljenoStudentu, bool dozvoljenoNastavnicima)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { dozvoljenoStudentu, dozvoljenoNastavnicima };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        public MyAuthorizeImpl(bool dozvoljenoStudentu, bool dozvoljenoNastavnicima)
        {
            _dozvoljenoStudentu = dozvoljenoStudentu;
            _dozvoljenoNastavnicima = dozvoljenoNastavnicima;
        }
        private readonly bool _dozvoljenoStudentu;
        private readonly bool _dozvoljenoNastavnicima;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext httpContext = filterContext.HttpContext;

            Korisnik k = httpContext.LogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["PorukaError"] = "Niste logirani";
                }
                filterContext.Result = new RedirectResult( "/");
                return;
            }

            KretanjePoSistemu.Save(httpContext);

            //studenti mogu pristupiti 
            if (_dozvoljenoStudentu && k.Student != null)
            {
                return;//ok - ima pravo pristupa
            }

            //nastavnici mogu pristupiti 
            if (_dozvoljenoNastavnicima && k.Nastavnik != null)
            {
                return;//ok - ima pravo pristupa
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["PorukaError"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectResult("/Identity/Account/Login");
        }
    }
}
