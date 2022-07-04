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
    class SQLiteDatenbank
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
            SQLiteDatenbank sqlite = new SQLiteDatenbank();
            var verbindung = sqlite.Verbindung_Herstellen();

            // SQL Befehl verfassen
            SQLiteCommand sqlBefehl;
            sqlBefehl = verbindung.CreateCommand();
            sqlBefehl.CommandText = "SELECT * FROM karten WHERE ID=" + id;

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







        // DIE NACHFOLGENDEN FUNKTIONEN BLEIBEN AUSKOMMENTIERT, BIS SIE BENÖTIGT WERDEN UM EINE TABELLE 
        // ZU ERSTELLEN UND ZU FÜLLEN MIT EINTRÄGEN AUS DER XAMPP DATENBANK
        
        //###########################################################
        public void Tabelle_Erstellen()
        {
            SQLiteConnection verbindung = Verbindung_Herstellen();
            // Das Objekt für SQL Befehle wird erzeugt
            SQLiteCommand sqlBefehl;
            // Der SQL-Befehl zum erstellen der Tabelle in Form eines Strings

            string tabelleErstellen = "CREATE TABLE karten ("
                                                        + "ID int(5) , Art varchar(15) , Kartenname varchar(100) , Vorentwicklung varchar(100) ,"
                                                        + "Weiterentwicklung varchar(100) , Typ varchar(20) , KP int(5) , Fähigkeit int(1) , Angriff1 varchar(100) ,"
                                                        + "Kosten1 int(3) , Energie1 varchar(20) , Farblos1 int(3) , Schaden1 int(5) , Fähigkeit1 int(1) , Angriff2 varchar(100) ,"
                                                        + "Kosten2 int(3) , Energie2 varchar(20) , Farblos2 int(3) , Schaden2 int(5) , Fähigkeit2 int(1) ,"
                                                        + "Schwäche varchar(20) , Resistenz varchar(20) , Rückzugskosten int(3) , DexNummer int(5) ,"
                                                        + "KartenNummer int(5) , Booster varchar(15) , BasisEnergie int(1)); ";

            //string tabelleErstellen = "CREATE TABLE decks ("
             //   + "ID int(1)"


            // Dem Verbindungsobjekt, welches der Funktion übergeben wurde, wird der SQL Befehlhinzugefügt, 
            // damit dieser Befehl ausgeführt werden und eine Tabelle erzeugt werden kann
            sqlBefehl = verbindung.CreateCommand();
            sqlBefehl.CommandText = tabelleErstellen;
            sqlBefehl.ExecuteNonQuery();

            verbindung.Close();
        }
        //###########################################################
        



        //###########################################################
        public void Daten_Einfügen(SQLiteConnection varbindung,string sql)
        {
            SQLiteConnection verbindung = Verbindung_Herstellen();
            // Das Objekt für SQL Befehle wird erzeugt
            SQLiteCommand sqlBefehl;
            // Ein Befehl wird vorbereitet
            sqlBefehl = varbindung.CreateCommand();
            // Der SQL Befehl wird als Parameter übergeben und ausgeführt
            sqlBefehl.CommandText = sql;
            sqlBefehl.ExecuteNonQuery();
        }
        //###########################################################

    }
}
