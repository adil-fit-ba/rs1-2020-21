using System;
using System.Collections.Generic;
using System.Text;

namespace eUniverzitet.Shared.EntityModels
{
    public class LogKretanjePoSistemu
    {
        public int ID { get; set; }
        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        public string QueryPath { get; set; }
        public string PostData { get; set; }
        public DateTime Vrijeme { get; set; }
        public string IpAdresa { get; set; }
        public string exceptionMessage { get; set; }
        public bool isException { get; set; }
    }
}
