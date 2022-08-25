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
        private Karte[] Ao_inhalt;
        private int I_slotID;
        //#############################



        //KONSTRUKTOR
        //#######################################
        public Deck(string name,Karte[]deck){

            this.I_kartenAnzahl = I_deckGröße;
            this.S_deckName = name;
            //this.inhalt = new int[deckGröße];
            this.Ao_inhalt = deck;
            this.I_slotID = 12;
            this.Deck_Mischen();
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
                Karte[] vermischt = new Karte[I_kartenAnzahl];


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
                    vermischt[index] = Ao_inhalt[I_kartenAnzahl - index - 1];
                }
                // Die erste Hälfte kommt in "NORMALER" Reihenfolge als zweites
                for(int index = 0; index < schnitt; index++)
                {
                    vermischt[(I_kartenAnzahl - schnitt) + index] = Ao_inhalt[index];
                }

                // Zum Schluss das Deck mit dem Zwischengespeicherten Array überschreiben
                for(int index = 0; index < I_kartenAnzahl; index++)
                {
                    Ao_inhalt[index] = vermischt[index];
                }

            }
            
        }
        //#################################################





        // Anhand ihrem Index im Deck wird eine Karte heraus gezogen
        //#################################################
        public Karte Karte_Ausgeben(int index)
        {
            // oberste Karte vom Stapel nehmen
            Karte karte = Ao_inhalt[index];

            // Deckgröße anpassen
            Deck_Größe_Ändern("kleiner", index,null);

            // Karte ausgeben
            return karte;
        }
        //#################################################





        // Eine Karte wird aufs Deck zurück gelegt
        //#################################################
        public void Karte_Aufnehmen(Karte karte)
        {
            Deck_Größe_Ändern("größer",0,karte);
        }
        //#################################################





        // Die Größe des Decks ändert sich und die "Lücken" schließen sich
        //#################################################
        private void Deck_Größe_Ändern(string befehl,int deckStelle,Karte karte)
        {
            // Eine Karte wird vom Deck entfernt
            if (befehl == "kleiner")
            {
                // Jede Stelle im Deck-Array nimmt den Wert der darauf folgenden Stelle an ...
                for(int index = deckStelle; index < I_deckGröße - 1; index++)
                {
                    Ao_inhalt[index] = Ao_inhalt[index + 1];
                }

                //... die letzte Stelle wird bekommt den Wert '-1'  (-1 = lehrer Deck Platz)
                Ao_inhalt[I_deckGröße - 1] = null;

                // WICHTIG : Karten anzahl um 1 verringern
                I_kartenAnzahl -= 1;
            }
            // Eine Karte wird dem Deck hinzugefügt
            else if(befehl == "größer")
            {
                for(int index = I_deckGröße-1; index > 0; index--)
                {
                    Ao_inhalt[index] = Ao_inhalt[index - 1];      
                }

                Ao_inhalt[0] = karte;

                // WICHTIG : Karten anzahl um 1 erhöhen
                I_kartenAnzahl += 1;
            }
        }
        //#################################################



        // Die Anzahl der Karten, die noch im Deck sind, wird ausgegeben 
        //#################################################
        public int Kartenanzahl_Ausgeben()
        {
            return I_kartenAnzahl;
        }
        //#################################################
    }
}



/*
 
        //#################################################
        public void Deck_Visualisieren()
        {
            
            
        }
        //#################################################



        // Getter
        //#################################################
        public Karte[] Inhalt_Ausgeben()
        {
            return Ao_inhalt;
        }
        //#################################################

        //#################################################
        public string DeckNamen_Ausgeben()
        {
            return S_deckName;
        }
        //#################################################
*/
