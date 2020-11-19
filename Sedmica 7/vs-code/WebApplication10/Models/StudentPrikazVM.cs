using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.EntityModels;

namespace WebApplication10.Models
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
        }

        public List<Row> studenti;
        public string q;
    }
}
