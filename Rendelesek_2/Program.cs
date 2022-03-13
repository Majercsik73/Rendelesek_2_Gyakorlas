using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Rendelesek_2
{
    class Program
    {
        static List<Aru> raktar = new List<Aru>();

        static void Main(string[] args)
        {
            string[] teljesRaktar = File.ReadAllLines("../../../../raktar.csv");


            for (int i = 0; i < teljesRaktar.Length; i++)
            {
                raktar.Add(new Aru(teljesRaktar[i]));
            }
            Console.WriteLine();
            Console.WriteLine("RAKTÁRKÉSZLET BEOLVASVA!!!");

            string[] teljesRendel = File.ReadAllLines("../../../../rendeles.csv");
            List<Rendeles> rendeles = new List<Rendeles>();
            List<Tetel> tetel = new List<Tetel>();

            for (int j = 0; j < teljesRendel.Length; j++)
            {
                if (teljesRendel[j].StartsWith("M"))
                {
                    rendeles.Add(new Rendeles(teljesRendel[j]));
                }
                else
                {
                    tetel.Add(new Tetel(teljesRendel[j]));
                }
            }
            Console.WriteLine();
            Console.WriteLine("RENDELÉSEK BEOLVASVA!!!");


            rendelesFeldolgozas(rendeles, tetel, raktar);

        }


        public static void rendelesFeldolgozas(List<Rendeles> rendeles, List<Tetel> tetel, List<Aru> raktar)
        {
            List<Tetel> fuggoTetelek = new List<Tetel>();
            for (int i = 0; i < rendeles.Count; i++)
            {
                List<Tetel> rendeltTermekek = new List<Tetel>();

                for (int j = 0; j < tetel.Count; j++)
                {
                    if (rendeles[i].rendszama == tetel[j].rendszama)
                    {
                        Aru aru = aruKereses(tetel[j].kod);
                        if (aru.db > tetel[j].darab)
                        {
                            rendeltTermekek.Add(tetel[j]);
                        }
                        else
                        {
                            rendeles[i].statusz = false;
                            break;
                        }
                    }

                }
                if (rendeles[i].statusz)
                {
                    int rendelesErteke = 0;
                    for (int k = 0; k < rendeltTermekek.Count; k++)
                    {
                        for (int l = 0; l < raktar.Count; l++)
                        {
                            if (rendeltTermekek[k].kod == raktar[l].kod)
                            {
                                rendelesErteke += rendeltTermekek[k].darab * raktar[l].ar;
                                raktar[l].db -= rendeltTermekek[k].darab;
                            }
                        }
                    }
                    File.AppendAllText("levelek.csv", $"{rendeles[i].email} A rendelését két napon belül szállítjuk. A rendelés értéke: {rendelesErteke} Ft. \n");
                }
                else
                {
                    for (int m = 0; m < rendeltTermekek.Count; m++)
                    {

                        fuggoTetelek.Add(rendeltTermekek[m]);
                    }
                    File.AppendAllText("levelek.csv", $"{rendeles[i].email} A rendelése függő állapotba került. Hamarosan értesítjük a szállítás időpontjáról. \n");
                }

            }
            Console.WriteLine();
            Console.WriteLine("FELDOLGOZÁS KÉSZ!!!");

            beszerzes(raktar, fuggoTetelek);
        }

        public static void beszerzes(List<Aru> raktar, List<Tetel> fuggoTetelek)
        {
            Dictionary<string, int> fuggo = new Dictionary<string, int>();

            for (int j = 0; j < fuggoTetelek.Count; j++)
            {
                int darabszam = 0;
                for (int k = 0; k < fuggoTetelek.Count; k++)
                {
                    if (fuggoTetelek[j].kod == fuggoTetelek[k].kod)
                    {
                        darabszam += fuggoTetelek[k].darab;
                    }
                }
                //Console.WriteLine($"{fuggoTetelek[j].kod}  ++  {darabszam }");
                bool vane = false;
                for (int m = 0; m < fuggo.Count; m++)
                {
                    if (fuggoTetelek[j].kod == fuggo.ElementAt(m).Key)
                    {
                        vane = true;
                    }
                }

                if (!vane)
                {
                    fuggo.Add(fuggoTetelek[j].kod, darabszam);
                }
            }

            int beszerzesdb = 0;
            for (int n = 0; n < raktar.Count; n++)
            {
                for (int p = 0; p < fuggo.Count; p++)
                {
                    if (raktar[n].kod == fuggo.ElementAt(p).Key)
                    {
                        beszerzesdb = fuggo.ElementAt(p).Value - raktar[n].db;
                    }
                }

                if (beszerzesdb > 0)
                {
                    File.AppendAllText("beszerzes.csv", $"{raktar[n].kod};{beszerzesdb}\n");
                }
            }
            Console.WriteLine();
            Console.WriteLine("BESZERZÉS KIÍRVA!!!");
        }

        public static Aru aruKereses(string kod)
        {
            for (int i = 0; i < raktar.Count; i++)
            {
                if (raktar[i].kod == kod)
                {
                    return raktar[i];
                }
            }
            return null;
        }
    }
}
