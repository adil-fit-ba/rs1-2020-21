using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models
{
    public class StudentOcjenaUrediVM
    {
        public int OcjenaID { get; set; }

        public string ImeStudent { get; set; }
        public string NazivPredmet { get; set; }
        public int Ocjena { get; set; }
    }
}
