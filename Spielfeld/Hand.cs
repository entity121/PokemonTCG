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
        private List<Karte> karten;
        //#############################


        //KONSTRUKTOR
        //#######################################
        public Hand()
        {
            this.karten = new List<Karte>();
        }
        //#######################################




        //###########################################################
        public void Karten_Aufnehmen(Karte k)
        {
            karten.Add(k);
        }
        //###########################################################



        //###########################################################
        public Karte Karte_Entfernen(int index)
        {
            Karte karte = karten[index];

            karten.RemoveAt(index);

            return karte;
        }
        //###########################################################




        //###########################################################
        public string[] Hand_Zeigen()
        {
            string[] k = new string[karten.Count];

            for (int i = 0; i < karten.Count; i++)
            {
                k[i] = karten[i].kartenname;
            }

            return k;

        }
        //###########################################################






    }
}
