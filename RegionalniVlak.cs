using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroRail
{
    public class RegionalniVlak : Vlak
    {
        private int hitrost = 50;
        private double cena = 0.068;

        public RegionalniVlak(string id, Kraj start, Kraj end, int časVožnje) : base(id, start, end, časVožnje) { }

        public override String opis()
        {
            return "regionalni";
        }

        public override double cenaVoznje()
        {
            return this.časVožnje / 60.0 * this.hitrost * this.cena;
        }
    }
}
