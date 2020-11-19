using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.EntityModels
{
    public class Student
    {
        public int ID { get; set; }
        public string BrojIndeksa { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public int OpstinaRodjenjaID { get; set; }
        public Opstina OpstinaRodjenja { get; set; }


        public int OpstinaPrebivalistaID { get; set; }
        public Opstina OpstinaPrebivalista { get; set; }
    }
}
