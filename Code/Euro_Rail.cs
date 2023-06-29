using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroRail
{
    public class Euro_Rail
    {
        public List<Kraj> kraji { get; }
        public List<Vlak> vlaki { get; }

        public Euro_Rail()
        {
            kraji = new List<Kraj>();
            vlaki = new List<Vlak>();
        }

        public bool preberiKraje(String imeDatoteke)
        {
            List<String> imena = new List<String>();

            try
            {
                String[] vsiKraji = File.ReadAllLines(imeDatoteke);

                foreach (String s in vsiKraji)
                {
                    if (s == "") { continue; }

                    String[] podatki = s.Split(';');

                    if (!imena.Contains(podatki[0].ToLower())) { imena.Add(podatki[0].ToLower()); }
                    else { continue; }

                    if (podatki.Length == 2)
                    {
                        kraji.Add(new Kraj(podatki[0], podatki[1]));
                    }
                }

                return true;
            }
            catch (FileNotFoundException e) { return false; }
        }

        public void izpisiKraje()
        {
            Console.WriteLine("Kraji, povezani z vlaki:");

            foreach (Kraj kraj in kraji)
            {
                Console.WriteLine(kraj.ToString());
            }
        }

        private Kraj dobiKraj(String ime)
        {
            return kraji.Find(x => x.name == ime);
        }

        private int pretvoriCas(String koliko)
        {
            int cas = 0;

            if (!koliko.Contains(".")) { return Convert.ToInt32(koliko); }
            else
            {
                String[] min = koliko.Split(".");
                cas = Convert.ToInt32(min[0]) * 60 + Convert.ToInt32(min[1]);
            }

            return cas;
        }

        public bool preberiPovezave(String imeDatoteke)
        {
            try
            {
                String[] povezave = File.ReadAllLines(imeDatoteke);
                Vlak vlak;

                foreach (String s in povezave)
                {
                    if (s == "") { continue; }

                    String[] podatki = s.Split(';');

                    if (podatki[0].Length > 6) { continue; }

                    if(dobiKraj(podatki[1]) == null || dobiKraj(podatki[2]) == null) { continue; }
                    if(dobiKraj(podatki[1]) == dobiKraj(podatki[2])) { continue; }

                    Kraj start = dobiKraj(podatki[1]);
                    Kraj end = dobiKraj(podatki[2]);

                    if (podatki.Length == 4) 
                    {
                        vlak = new RegionalniVlak(podatki[0], start, end, pretvoriCas(podatki[3]));
                        vlaki.Add(vlak);
                        start.dodajOdhod(vlak);
                    }
                    else if(podatki.Length == 5)
                    {
                        vlak = new EkspresniVlak(podatki[0], start, end, pretvoriCas(podatki[3]), Convert.ToDouble(podatki[4]));
                        vlaki.Add(vlak);
                        start.dodajOdhod(vlak);
                    }
                    
                }

                return true;
            }
            catch (FileNotFoundException e) { return false; }
        }

        public void izpisiPovezave()
        {
            Console.WriteLine("Vlaki, ki povezujejo kraje:");

            foreach(Vlak vlak in vlaki)
            {
                Console.WriteLine(vlak.ToString());
            }
        }

        public void odhodi()
        {
            Console.WriteLine("Kraji in odhodi vlakov:");
            kraji.Sort();
            vlaki.Sort();

            foreach (Kraj kraj in kraji)
            {
                var list = vlaki.FindAll(x => x.start.name ==  kraj.name);

                Console.WriteLine(kraj.ToString());
                Console.WriteLine($"odhodi vlakov ({list.Count}):");
                foreach(var item in list)
                {
                    Console.WriteLine($" - {item.ToString()}");
                }
                Console.WriteLine();
            }
        }

        public void izlet(String ime, int k)
        {
            Kraj kraj = dobiKraj(ime);

            if(kraj == null)
            {
                Console.WriteLine($"NAPAKA: podanega kraja ({ime}) ni na seznamu krajev.");
            }
            else
            {
                var set = kraj.destinacije(k);

                if(set.Count == 0)
                {
                    Console.WriteLine($"Iz kraja {kraj.ToString()} ni nobenih povezav.");
                }
                else
                {
                    Console.WriteLine($"Iz kraja {kraj.ToString()} lahko z max {k} prestopanji pridemo do naslednjih krajev:");
                
                    foreach(var item in set)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
            }
        }
    }
}
