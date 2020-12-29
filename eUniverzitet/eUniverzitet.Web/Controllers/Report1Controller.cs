using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using eUniverzitet.BL.Data;
using eUniverzitet.Shared.Reports;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace eUniverzitet.Web.Controllers
{
    [Autorizacija(false, true)]
    public class Report1Controller : Controller
    {
        private ApplicationDbContext _db;

        public Report1Controller(ApplicationDbContext applicationDbContext)
        {
            this._db = applicationDbContext;
        }

        public IActionResult Index()
        {
            List<Report1Row> podaci = getStudenti(_db);


            LocalReport _localReport = new LocalReport("Reporti/Report1.rdlc");
            _localReport.AddDataSource("dsStudenti", podaci);
         
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportSastavio", HttpContext.LogiraniKorisnik().ImePrezime);

            ReportResult result = _localReport.Execute(RenderType.Pdf, parameters: parameters);
            return File(result.MainStream, "application/pdf");

        }


        public static List<Report1Row> getStudenti(ApplicationDbContext db)
        {
            List<Report1Row> podaci = db.Student.Select(s => new Report1Row
            {
                PrezimeIme = s.Korisnik.Prezime + " " + s.Korisnik.Ime,
                BrojIndeksa = s.BrojIndeksa,
                ProsjecnaOcjena = db.Ocjene.Where(w => w.Student == s).Average(a => a.OcjenaBrojacna),

            }).ToList();

            return podaci;
        }
    }

  
}
