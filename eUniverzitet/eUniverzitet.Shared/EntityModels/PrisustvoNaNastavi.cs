using System;
using System.Collections.Generic;
using System.Text;


namespace eUniverzitet.Shared.EntityModels
{
    public class PrisustvoNaNastavi
    {
        public int ID { get; set; }


        public int StudentID { get; set; }
        public Student Student { get; set; }

    

        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }

        public DateTime Datum{ get; set; }
        public bool IsPrisutan { get; set; }             
        public string Komentar { get; set; }             

    }
}
