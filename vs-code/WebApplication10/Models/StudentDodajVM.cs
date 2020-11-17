using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication10.Helper;

namespace WebApplication10.Models
{
    public class StudentDodajVM
    {
        public List<SelectListItem> opstine;
        public int OpstinaRodjenjaID { get; set; }
        public int ID { get; set; }
        [MaxLength(5)]
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int OpstinaPrebivalistaID { get; set; }
    }
}
