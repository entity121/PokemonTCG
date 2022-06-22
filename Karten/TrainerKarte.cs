using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonTCG.Karten
{
    class TrainerKarte : Karte
    {

        public TrainerKarte(int id, string art, string nam, bool fäh, int num, string booster)
        {
            this.ID = id;
            this.kartenArt = art;
            this.kartenName = nam;
            this.fähigkeit = fäh;
            this.kartenNummer = num;
            this.booster = booster;

        }


    }
}
