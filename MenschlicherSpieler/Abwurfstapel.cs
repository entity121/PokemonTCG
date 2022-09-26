using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.Karten
{
    class Abwurfstapel
    {
        private List<Karte> Lo_aburfstapel;
        private int I_slot;
        private char C_farbe;



        public Abwurfstapel(char farbe)
        {
            this.Lo_aburfstapel = new List<Karte>();
            this.I_slot = 7;
            this.C_farbe = farbe;
        }
        //
        //
        //
        //
        //
        //
        //###########################################################
        public void Karte_Aufnehmen(Karte k)
        {
            Lo_aburfstapel.Add(k);
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        public int Anzahl_Ausgeben()
        {
            return Lo_aburfstapel.Count;
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        public List<Karte> Abwurfstapel_Ausgeben()
        {
            return Lo_aburfstapel;
        }
        //###########################################################



    }
}
