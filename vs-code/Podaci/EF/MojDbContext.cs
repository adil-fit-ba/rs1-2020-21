using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Podaci.EntityModels;
using WebApplication10.EntityModels;

namespace WebApplication10.EF
{
    public class MojDbContext : DbContext
    {
        public DbSet<Opstina> Opstina { get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<Ocjene> Ocjene { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<PrisustvoNaNastavi> PrisustvoNaNastavi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"	Server=app.fit.ba,1431;
                                        	Database=db124;
                                            Trusted_Connection=false;
                                            User ID=db124;
                                            Password=oj!Z8u54;
                                            MultipleActiveResultSets=true;     ");
        }

    }

}
