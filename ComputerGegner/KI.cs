﻿using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Spielfeld;
using Microsoft.Xna.Framework.Graphics;
using PokemonTCG.Karten;

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
        private void Aktives_Pokemon_Setzen()
        {

            List<Karte> basis = new List<Karte>();
            List<Karte> hand = O_gegner.O_hand.Hand_Ausgeben();

            // Alle Basis Pokemon in eine extra Liste eintragen
            for (int i = 0; i < hand.Count; i++)
            {
                if(Basis_Pokemon(hand[i]))
                {
                    basis.Add(hand[i]);
                }
            }


            // weitere Basis Pokemon auf die Bank setzen
            for(int i = 0; i < basis.Count; i++)
            {
                O_gegner.Karte_Setzen(basis[i], i + 1);
            }

        }
        //#####################################################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        private bool Basis_Pokemon(Karte k)
        {
            if (k.S_art == "Pokémon" && k.S_vorentwicklung == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //#####################################################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        private void Karte_Auf_Bank()
        {
            List<Karte> basis = new List<Karte>();
            List<Karte> hand = O_gegner.O_hand.Hand_Ausgeben();

            for(int i = 0; i < hand.Count; i++)
            {
                if (Basis_Pokemon(hand[i]))
                {
                    basis.Add(hand[i]);
                }
            }

        }
        //#####################################################################
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
            Aktives_Pokemon_Setzen();
        }
        //#####################################################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        public void Gegner_Zug()
        {
            O_gegner.Karten_Ziehen(1);

            //Karte_Auf_Bank();

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
