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
        public int deckGröße = 60;

        public string deckName;
        public int kartenAnzahl;
        public int[] inhalt;
        //#############################



        //Konstruktor
        //#######################################
        public Deck(){
            this.deckName = name;
            this.kartenAnzahl = deckGröße;
            //this.inhalt = new int[deckGröße];
            //this.inhalt = deck;
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
                for(int index = 0; index < kartenAnzahl-schnitt; index++)
                {
                    vermischt[index] = inhalt[kartenAnzahl - index - 1];
                }
                // Die erste Hälfte kommt in "NORMALER" Reihenfolge als zweites
                for(int index = 0; index < schnitt; index++)
                {
                    vermischt[(kartenAnzahl - schnitt) + index] = inhalt[index];
                }

                // Zum Schluss das Deck mit dem Zwischengespeicherten Array überschreiben
                for(int index = 0; index < kartenAnzahl; index++)
                {
                    inhalt[index] = vermischt[index];
                }

            }
            
        }
        //#################################################





        // Anhand ihrem Index im Deck wird eine Karte heraus gezogen
        //#################################################
        public int Karte_Ziehen(int ind)
        {
            // oberste Karte vom Stapel nehmen
            int karte = inhalt[ind];

            // Deckgröße anpassen
            Deck_Größe_Ändern("kleiner", ind);

            // Karte ausgeben
            return karte;
        }
        //#################################################





        // Eine Karte wird aufs Deck zurück gelegt
        //#################################################
        public void Karte_Zurücklegen(int karte)
        {
            Deck_Größe_Ändern("größer", karte);
        }
        //#################################################





        // Die Größe des Decks ändert sich und die "Lücken" schließen sich
        //#################################################
        private void Deck_Größe_Ändern(string befehl,int karte)
        {
            // Eine Karte wird vom Deck entfernt
            if (befehl == "kleiner")
            {
                // Jede Stelle im Deck-Array nimmt den Wert der darauf folgenden Stelle an ...
                for(int index = karte; index < deckGröße - 1; index++)
                {
                    inhalt[index] = inhalt[index + 1];
                }

                //... die letzte Stelle wird bekommt den Wert '-1'  (-1 = lehrer Deck Platz)
                inhalt[deckGröße - 1] = -1;

                // WICHTIG : Karten anzahl um 1 verringern
                kartenAnzahl -= 1;
            }
            // Eine Karte wird dem Deck hinzugefügt
            else if(befehl == "größer")
            {
                for(int index = deckGröße-1; index > 0; index--)
                {
                    inhalt[index] = inhalt[index - 1];      
                }

                inhalt[0] = karte;

                // WICHTIG : Karten anzahl um 1 erhöhen
                kartenAnzahl += 1;
            }
        }
        //#################################################






        // Das ganze Deck wird als ein Array ausgegeben
        //#################################################
        public int[] Deck_Ausgeben()
        {
            return inhalt;
        }
        //#################################################
    }
}
