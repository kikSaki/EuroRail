using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroRail
{
    public class EkspresniVlak : Vlak
    {
        private int hitrost = 110;
        private double cena = 0.154;

        private double doplacilo;

        public EkspresniVlak(string id, Kraj start, Kraj end, int časVožnje, double doplacilo) : base(id, start, end, časVožnje)
        {
            this.doplacilo = doplacilo;
        }



        public override string opis()
        {
            return "ekspresni";
        }

        public override double cenaVoznje()
        {
            return this.hitrost * (this.časVožnje / 60.0) * this.cena + this.doplacilo;
        }

    }
}
