using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using PokemonTCG.Karten;

namespace PokemonTCG.Datenbanken
{
    class PokemonTCGDatenbank
    {

        // GIBT EINE VERBINDUNG ZURÜCK, DIE FÜR ALLE DATENBANK ANFRAGEN VERWENDET WIRD
        //###########################################################
        public SQLiteConnection Verbindung_Herstellen()
        {
            // Eine SQLite Verbindungsvariable wird erstellt
            SQLiteConnection verbindung;

            // Eine neue Verbindung wird mithilfe eines connection Strings vorbereitet
            verbindung = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");

            // Es wird versucht die Verbindung zu öffnen andernfalls soll eine Meldung angezeigt werden
            try{verbindung.Open();}
            catch{MessageBox.Show("Verbindung zur Datenbank konnte nicht hergestellt werden");}

            // Die Verbindung wird als "Objekt" zurück gegeben
            return verbindung;
        }
        //###########################################################




        // Eine einzelne Karte wird anhand ihrer eindeutigen ID mit allen Attributen aus der Datenbank geholt 
        //###########################################################
        Karte karte;
        public Karte Karte_Abrufen(int id)
        {
            // Eine Verbindung zur Datenbank herstellen
            SQLiteConnection verbindung = Verbindung_Herstellen();

            // SQL Befehl verfassen
            SQLiteCommand sqlBefehl;
            sqlBefehl = verbindung.CreateCommand();
            sqlBefehl.CommandText = "SELECT * FROM karten WHERE ID="+id;

            // SQL Befehl ausführen und Ergebniss empfangen
            SQLiteDataReader daten;
            daten = sqlBefehl.ExecuteReader();


            // Anhand des erhaltenen Datenbank Eintrages wird ein Karten Objekt erstellt
            while (daten.Read())
            {
                karte = new Karte(daten.GetInt32(0), daten.GetString(1), daten.GetString(2),
                daten.GetString(3), daten.GetString(4), daten.GetString(5), daten.GetInt32(6), daten.GetInt32(7),
                daten.GetString(8), daten.GetInt32(9), daten.GetString(10), daten.GetInt32(11), daten.GetInt32(12),
                daten.GetInt32(13), daten.GetString(14), daten.GetInt32(15), daten.GetString(16), daten.GetInt32(17),
                daten.GetInt32(18), daten.GetInt32(19), daten.GetString(20), daten.GetString(21), daten.GetInt32(22),
                daten.GetInt32(23), daten.GetInt32(24), daten.GetString(25), daten.GetInt32(26));
            }

            // Verbindung schließen
            verbindung.Close();

            // Das Objekt an die aufrufende Stelle zurück liefern
            return karte;
        }
        //###########################################################



        //###########################################################
        Deck deck;
        public Deck Deck_Abrufen(int id)
        {
            SQLiteConnection verbindung = Verbindung_Herstellen();
            SQLiteCommand sqlBefehl;

            int deckGröße = 60;

            sqlBefehl = verbindung.CreateCommand();
            sqlBefehl.CommandText = "SELECT * FROM decks WHERE ID=" + id;

            SQLiteDataReader daten;
            daten = sqlBefehl.ExecuteReader();




            int[] d = new int[deckGröße];


            while (daten.Read())
            {
                MessageBox.Show("OK");
                for(int i = 0; i < d.Length; i++)
                {
                    d[i] = daten.GetInt32(i + 2);
                }

                //deck = new Deck();
            }


            deck = new Deck();




            //MessageBox.Show(deck.ToString());

            verbindung.Close();

            return deck;
        }
        //###########################################################


    }
}
