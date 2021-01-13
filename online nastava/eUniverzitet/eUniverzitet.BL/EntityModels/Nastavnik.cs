
namespace eUniverzitet.BL.EntityModels
{
    public class Nastavnik
    {
        public int ID { get; set; }

        public string Zvanje { get; set; }

        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

    }
}
