using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Spielfeld;
using PokemonTCG.Karten;

namespace PokemonTCG.ComputerGegner
{
    class Gegner
    {

        Hand O_hand;
        PokemonTCG.Karten.Deck O_deck;
        Kartenslot O_kartenslot;
        Aktionen O_aktionen;

        //##########################################
        public Gegner(Kartenslot slot)
        {
            PokemonTCG.Datenbanken.PokemonTCGDatenbank db_conn = new Datenbanken.PokemonTCGDatenbank();
            this.O_deck = db_conn.Deck_Abrufen(8);
            db_conn = null;

            this.O_kartenslot = slot;
            this.O_hand = new Hand();
            this.O_aktionen = new Aktionen();

        }
        //##########################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        public void Deck_Platzieren()
        {
            if(O_deck != null)
            {
                O_kartenslot.Slot_Ändern(0, 0, 'r');
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
        public void Starthand()
        {
            bool starthand = false;
            do
            {
                O_deck.Deck_Mischen();
                Karte k = O_deck.Karte_Zeigen(0);

                if (k.S_art == "Pokémon" && k.S_vorentwicklung == "")
                {
                    Karten_Ziehen(7);
                    starthand = true;
                }
            }
            while (starthand == false);

        }
        //#####################################################################
        //
        //
        //
        //
        //
        //
        //#####################################################################
        public void Karten_Ziehen(int anzahl)
        {
            for(int i = 0; i < anzahl; i++)
            {
                O_hand.Karte_Aufnehmen(O_deck.Karte_Ausgeben(0));
            }
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
