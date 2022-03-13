using System;
using System.Collections.Generic;
using System.Text;

namespace Rendelesek_2
{
    public class Rendeles
    {
        public string datum { get; set; }
        public int rendszama { get; set; }
        public string email { get; set; }

        public bool statusz { get; set; }

        public Rendeles(string sor)
        {
            string[] sordarab = sor.Split(";");

            datum = sordarab[1];
            rendszama = Convert.ToInt32(sordarab[2]);
            email = sordarab[3];
            statusz = true;


        }
    }
}
