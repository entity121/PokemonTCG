﻿using System;
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

        private Deck O_deck;
        private Hand O_hand;
        //.......
        //.......
        //.......
        //#############################

        //KONSTRUKTOR
        //#######################################
        public Spieler(int deck,double skalierung)
        {
            this.O_hand = new Hand(skalierung);
            this.O_deck = datenbank.Deck_Abrufen(deck);
        }
        //#######################################





        //###########################################################
        public Hand Get_Hand()
        {
            return this.O_hand;
        }
        //###########################################################









        //###########################################################
        public void Karte_Ziehen()
        {
            Karte k = O_deck.Karte_Ausgeben(0);

            O_hand.Karten_Aufnehmen(k);
        }
        //###########################################################




        //###########################################################
        public void Karte_Zurücklegen(int index)
        {
            Karte karte = O_hand.Karte_Entfernen(index);

            O_deck.Karte_Aufnehmen(karte);
      
        }
        //###########################################################




        //###########################################################
        public void Hand_Anschauen()
        {
            Karte[] k = O_hand.Hand_Zeigen();

        }
        //###########################################################




    }
}
