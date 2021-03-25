using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Scala.Toestellen.Core
{
    public enum ToestelSoort { Vaatwas, Oven, Wasmachine, Droogkast, Koelkast, Diepvries}
    public class Toestel
    {
        private string merk;
        private string serie;
        private ToestelSoort soort;
        private decimal verkoopprijs;
        private int voorraad;
        private int watt;
        private int voltage;

        public string Merk
        {
            get { return merk; }
            set 
            {
                value = value.Trim();
                if (value == "")
                    value = "Onbekend merk";
                merk = value; 
            }
        }
        public string Serie
        {
            get { return serie; }
            set
            {
                value = value.Trim();
                if (value == "")
                    value = "Onbekende serie";
                serie = value;
            }
        }
        public ToestelSoort Soort
        {
            get { return soort; }
            set { soort = value; }
        }
        public decimal Verkoopprijs
        {
            get { return verkoopprijs; }
            set 
            {
                if (value < 0)
                    value = 0m;
                verkoopprijs = value; 
            }
        }  
        public int Voorraad
        {
            get { return voorraad; }
            set 
            {
                if (value < 0)
                    value = 0;
                voorraad = value; 
            }
        }
        public int Watt
        {
            get { return watt; }
            set 
            {
                if (value < 0)
                    value = 0;
                if (value > 5000)
                    value = 5000;
                watt = value; 
            }
        }
        public int Voltage
        {
            get { return voltage; }
            set
            {
                if (value < 110)
                    value = 110;
                if (value > 400)
                    value = 400;
                voltage = value;
            }
        }
        public double Ampere
        {
            get
            {
                return 1.0 * watt / voltage;
            }
        }

        public Toestel()
        { }
        public Toestel(string merk, string serie, ToestelSoort soort, decimal verkoopsprijs, int voorraad, int watt, int voltage)
        {
            Merk = merk;
            Serie = serie;
            Soort = soort;
            Verkoopprijs = verkoopsprijs;
            Voorraad = voorraad;
            Watt = watt;
            Voltage = voltage;
        }
        public override string ToString()
        {
            return $"{Merk} {Serie}";
        }




    }
}
