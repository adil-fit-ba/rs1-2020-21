using Microsoft.AspNetCore.Identity;

namespace eUniverzitet.BL.EntityModels
{
   public class Korisnik: IdentityUser
   {
       public string Ime { get; set; }
       public string Prezime { get; set; }

       public Nastavnik Nastavnik { get; set; }
       
       public Student Student { get; set; }

       public string ImePrezime => Ime + " " + Prezime;
   }
}
