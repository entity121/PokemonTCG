using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;

namespace PokemonTCG.ComputerGegner
{
    class Hand
    {

        private List<Karte> Lo_karten;

        //#######################################
        public Hand()
        {
            this.Lo_karten = new List<Karte>();
        }
        //#######################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        public void Karte_Aufnehmen(Karte k)
        {
            Lo_karten.Add(k);
        }
        //#####################################################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        public List<Karte> Hand_Ausgeben()
        {
            return Lo_karten;
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
