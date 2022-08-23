using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using PokemonTCG.Datenbanken;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


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
        private MouseState lastState;
        private bool B_halten = false;
        private int I_karteHalten;
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
            O_kartenslot.Slot_Ändern(0, 0, 'w');
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
            if (O_hand.Brett_Hover(mousePoint) == true)
            {
                Karte_Hover(mousePoint);
            }
            if (B_halten == true)
            {
                Karte_Bewegen(I_karteHalten,mousePoint);
            }
        }
        //###########################################################



        //###########################################################
        public void Karte_Hover(Point mousePoint) {

            if (O_hand.Karte_Hover(mousePoint) != -1)
            {
                Karte_Bewegen(O_hand.Karte_Hover(mousePoint), mousePoint);
            }

        }
        //###########################################################




        //###########################################################     
        public void Karte_Bewegen(int karte,Point mousePoint)
        {

            var mouseState = Mouse.GetState();

            if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                I_karteHalten = karte;
                O_hand.Karte_Bewegen(O_hand.Get_Karte_In_Hand(karte).I_ID, mousePoint);
                B_halten = true;
            }
            else if (B_halten == true)
            {
                O_hand.Karte_Bewegen(O_hand.Get_Karte_In_Hand(karte).I_ID, mousePoint);

                if(mouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {

                    B_halten = false;


                    // Nur setzen, wenn es eine Pokemon Karte ist und ein Basis Pokemon 
                    if(O_hand.Get_Karte_In_Hand(I_karteHalten).S_art == "Pokémon" && O_hand.Get_Karte_In_Hand(I_karteHalten).S_vorentwicklung == "")
                    {

                        if (O_kartenslot.Slot_Hover(mousePoint, O_hand.Get_Karte_In_Hand(karte), false) > -1)
                        {
                            O_hand.Karte_Entfernen(I_karteHalten);
                        }
                        
                    }


                    // Trainer Karten müssen lediglich vom Brett gezogen werden um gespielt zu werden
                    else if(O_hand.Get_Karte_In_Hand(I_karteHalten).S_art == "Trainer" && O_hand.Brett_Hover(mousePoint) == false)
                    {
                        O_hand.Karte_Entfernen(I_karteHalten);
                    }


                    // Energiekarten müssen auf ein Pokemon auf dem Spielfeld gezogen werden
                    else if (O_hand.Get_Karte_In_Hand(I_karteHalten).S_art == "Energie")
                    {
                        int slot = O_kartenslot.Slot_Hover(mousePoint, O_hand.Get_Karte_In_Hand(karte), true);

                        if (slot > -1)
                        {
                            O_kartenslot.Get_Karte(slot).Energie_Anlegen(O_hand.Get_Karte_In_Hand(I_karteHalten).S_typ);
                            O_kartenslot.Get_Karte(slot).Energie_Zeigen();
                            O_hand.Karte_Entfernen(I_karteHalten);
                        }
                    }

                }
            }

            lastState = mouseState;
            
        }
        //###########################################################




        
        //###########################################################
        public void Karte_Ziehen(Point point)
        {
            Rectangle R_slot = O_kartenslot.Get_Kartenslot(0, 'w').Slot_Position();     

            if (R_slot.Contains(point))
            {
                var mouseState = Mouse.GetState();
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    Karte k = O_deck.Karte_Ausgeben(0);
                    O_hand.Karten_Aufnehmen(k);
                }
                lastState = mouseState;
            }

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
        public void Slot_Füllen(int slotID,Karte karte)
        {
            O_kartenslot.Slot_Ändern(slotID, karte, 'w');
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
