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
        private Karte O_karteHalten;
        private int I_karteHaltenPosition = -1;
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
        //
        //
        //
        //
        //
        //
        //###########################################################
        public void Aktives_Pokemon_Setzen()
        {
            while(Spielzug.B_spielerAktiv == false)
            {
                Brett_Hover();


                if (O_kartenslot.Get_Kartenslot(1, 'w').B_besetzt == true)
                {
                    Spielzug.B_spielerAktiv = true;
                }
            }
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Der Spieler befindet sich mit der Maus über dem Brett
        //###########################################################
        public void Brett_Hover()
        {
            if (O_hand.Brett_Hover() == true)
            {
                Karte_Hover();
            }
            if (O_hand.B_halten == true)
            {
                Karte_Bewegen(I_karteHaltenPosition);
            }
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Der Spieler befindet sich mit der Maus überhalb einer Karte in der Hand
        //###########################################################
        public void Karte_Hover() {

            int position = O_hand.Karte_Hover();

            if (position>=0)
            {
                Karte_Bewegen(position);
            }

        }
        //###########################################################
        //
        //
        //
        //
        //
        // Eine ausgewählte Karte wird festgehalten und mit Hilfe der Maus über das Spielfeld bewegt
        //###########################################################     
        public void Karte_Bewegen(int position)
        {
            //Point mousePoint = MausPunkt.MausPoint();

            var mouseState = Mouse.GetState();

            if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                O_karteHalten = O_hand.Get_Karte_In_Hand(position);
                I_karteHaltenPosition = position;
                O_hand.Karte_Bewegen(O_hand.Get_Karte_In_Hand(position).I_ID);
                O_hand.B_halten = true;
            }
            else if (O_hand.B_halten == true)
            {
                O_hand.Karte_Bewegen(O_hand.Get_Karte_In_Hand(position).I_ID);

                if(mouseState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {

                    O_hand.B_halten = false;
                    I_karteHaltenPosition = -1;

                    // Nur setzen, wenn es eine Pokemon Karte 
                    if(O_hand.Get_Karte_In_Hand(position).S_art == "Pokémon" && O_hand.B_brettAusgefahren == false)
                    {
                        int slot = O_kartenslot.Slot_Hover(O_hand.Get_Karte_In_Hand(position), false);

                        if (slot > -1)
                        {
                            O_hand.Karte_Entfernen(O_karteHalten);

                            if(slot == 1)
                            {
                                O_kartenslot.Aktionen_Erstellen();
                            }                          
                        }
                        
                    }


                    // Trainer Karten müssen lediglich vom Brett gezogen werden um gespielt zu werden
                    else if(O_hand.Get_Karte_In_Hand(position).S_art == "Trainer" && O_hand.Brett_Hover() == false)
                    {
                        O_hand.Karte_Entfernen(O_karteHalten);
                    }


                    // Energiekarten müssen auf ein Pokemon auf dem Spielfeld gezogen werden
                    else if (O_hand.Get_Karte_In_Hand(position).S_art == "Energie")
                    {
                        int slot = O_kartenslot.Slot_Hover(O_hand.Get_Karte_In_Hand(position), true);

                        if (slot > -1)
                        {
                            O_kartenslot.Get_Karte(slot).Energie_Anlegen(O_hand.Get_Karte_In_Hand(position).S_typ);
                            O_kartenslot.Get_Karte(slot).Energie_Zeigen();
                            O_hand.Karte_Entfernen(O_karteHalten);
                            if(slot == 1)
                            {
                                O_kartenslot.Aktionen_Erstellen();
                            }                         
                        }
                    }

                }
            }

            lastState = mouseState;
            
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Es wird eine Karte gezogen, indem man mit der Maus auf das Deck am rechten Spielfeldrand klickt
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

                                // Die gezogene Karte wird 2 Sekunden am Bildschirm angezeigt
                                KartenAnzeige.Set_AnzeigeID("ziehen", k.I_ID);
                                System.Threading.Thread.Sleep(2000);
                                KartenAnzeige.Set_AnzeigeID("ziehen", 0);

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
        //
        //
        //
        //
        //
        // Es wird eine bestimmte Anzahl an KArten vom Deck entfernt und der Hánd zugefügt
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
        //
        //
        //
        //
        //
        // Eine Karte aus der Hand wird aufs Deck zurück gelegt
        //###########################################################
        public void Karte_Zurücklegen(int id)
        {

            Karte karte = O_hand.Get_Karte_In_Hand(id);

            O_hand.Karte_Entfernen(karte);

            O_deck.Karte_Aufnehmen(karte);

        }
        //###########################################################
        //
        //
        //
        //
        //
        // Die 6 Preiskarten werden vom Deck in den vorgesehenen Slot gelegt
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
        //
        //
        //
        //
        //
        // Die Starthand mit 7 KArten wird der Hand hinzugefügt (mindestens 1 Basis Pokémon)
        //###########################################################
        public void Starthand()
        {

            bool starthand = false;

            while (starthand == false)
            {
                O_deck.Deck_Mischen();

                Karte oben = O_deck.Karte_Zeigen(0);

                if (oben.S_art == "Pokémon" && oben.S_vorentwicklung == "")
                {
                    Karte_Ziehen(7);
                    starthand = true;
                }
            }

            Preiskarten();
        }
        //###########################################################
        //
        //
        //
        //
        //
        // 
        //###########################################################
        public void Draw_Hand()
        {
            O_hand.Draw(I_karteHaltenPosition);
        }
        //###########################################################
    }
}

