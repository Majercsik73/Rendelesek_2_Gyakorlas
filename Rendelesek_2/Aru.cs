using System;
using System.Collections.Generic;
using System.Text;

namespace Rendelesek_2
{
    public class Aru
    {
        public string kod { get; set; }
        public string nev { get; set; }
        public int ar { get; set; }
        public int db { get; set; }

        public Aru(string sor)
        {
            string[] sordarab = sor.Split(";");

            kod = sordarab[0];
            nev = sordarab[1];
            ar = Convert.ToInt32(sordarab[2]);
            db = Convert.ToInt32(sordarab[3]);
        }

    }
}
