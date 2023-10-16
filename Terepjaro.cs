using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorLeventeTz
{
    class Terepjaro
    {
        public string MarkaModell { get; set; }
        public int Evjarat { get; set; }
        public string Uzemanyag { get; set; }
        public int Tomeg { get; set; }
        public string Hajtas { get; set; }
        public string Kepesseg { get; set; }
        public double TomegFont { get; set; }

        public Terepjaro(string sor)
        {
            var darab = sor.Split(';');
            this.MarkaModell = darab[0];
            this.Evjarat = int.Parse(darab[1]);
            this.Uzemanyag = darab[2];
            this.Tomeg = int.Parse(darab[3]);
            this.Hajtas = darab[4];
            this.Kepesseg = darab[5];
            this.TomegFont = double.Parse(darab[3]) * 2.20462;
        }

        public override string ToString()
        {
            return string.Format($"{MarkaModell};\n{Evjarat};\n{Uzemanyag};\n{Tomeg}Kg;\n{Hajtas};\n{Kepesseg}\n");
        }
    }
}
