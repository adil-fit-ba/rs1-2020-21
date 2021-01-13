using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.BL.Data;
using eUniverzitet.BL.EntityModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace eUniverzitet.Web.Helper
{
    public class KretanjePoSistemu
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature exceptionMessage=null)
        {
            Korisnik korisnik = httpContext.LogiraniKorisnik();

            var request = httpContext.Request;

            var queryString = request.Query;

            if (queryString.Count == 0 && !request.HasFormContentType)
                return 0;

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

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.exceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }
      

            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            db.Add(x);
            db.SaveChanges();

            return x.ID;
        }


       
       
    }
}
