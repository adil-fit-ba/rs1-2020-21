using System.Collections.Generic;

namespace eUniverzitet.Web.Reporti
{
    public class Report1Model
    {
        
        public string PrezimeIme { get; set; }
        public string BrojIndeksa { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public int BrojPolozenihIspita { get; set; }
        public int ECTS { get; set; }

        public static List<Report1Model> Get()
        {
            return new List<Report1Model> { };
        }
    }
}