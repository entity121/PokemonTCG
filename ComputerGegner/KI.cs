using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Spielfeld;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.ComputerGegner
{
    class KI
    {

        Gegner O_gegner;

        //#################################################
        public KI(Kartenslot slot)
        {
            this.O_gegner = new Gegner(slot);

            Gegner_Startfunktionen();
        }
        //#################################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        private void Gegner_Startfunktionen()
        {
            O_gegner.Deck_Platzieren();
            O_gegner.Starthand();

            Der Gegner ist nun Startbereit
        }
        //#####################################################################
        //
        //
        //
        //
        //
        //



    }
}
