using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace eUniverzitet.Shared.EntityModels
{
   public class Korisnik: IdentityUser
   {
       public string Ime { get; set; }
       public string Prezime { get; set; }

       public Nastavnik Nastavnik { get; set; }
       
       public Student Student { get; set; }
   }
}
