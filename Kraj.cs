using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroRail
{
    public class Kraj : IComparable
    {
        public String name { get; set; }
        public String kratica { get; set; }
        public List<Vlak> odhodi { get; }

        public Kraj(string name, string kratica)
        {
            this.name = name;
            this.kratica = kratica;
            this.odhodi = new List<Vlak>();

        }

        public override string ToString()
        {
            return $"{name} ({kratica})";
        }

        public bool dodajOdhod(Vlak vlak)
        {
            if (odhodi.Contains(vlak)) { return false; }
            
            odhodi.Add(vlak);
            return true;
        }

        public SortedSet<Kraj> destinacije(int k)
        {
            SortedSet<Kraj> vsi = new SortedSet<Kraj>();
            SortedSet<Kraj> trenutni = new SortedSet<Kraj>();

            foreach (Vlak vlak in odhodi)
            {
                vsi.Add(vlak.end);
            }

            if(k > 0)
            {
                foreach(Kraj kraj in vsi)
                {
                    SortedSet<Kraj> nov = kraj.destinacije(k-1);
                    trenutni.UnionWith(nov);
                } 
                vsi.UnionWith(trenutni);
            }
            vsi.Remove(this);
            return vsi;
        }

        public int CompareTo(object obj)
        {
            Kraj novKraj = obj as Kraj;

            if(novKraj.kratica == this.kratica)
            {
                return this.name.CompareTo(novKraj.name);
            }
            return this.kratica.CompareTo(novKraj.kratica);
        }

    }
}
