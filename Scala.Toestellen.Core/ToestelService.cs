using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Scala.Toestellen.Core
{
    public class ToestelService
    {
        public List<Toestel> Toestellen { get; private set; }
        public int TotaalAantalToestellen
        {
            get
            {
                int aantal = 0;
                foreach(Toestel toestel in Toestellen)
                {
                    aantal += toestel.Voorraad;
                }
                return aantal;
            }
        }
        public decimal TotaleStockWaarde
        {
            get
            {
                decimal totaal = 0m;
                foreach (Toestel toestel in Toestellen)
                {
                    totaal += toestel.Verkoopprijs;
                }
                return totaal;
            }
        }        

        public ToestelService()
        {
            Toestellen = new List<Toestel>();
            DoSeeding();
            Sort();
        }
        private void DoSeeding()
        {
            Toestellen.Add(new Toestel("Siemens", "ABC", ToestelSoort.Oven, 399.99m, 7, 2500, 220));
            Toestellen.Add(new Toestel("Siemens", "BCD", ToestelSoort.Oven, 450m, 6, 2500, 220));
            Toestellen.Add(new Toestel("Zanusi", "XU", ToestelSoort.Oven, 299.99m, 12, 2000, 230));
            Toestellen.Add(new Toestel("Zanusi", "YZ", ToestelSoort.Oven, 320m, 9, 2200, 230));
            Toestellen.Add(new Toestel("AEG", "XZ123", ToestelSoort.Koelkast, 375m, 2, 1000, 220));
            Toestellen.Add(new Toestel("AEG", "XYZ45", ToestelSoort.Koelkast, 450m, 6, 1100, 220));
            Toestellen.Add(new Toestel("Zanusi", "KK101", ToestelSoort.Koelkast, 199.99m, 12, 1200, 230));
            Toestellen.Add(new Toestel("Zanusi", "KK102", ToestelSoort.Koelkast, 220m, 9, 1200, 230));

            Toestellen.Add(new Toestel("Siemens", "VW1", ToestelSoort.Vaatwas, 599.99m, 7, 1500, 220));
            Toestellen.Add(new Toestel("Siemens", "VW2", ToestelSoort.Vaatwas, 650m, 6, 1500, 220));
            Toestellen.Add(new Toestel("Zanusi", "WMXU", ToestelSoort.Wasmachine, 499.99m, 12, 1600, 230));
            Toestellen.Add(new Toestel("Zanusi", "WMYZ", ToestelSoort.Wasmachine, 420m, 9, 1700, 230));
            Toestellen.Add(new Toestel("AEG", "DK123", ToestelSoort.Droogkast, 475m, 2, 3000, 220));
            Toestellen.Add(new Toestel("AEG", "DKZ45", ToestelSoort.Droogkast, 650m, 6, 3500, 220));
            Toestellen.Add(new Toestel("Zanusi", "DV101", ToestelSoort.Diepvries, 299.99m, 12, 900, 230));
            Toestellen.Add(new Toestel("Zanusi", "DV102", ToestelSoort.Diepvries, 470m, 9, 1100, 230));
        }
        private void Sort()
        {
            Toestellen = Toestellen.OrderBy(t => t.Merk).ThenBy(t => t.Serie).ToList();
        }
        public void ToestelToevoegen(Toestel toestel)
        {
            Toestellen.Add(toestel);
            Sort();
        }
        public void ToestelVerwijderen(Toestel toestel)
        {
            Toestellen.Remove(toestel);
        }

        public List<Toestel> FilterPerSoort(ToestelSoort toestelSoort)
        {
            List<Toestel> filter = new List<Toestel>();
            foreach(Toestel toestel in Toestellen)
            {
                if(toestel.Soort == toestelSoort)
                {
                    filter.Add(toestel);
                }
            }
            return filter;
        }

    }
}
