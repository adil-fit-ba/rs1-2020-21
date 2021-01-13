using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eUniverzitet.Web.Views
{
    public class TestCas1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
