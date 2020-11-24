using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models
{
    public class PrisustvnoNaNastaviPrikazVM
    {
        public class Zapis
        {
            public string Predmet { get; set; }
            public DateTime Datum { get; set; }
            public bool IsPrisutan{ get; set; }
            public string Komentar{ get; set; }
            public int PrisustvoNaNastaviID { get; set; }
        }
        public string ImeStudenta { get; set; }
        public IEnumerable<Zapis> Zapisi { get; set; }
    }
}
