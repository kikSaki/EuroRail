using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroRail
{
    public abstract class Vlak : IComparable
    {
        public String id { get; set; }
        public Kraj start { get; set; }
        public Kraj end { get; set; }
        public int časVožnje { get; set; }

        public Vlak(string id, Kraj start, Kraj end, int časVožnje)
        {
            this.id = id;
            this.start = start;
            this.end = end;
            this.časVožnje = časVožnje;
        }
        public abstract String opis();
        public abstract double cenaVoznje();

        private double pretvoriCas(int cas)
        {
            double vrni = Math.Floor((double)cas / 60) + (double)(cas % 60)/100.0;
            return vrni;
        }

        public override string ToString()
        {
            if(this.časVožnje > 60)
            {
                return $"Vlak {this.id} ({this.opis()}) {this.start.ToString()} -- {this.end.ToString()} ({String.Format("{0:N2}", pretvoriCas(this.časVožnje))}h, {Math.Round((Decimal)this.cenaVoznje(), 2)} EUR)";
            }
            else
            {
                return $"Vlak {this.id} ({this.opis()}) {this.start.ToString()} -- {this.end.ToString()} ({this.časVožnje} min, {Math.Round((Decimal)this.cenaVoznje(), 2)} EUR)";
            }
        }

        public int CompareTo(object obj)
        {
            Vlak vlak = obj as Vlak;

            return vlak.cenaVoznje().CompareTo(this.cenaVoznje());
        }

    }
}
