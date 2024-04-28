using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacie
{
    public class Medicament
    {
        public enum Reteta
        { Fara,Cu}
        [Flags]
        public enum Varsta
        { Adulti=1,  Copii4_9=2, Copii10_17=4, Copii0_3 = 8, Batrani = 16 }

        public Reteta reteta;
        public Varsta varsta;

        public string nume { get; set; }
        public string data_expirare {  get; set; }
        public double pret { get; set; }
        public int cantitate { get; set; }

        public Medicament(string nume, string data_expirare, double pret, int cantitate,Reteta reteta, Varsta varsta )
        {
            this.nume = nume;
            this.data_expirare = data_expirare;
            this.pret = pret;
            this.cantitate = cantitate;
            this.reteta = reteta;
            this.varsta= varsta;
        }
    }
}
