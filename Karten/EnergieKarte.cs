using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonTCG.Karten
{
    class EnergieKarte : Karte
    {

        public string typ;
        public bool basisEnergie;

        public EnergieKarte(int id,string art,bool fäh,int num,string booster,string typ, bool bas)
        {
            this.ID = id;
            this.kartenArt = art;
            this.fähigkeit = fäh;
            this.kartenNummer = num;
            this.booster = booster;
            this.typ = typ;
            this.basisEnergie = bas;
        }


    }
}
