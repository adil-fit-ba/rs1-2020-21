using System;

namespace eUniverzitet.Shared.EntityModels
{
    public class Ocjene
    {
        public int ID { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }
    
        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }

        public int NastavnikID { get; set; }
        public Nastavnik Nastavnik { get; set; }

        public DateTime Datum{ get; set; }
        public int OcjenaBrojacna { get; set; }
    }
}
