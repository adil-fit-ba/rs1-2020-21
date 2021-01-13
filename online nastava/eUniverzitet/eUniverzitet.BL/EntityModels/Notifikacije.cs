using System;

namespace eUniverzitet.BL.EntityModels
{
    public class Notifikacije
    {
        public int ID { get; set; }
        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
        public DateTime Vrijeme { get; set; }
        public bool JelProcitana { get; set; }
        public string Naslov { get; set; }
        public string Poruka { get; set; }
        public string UrlAkcija { get; set; }
    }
}
