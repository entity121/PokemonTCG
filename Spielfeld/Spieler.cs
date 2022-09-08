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
        private Karte I_karteHalten;
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
        public void Brett_Hover()
        {
            if (O_hand.Brett_Hover() == true)
            {
                Karte_Hover();
            }
            if (O_hand.B_halten == true)
            {
                Karte_Bewegen(I_karteHalten);
            }
        }
        //###########################################################



        //###########################################################
        public void Karte_Hover() {

            if (O_hand.Karte_Hover() != null)
            {
                Karte_Bewegen(O_hand.Karte_Hover());
            }

        }
        //###########################################################




        //###########################################################     
        public void Karte_Bewegen(Karte karte)
        {
            //Point mousePoint = MausPunkt.MausPoint();

            var mouseState = Mouse.GetState();

            if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                I_karteHalten = karte;
                O_hand.Karte_Bewegen(karte.I_ID);
                O_hand.B_halten = true;
            }
            else if (O_hand.B_halten == true)
            {
                O_hand.Karte_Bewegen(karte.I_ID);

                if(mouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {

                    O_hand.B_halten = false;


                    // Nur setzen, wenn es eine Pokemon Karte 
                    if(karte.S_art == "Pokémon")
                    {

                        if (O_kartenslot.Slot_Hover(karte, false) > -1)
                        {
                            O_hand.Karte_Entfernen(I_karteHalten);
                        }
                        
                    }


                    // Trainer Karten müssen lediglich vom Brett gezogen werden um gespielt zu werden
                    else if(karte.S_art == "Trainer" && O_hand.Brett_Hover() == false)
                    {
                        O_hand.Karte_Entfernen(I_karteHalten);
                    }


                    // Energiekarten müssen auf ein Pokemon auf dem Spielfeld gezogen werden
                    else if (karte.S_art == "Energie")
                    {
                        int slot = O_kartenslot.Slot_Hover(karte, true);

                        if (slot > -1)
                        {
                            O_kartenslot.Get_Karte(slot).Energie_Anlegen(karte.S_typ);
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
        public void Karte_Ziehen()
        {
            // Wenn es der Spielzug zulässt
            Point mousePoint = MausPunkt.MausPoint();

                if (O_deck.Kartenanzahl_Ausgeben() > 0)
                {
                    Rectangle R_slot = O_kartenslot.Get_Kartenslot(0, 'w').Slot_Position();

                    if (R_slot.Contains(mousePoint))
                    {
                        var mouseState = Mouse.GetState();
                        if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                        {
                            if(Spielzug.B_ziehen == true)
                            {
                                Karte k = O_deck.Karte_Ausgeben(0);
                                O_hand.Karten_Aufnehmen(k);
                                Spielzug.B_ziehen = false;
                            }
                            else
                            {
                                Textbox.Infobox("Es wurde in diesem Zug bereits eine Karte gezogen");
                            }

                        }
                        lastState = mouseState;
                    }                
           
                }

                if (O_deck.Kartenanzahl_Ausgeben() == 0)
                {
                    O_kartenslot.Slot_Ändern(0, 264, 'w');
                }
                        
        }
        //###########################################################





        //###########################################################
        public void Karte_Ziehen(int anzahl)
        {
            for(int i = 0; i < anzahl; i++)
            {
                Karte k = O_deck.Karte_Ausgeben(0);
                O_hand.Karten_Aufnehmen(k);
            }

        }
        //###########################################################





        //###########################################################
        public void Karte_Zurücklegen(int id)
        {

            Karte karte = O_hand.Get_Karte_In_Hand(id);

            O_hand.Karte_Entfernen(karte);

            O_deck.Karte_Aufnehmen(karte);

        }
        //###########################################################





        //###########################################################
        public void Preiskarten()
        {

            for(int i = 8; i <= 13; i++)
            {
                Karte karte = O_deck.Karte_Ausgeben(0);

                O_kartenslot.Slot_Ändern(i, karte, 'w');
            }


        }
        //###########################################################






        //###########################################################
        public void Starthand()
        {

            bool starthand = false;
            int anzahl;

            while (starthand == false)
            {
                anzahl = 0;

                Karte_Ziehen(7);

                Karte[] arr = O_hand.Hand_Zeigen();

                for (int i = 0; i < arr.Length; i++)
                {
                    if (O_hand.Basis_Pokemon(i))
                    {
                        anzahl += 1;
                    }
                }

                if (anzahl > 0)
                {
                    starthand = true;
                }
                else
                {
                    O_hand.Hand_Abwerfen();
                }
            }
            Preiskarten();

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


/* UNGENUTZTE FUNKTIONEN
 
        //###########################################################
        public Hand Get_Hand()
        {
            return this.O_hand;
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
     
*/
