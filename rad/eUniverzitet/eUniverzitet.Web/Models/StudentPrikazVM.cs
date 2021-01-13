using System.Collections.Generic;

namespace eUniverzitet.Web.Models
{
    public class StudentPrikazVM
    {
        public class Row
        {
         

            public int id { get; set; }
            public string brojIndeksa { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public string opstinaPrebivalista { get; set; }
            public string opstinaRodjenja { get; set; }
            public string email { get; set; }
        }

        public List<Row> studenti { get; set; }
        public string q { get; set; }
        public int total { get; set; }
    }
}
