﻿using System;
using System.Collections.Generic;
using System.Text;
using WebApplication10.EntityModels;

namespace Podaci.EntityModels
{
    public class Ocjene
    {
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int ID { get; set; }
        public int PredmetID { get; set; }
        public Predmet Predmet { get; set; }
        public DateTime Datum{ get; set; }
        public int OcjenaBrojacna{ get; set; }
    }
}
