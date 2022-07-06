using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using PokemonTCG.Datenbanken;
using System.Windows.Forms;

namespace PokemonTCG.Spielfeld
{
    class Spieler
    {
        //VARIABLEN
        //#############################
        PokemonTCGDatenbank datenbank = new PokemonTCGDatenbank();

        private Deck deck;
        private Hand hand;
        //.......
        //.......
        //.......
        //#############################

        //KONSTRUKTOR
        //#######################################
        public Spieler(int deck)
        {
            this.hand = new Hand();
            this.deck = datenbank.Deck_Abrufen(deck);
        }
        //#######################################





        //###########################################################
        public void Karte_Ziehen()
        {
            Karte k = deck.Karte_Ausgeben(0);

            hand.Karten_Aufnehmen(k);
        }
        //###########################################################




        //###########################################################
        public void Karte_Zurücklegen(int index)
        {
            Karte karte = hand.Karte_Entfernen(index);

            int id = karte.ID;

            karte = null;

            deck.Karte_Aufnehmen(id);
            
        }
        //###########################################################




        //###########################################################
        public void Karten_Zählen()
        {
            string[] z = hand.Hand_Zeigen();
            string erg = "";



            // HIER WIRD WEITER GEARBEITET, DESHALB FEHLER
            stelle


            for(int i = 0; i < z.Length; i++)
            {
                erg += z[i]+"--";
            }

            MessageBox.Show(erg);
        }
        //###########################################################




    }
}
