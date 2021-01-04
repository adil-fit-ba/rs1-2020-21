﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using eUniverzitet.BL.Data;
using eUniverzitet.Web.Helper;
using TmpAppForReportDesign;

namespace eUniverzitet.Web.Controllers
{
    public class Report1Controller : Controller
    {
        private ApplicationDbContext _db;

        public Report1Controller(ApplicationDbContext db)
        {
            _db = db;
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
        public IActionResult Index()
        {
            LocalReport _localReport = new LocalReport("Reporti/Report1.rdlc");
            List<Report1Row> podaci = getStudenti(_db);
            _localReport.AddDataSource("DataSet1", podaci);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("ReportSastavio", HttpContext.LogiraniKorisnik().ImePrezime);

            ReportResult result = _localReport.Execute(RenderType.ExcelOpenXml, parameters: parameters);
            return File(result.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
    }
}
