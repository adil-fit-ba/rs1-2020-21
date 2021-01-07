using System.Collections.Generic;

namespace eUniverzitet.Web.Models
{
    public class Student3RowVM
    {
        public int id { get; set; }
        public string brojIndeksa { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string opstinaPrebivalista { get; set; }

        public string opstinaRodjenja { get; set; }
        public int opstinaRodjenjaID { get; set; }
        public string email { get; set; }
        public int opstinaPrebivalistaID { get; set; }
        public string slikaStudentaNew { get; set; }
    }
    
    public class Student3IndexVM
    {
      
        public List<Student3RowVM> studenti { get; set; }
        public string q { get; set; }
    }
}
