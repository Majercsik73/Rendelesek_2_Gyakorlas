using System;
using System.Collections.Generic;
using System.Text;

namespace Rendelesek_2
{
    public class Tetel
    {
        public int rendszama { get; set; }
        public string kod { get; set; }
        public int darab { get; set; }

        public Tetel(string sor)
        {
            string[] sordarab = sor.Split(";");

            rendszama = Convert.ToInt32(sordarab[1]);
            kod = sordarab[2];
            darab = Convert.ToInt32(sordarab[3]);

        }
    }
}
