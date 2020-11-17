using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci;
using WebApplication10.EF;

namespace WebApplication10.Controllers
{
    public class TestniPodaciController : Controller
    {
        public IActionResult Index()
        {
            DbInicijalizator.Generisi();

         
            return View();
        }
    }
}