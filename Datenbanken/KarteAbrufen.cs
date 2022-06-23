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
        public Karte Abrufen()
        {
            SQLiteDatenbank sqlite = new SQLiteDatenbank();
            var verbindung = sqlite.Verbindung_Herstellen();

            SQLiteDataReader daten;
            SQLiteCommand sqlBefehl;
            sqlBefehl = verbindung.CreateCommand();
            sqlBefehl.CommandText = "SELECT * FROM karten WHERE ID=1";

            daten = sqlBefehl.ExecuteReader();

            while (daten.Read())
            {
                karte = new Karte(daten.GetInt32(0), daten.GetString(1), daten.GetString(2),
                daten.GetString(3), daten.GetString(4), daten.GetString(5), daten.GetInt32(6), daten.GetInt32(7),
                daten.GetString(8), daten.GetInt32(9), daten.GetString(10), daten.GetInt32(11), daten.GetInt32(12),
                daten.GetInt32(13), daten.GetString(14), daten.GetInt32(15), daten.GetString(16), daten.GetInt32(17),
                daten.GetInt32(18), daten.GetInt32(19), daten.GetString(20), daten.GetString(21), daten.GetInt32(22),
                daten.GetInt32(23), daten.GetInt32(24), daten.GetString(25),daten.GetInt32(26));

            }

            verbindung.Close();

            return karte;
        }
        //###########################################################











        /*
        //###########################################################
        PokemonKarte karte;
        public PokemonKarte Test(SQLiteConnection c)
        {
            SQLiteDataReader reader;
            SQLiteCommand sql;
            sql = c.CreateCommand();
            sql.CommandText = "SELECT * FROM karten WHERE ID=1";

            reader = sql.ExecuteReader();

            while (reader.Read())
            {
                karte = new PokemonKarte(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7),
                reader.GetString(8), reader.GetInt32(9), reader.GetString(10), reader.GetInt32(11), reader.GetInt32(12),
                reader.GetInt32(13), reader.GetString(14), reader.GetInt32(15), reader.GetString(16), reader.GetInt32(17),
                reader.GetInt32(18), reader.GetInt32(19), reader.GetString(20), reader.GetString(21), reader.GetInt32(22),
                reader.GetInt32(23), reader.GetInt32(24), reader.GetString(25));

            }

            c.Close();
            return karte;
        }
        //###########################################################
        */




    }
}
