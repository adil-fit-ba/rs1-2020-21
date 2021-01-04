using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TmpAppForReportDesign
{
    public class Report1Row
    {
        public string PrezimeIme { get; set; }
        public string BrojIndeksa { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public int BrojPolozenihIspita { get; set; }
        public int ECTS { get; set; }

        public static List<Report1Row> Get()
        {
            return new List<Report1Row> { };
        }
    }
}