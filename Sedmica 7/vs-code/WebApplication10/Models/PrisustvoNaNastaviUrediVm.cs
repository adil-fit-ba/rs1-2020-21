using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace WebApplication10.Models
    {
        public class PrisustvoNaNastaviUrediVm
        {
            public int PrisustvoNaNastaviID { get; set; }

            public string ImeStudent { get; set; }
            public DateTime Datum { get; set; }
            public string NazivPredmet { get; set; }
            public bool IsPrisutan { get; set; }
            public string komentar { get; set; }
        }
    }
}
