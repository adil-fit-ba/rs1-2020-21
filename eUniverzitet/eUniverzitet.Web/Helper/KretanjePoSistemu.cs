using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.Shared.Data;
using eUniverzitet.Shared.EntityModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace eUniverzitet.Web.Helper
{
    public class KretanjePoSistemu
    {
        public static void Save(Korisnik korisnik, ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            var queryString = request.Query;

            if (queryString.Count == 0 && !request.HasFormContentType)
                return;

            //IHttpRequestFeature feature = request.HttpContext.Features.Get<IHttpRequestFeature>();
            string detalji = "";
            if (request.HasFormContentType)
            { 
                foreach (string key in request.Form.Keys)
                {
                    detalji += " | " + key + "=" + request.Form[key];
                }
            }
            
            var x = new LogKretanjePoSistemu
            {
                Korisnik = korisnik,
                Vrijeme = DateTime.Now,
                QueryPath = request.GetEncodedPathAndQuery(),
                PostData = detalji,
                IpAdresa = request.HttpContext.Connection.RemoteIpAddress.ToString(),
            };


            ApplicationDbContext db = filterContext.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            db.Add(x);
            db.SaveChanges();

        }

        private static void SnimiLog(Korisnik korisnik, string detalji)
        {
            //Snimi Log u DB ili File sistem
        }
    }
}
