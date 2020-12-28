using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eUniverzitet.Web.Models
{
    public class StudentDodajVM
    {
        public List<SelectListItem> opstine;
        public int OpstinaRodjenjaID { get; set; }
        public int ID { get; set; }
        [MaxLength(5)]
        public string Ime { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Prezime { get; set; }
        public int OpstinaPrebivalistaID { get; set; }
        public IFormFile SlikaStudentaNew { set; get; }
        public string SlikaStudentaCurrent { get; set; }
    }
}
