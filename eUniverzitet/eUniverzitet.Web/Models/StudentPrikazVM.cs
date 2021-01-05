using System.Collections.Generic;

namespace eUniverzitet.Web.Models
{
    public class StudentPrikazVM
    {
        public class Row
        {
         

            public int ID { get; set; }
            public string BrojIndeksa { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string OpstinaPrebivalista { get; set; }
            public string OpstinaRodjenja { get; set; }
            public string Email { get; set; }
        }

        public List<Row> studenti { get; set; }
        public string q { get; set; }
    }
}
