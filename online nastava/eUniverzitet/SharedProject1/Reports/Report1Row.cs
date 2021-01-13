using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eUniverzitet.Shared.Reports
{

    public class Report1Row
    {
        public string PrezimeIme { get; set; }
        public string BrojIndeksa { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public int BrojPolozenihIspita { get; set; }

        public static List<Report1Row> Get()
        {
            return new List<Report1Row> { };
        }
    }
}
