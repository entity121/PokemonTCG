using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Security.Cryptography;

namespace PokemonTCG.Karten
{
    class Deck
    {

        // !****! Schnittstelle für die gespeicherten Decks
        public int[] deck_A = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public int[] deck_B = { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


        // Variablen
        //#############################
        private int deckGröße = 10;

        private int kartenAnzahl;
        private int[] inhalt;
        //#############################



        //Konstruktor
        //#######################################
        public Deck(){
            this.kartenAnzahl = deckGröße;
            this.inhalt = new int[deckGröße];
        }
        //#######################################





        // Das Deck eines Spielers wird mit den Karten ID's eines gespeicherten Decks gefüllt
        //#################################################
        public void Deck_Füllen(int deckID)
        {
            int[] deck = new int[60];

            switch (deckID)
            {
                case 1: { deck = deck_A; }break;
                case 2: { deck = deck_B; }break;
            }

            for (int x = 0; x < deckGröße; x++)
            {
                inhalt[x] = deck[x];
            }
        }
        //#################################################



        // Das Deck eines Spielers wird gemischt
        //#################################################
        public void Deck_Mischen()
        {

            // STANDARDTFUNKTION UM ARRAYS ZU SORTIEREN

            //Random random = new Random();
            //inhalt = inhalt.OrderBy(x => random.Next()).ToArray();

            
            // Instanz für Random zahlen
            Random random = new Random();

            // 100 mal für bestes Mischergebnis
            for(int durchlauf = 0; durchlauf < 100; durchlauf++)
            {

                // Ein Array zum Zwischenspeichern der Werte
                int[] vermischt = new int[kartenAnzahl];


                // Zufallszahl bestimmt, an welcher Stelle das Array geteilt wird
                int schnitt;

                if (kartenAnzahl > 2) // mehr als 3 Karten
                {
                    schnitt = random.Next(1, kartenAnzahl - 1);
                }
                else if (kartenAnzahl == 2) // zwei Karten
                {
                    schnitt = 1;
                }
                else // eine Karte
                {
                    return;
                }
                

                // Die zweite hälfte kommt in "UMGEDREHTER" Reihenfolge an erste Stelle des Zwischenspeicher-Arrays
                for(int stelle = 0; stelle < kartenAnzahl-schnitt; stelle++)
                {
                    vermischt[stelle] = inhalt[kartenAnzahl - stelle - 1];
                }
                // Die erste Hälfte kommt in "NORMALER" Reihenfolge als zweites
                for(int stelle = 0; stelle < schnitt; stelle++)
                {
                    vermischt[(kartenAnzahl - schnitt) + stelle] = inhalt[stelle];
                }

                // Zum Schluss das Deck mit dem Zwischengespeicherten Array überschreiben
                for(int stelle = 0; stelle < kartenAnzahl; stelle++)
                {
                    inhalt[stelle] = vermischt[stelle];
                }

            }
            
        }
        //#################################################



        //#################################################
        public int Karte_Ziehen()
        {
            // oberste Karte vom Stapel nehmen
            int obersteKarte = inhalt[0];

            // Deckgröße anpassen
            Deck_Größe_Ändern("kleiner", -1);

            // Karte ausgeben
            return obersteKarte;
        }
        //#################################################



        //#################################################
        public void Karte_Zurücklegen(int karte)
        {
            Deck_Größe_Ändern("größer", karte);
        }
        //#################################################



        //#################################################
        private void Deck_Größe_Ändern(string befehl,int karte)
        {
            // Eine Karte wird vom Deck entfernt
            if (befehl == "kleiner")
            {
                // Jede Stelle im Deck-Array nimmt den Wert der darauf folgenden Stelle an ...
                for(int stelle = 0; stelle < deckGröße - 1; stelle++)
                {
                    inhalt[stelle] = inhalt[stelle + 1];
                }

                //... die letzte Stelle wird bekommt den Wert '-1'  (-1 = lehrer Deck Platz)
                inhalt[deckGröße - 1] = -1;

                // WICHTIG : Karten anzahl um 1 verringern
                kartenAnzahl -= 1;
            }
            // Eine Karte wird dem Deck hinzugefügt
            else if(befehl == "größer")
            {
                for(int stelle = 0; stelle < deckGröße; stelle++)
                {
                    if (inhalt[stelle] == -1)
                    {
                        inhalt[stelle] = karte;
                        // WICHTIG : Karten anzahl um 1 erhöhen
                        kartenAnzahl += 1;

                        Deck_Mischen();
                        return;
                    }
                }
            }

        }
        //#################################################




        //#################################################
        public string Deck_Zeigen()
        {
            string str = "";

            for(int i = 0; i < deckGröße; i++)
            {
                str += inhalt[i] + "|_|";
            }

            return str;
        }
        //#################################################
    }
}
