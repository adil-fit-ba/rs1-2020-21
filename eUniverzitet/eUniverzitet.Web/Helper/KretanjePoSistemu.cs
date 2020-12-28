using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using eUniverzitet.Shared.Data;
using eUniverzitet.Shared.EntityModels;
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
            UserManager<Korisnik> userManager = httpContext.RequestServices.GetService<UserManager<Korisnik>>();
            Korisnik korisnik = httpContext.User==null?null:userManager.GetUserAsync(httpContext.User).Result;


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
