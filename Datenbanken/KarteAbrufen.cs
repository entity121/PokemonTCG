using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using System.Data.SQLite;

namespace PokemonTCG.Datenbanken
{
    class KarteAbrufen
    {

        //###########################################################
        Karte karte;
        public Karte Abrufen(int id)
        {
            // Eine Verbindung zur Datenbank herstellen
            SQLiteDatenbank sqlite = new SQLiteDatenbank();
            var verbindung = sqlite.Verbindung_Herstellen();

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
                daten.GetInt32(23), daten.GetInt32(24), daten.GetString(25),daten.GetInt32(26));
            }

            // Verbindung schließen
            verbindung.Close();

            // Das Objekt an die aufrufende Stelle zurück liefern
            return karte;
        }
        //###########################################################

    }
}
