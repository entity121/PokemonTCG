using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Security.Cryptography;
using PokemonTCG.Datenbanken;

namespace PokemonTCG.Karten
{
    class Deck
    {


        //VARIABLEN
        //#############################
        private int I_deckGröße = 60;

        private string S_deckName;
        private int I_kartenAnzahl;
        private int[] Ai_inhalt;
        //#############################



        //KONSTRUKTOR
        //#######################################
        public Deck(string name,int[]deck){

            this.I_kartenAnzahl = I_deckGröße;
            this.S_deckName = name;
            //this.inhalt = new int[deckGröße];
            this.Ai_inhalt = deck;

        }
        //#######################################





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
            for(int durchlauf = 0; durchlauf < 1000; durchlauf++)
            {

                // Ein Array zum Zwischenspeichern der Werte
                int[] vermischt = new int[I_kartenAnzahl];


                // Zufallszahl bestimmt, an welcher Stelle das Array geteilt wird
                int schnitt;

                if (I_kartenAnzahl > 2) // mehr als 3 Karten
                {
                    schnitt = random.Next(1, I_kartenAnzahl - 1);
                }
                else if (I_kartenAnzahl == 2) // zwei Karten
                {
                    schnitt = 1;
                }
                else // eine Karte
                {
                    return;
                }
                

                // Die zweite hälfte kommt in "UMGEDREHTER" Reihenfolge an erste Stelle des Zwischenspeicher-Arrays
                for(int index = 0; index < I_kartenAnzahl-schnitt; index++)
                {
                    vermischt[index] = Ai_inhalt[I_kartenAnzahl - index - 1];
                }
                // Die erste Hälfte kommt in "NORMALER" Reihenfolge als zweites
                for(int index = 0; index < schnitt; index++)
                {
                    vermischt[(I_kartenAnzahl - schnitt) + index] = Ai_inhalt[index];
                }

                // Zum Schluss das Deck mit dem Zwischengespeicherten Array überschreiben
                for(int index = 0; index < I_kartenAnzahl; index++)
                {
                    Ai_inhalt[index] = vermischt[index];
                }

            }
            
        }
        //#################################################





        // Anhand ihrem Index im Deck wird eine Karte heraus gezogen
        //#################################################
        public Karte Karte_Ausgeben(int index)
        {
            // oberste Karte vom Stapel nehmen
            int id = Ai_inhalt[index];

            // Deckgröße anpassen
            Deck_Größe_Ändern("kleiner", index);

            PokemonTCGDatenbank datenbank = new PokemonTCGDatenbank();

            Karte karte = datenbank.Karte_Abrufen(id);

            // Karte ausgeben
            return karte;
        }
        //#################################################





        // Eine Karte wird aufs Deck zurück gelegt
        //#################################################
        public void Karte_Aufnehmen(int karte)
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
                for(int index = karte; index < I_deckGröße - 1; index++)
                {
                    Ai_inhalt[index] = Ai_inhalt[index + 1];
                }

                //... die letzte Stelle wird bekommt den Wert '-1'  (-1 = lehrer Deck Platz)
                Ai_inhalt[I_deckGröße - 1] = -1;

                // WICHTIG : Karten anzahl um 1 verringern
                I_kartenAnzahl -= 1;
            }
            // Eine Karte wird dem Deck hinzugefügt
            else if(befehl == "größer")
            {
                for(int index = I_deckGröße-1; index > 0; index--)
                {
                    Ai_inhalt[index] = Ai_inhalt[index - 1];      
                }

                Ai_inhalt[0] = karte;

                // WICHTIG : Karten anzahl um 1 erhöhen
                I_kartenAnzahl += 1;
            }
        }
        //#################################################






        // Getter
        //#################################################
        public int[] Inhalt_Ausgeben()
        {
            return Ai_inhalt;
        }
        //#################################################

        //#################################################
        public string DeckNamen_Ausgeben()
        {
            return S_deckName;
        }
        //#################################################

        //#################################################
        public int Kartenanzahl_Ausgeben()
        {
            return I_kartenAnzahl;
        }
        //#################################################
    }
}
