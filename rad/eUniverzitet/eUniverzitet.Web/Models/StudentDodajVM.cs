using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eUniverzitet.Web.Models
{
    public class StudentDodajVM
    {
        public List<SelectListItem> opstine { get; set; }
        public int opstinaRodjenjaID { get; set; }
        public int id { get; set; }
        [MaxLength(5)]
        public string ime { get; set; }
        [EmailAddress]
        public string email { get; set; }
        public string prezime { get; set; }
        public int opstinaPrebivalistaID { get; set; }
        public IFormFile slikaStudentaNew { set; get; }
        public string slikaStudentaCurrent { get; set; }
    }
}
