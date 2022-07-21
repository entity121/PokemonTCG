using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using PokemonTCG.Datenbanken;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace PokemonTCG.Spielfeld
{
    class Spieler
    {
        //VARIABLEN
        //#############################
        PokemonTCGDatenbank datenbank = new PokemonTCGDatenbank();

        private Deck O_deck;
        private Hand O_hand;
        private Kartenslot O_kartenslot;
        //.......
        //.......
        //.......
        //#############################

        //KONSTRUKTOR
        //#######################################
        public Spieler(int deck,double skalierung,SpriteBatch sprite,List<Texture2D>list,Texture2D holz,Kartenslot kartenslot)
        {
            this.O_hand = new Hand(skalierung,sprite,list,holz);
            this.O_deck = datenbank.Deck_Abrufen(deck);
            this.O_kartenslot = kartenslot;

            // Das Deck soll auf das Spielfeld gelegt werden (rein Visueller Zweck)
            Slot_Füllen(0,0);
        }
        //#######################################





        //###########################################################
        public Hand Get_Hand()
        {
            return this.O_hand;
        }
        //###########################################################




        //###########################################################
        public void Brett_Hover(Point mousePoint)
        {
            O_hand.Brett_Hover(mousePoint);
        }
        //###########################################################




        //###########################################################
        public void Karte_Ziehen(Point point)
        {
            O_kartenslot.

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
        public void Slot_Füllen(int slotID,int karteID)
        {
            O_kartenslot.Slot_Ändern(slotID, karteID, 'w');
        }
        //###########################################################




        //###########################################################
        public void DeckGröße_Prüfen()
        {
            int deckGröße = O_deck.Kartenanzahl_Ausgeben();

            if (deckGröße > 0)
            {

            }
            else
            {

            }

        }
        //###########################################################





        //###########################################################
        public void Draw_Hand()
        {
            O_hand.Draw();
        }
        //###########################################################


    }
}
