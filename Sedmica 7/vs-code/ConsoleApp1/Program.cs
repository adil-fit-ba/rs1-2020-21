using System;
using WebApplication10.EF;
using WebApplication10.EntityModels;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unesite naziv opstine");

            var x = new Opstina
            {
                Naziv = Console.ReadLine()
            };

            MojDbContext db = new MojDbContext();
            db.Add(x);
            db.SaveChanges();
        }
    }
}
