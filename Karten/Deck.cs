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

        public int[] testDeck = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


       
        public int deckGröße = 10;

        private int kartenAnzahl;
        public int[] inhalt;

        //Konstruktor
        //#################################################
        public Deck(){

            this.kartenAnzahl = deckGröße;
            this.inhalt = new int[deckGröße];

        }
        //#################################################




        //#################################################
        public void Deck_Erstellen()
        {
            for (int x = 0; x < deckGröße; x++)
            {
                inhalt[x] = testDeck[x];
            }
        }
        //#################################################





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
                int[] vermischt = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                // Zufallszahl bestimmt, an welcher Stelle das Array geteilt wird
                int schnitt = random.Next(1, 9);

                // Die zweite hälfte kommt in "UMGEDREHTER" Reihenfolge an erste Stelle des Zwischenspeicher-Arrays
                for(int stelle = 0; stelle < deckGröße-schnitt; stelle++)
                {
                    vermischt[stelle] = inhalt[deckGröße - stelle - 1];
                }
                // Die erste Hälfte kommt in "NORMALER" Reihenfolge als zweites
                for(int stelle = 0; stelle < schnitt; stelle++)
                {
                    vermischt[(deckGröße - schnitt) + stelle] = inhalt[stelle];
                }

                // Zum Schluss das Deck mit dem Zwischengespeicherten Array überschreiben
                inhalt = vermischt;

            }
            
        }
        //#################################################





        //#################################################
        public int Karte_Zeigen(int x)
        {
            return inhalt[x];
        }
        //#################################################
    }
}
