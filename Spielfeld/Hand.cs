using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;


namespace PokemonTCG.Spielfeld
{
    class Hand
    {

        //VARIABLEN
        //#############################
        private List<Karte> L_karten;

        //#############################


        //KONSTRUKTOR
        //#######################################
        public Hand()
        {
            this.L_karten = new List<Karte>();
        }
        //#######################################




        //###########################################################
        public void Karten_Aufnehmen(Karte k)
        {
            L_karten.Add(k);
        }
        //###########################################################




        //###########################################################
        public Karte Karte_Entfernen(int index)
        {
            Karte karte = L_karten[index];

            L_karten.RemoveAt(index);

            return karte;
        }
        //###########################################################




        //###########################################################
        public Karte[] Hand_Zeigen()
        {
            Karte[] k = new Karte[L_karten.Count];

            for (int i = 0; i < L_karten.Count; i++)
            {
                k[i] = L_karten[i];
            }

            return k;
        }
        //###########################################################






    }
}
