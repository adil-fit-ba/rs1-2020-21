
namespace eUniverzitet.Shared.EntityModels
{
    public class Student
    {
        public int ID { get; set; }

        public string BrojIndeksa { get; set; }
      

        public int OpstinaRodjenjaID { get; set; }
        public Opstina OpstinaRodjenja { get; set; }


        public int OpstinaPrebivalistaID { get; set; }
        public Opstina OpstinaPrebivalista { get; set; }


        public string KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}
