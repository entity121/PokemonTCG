using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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


        public void Test(SQLiteConnection c)
        {
            SQLiteDataReader reader;
            SQLiteCommand sql;
            sql = c.CreateCommand();
            sql.CommandText = "SELECT * FROM karten";

            reader = sql.ExecuteReader();

            while (reader.Read())
            {
                string erg = reader.GetString(2);
                MessageBox.Show(erg);
            }

            c.Close();
        }






        // DIE NACHFOLGENDEN FUNKTIONEN BLEIBEN AUSKOMMENTIERT, BIS SIE BENÖTIGT WERDEN UM EINE TABELLE 
        // ZU ERSTELLEN UND ZU FÜLLEN MIT EINTRÄGEN AUS DER XAMPP DATENBANK

        //###########################################################
        /*public void Tabelle_Erstellen(SQLiteConnection ver)
        {
            // Das Objekt für SQL Befehle wird erzeugt
            SQLiteCommand sqlBefehl;
            // Der SQL-Befehl zum erstellen der Tabelle in Form eines Strings
            string tabelleErstellen = "CREATE TABLE karten ("
                                                        + "ID int(1) , Art varchar(15) , Kartenname varchar(100) , Vorentwicklung varchar(100) ,"
                                                        + "Weiterentwicklung varchar(100) , Typ varchar(20) , KP int(5) , Fähigkeit int(1) , Angriff1 varchar(100) ,"
                                                        + "Kosten1 int(3) , Energie1 varchar(20) , Farblos1 int(3) , Schaden1 int(5) , Fähigkeit1 int(1) , Angriff2 varchar(100) ,"
                                                        + "Kosten2 int(3) , Energie2 varchar(20) , Farblos2 int(3) , Schaden2 int(5) , Fähigkeit2 int(1) ,"
                                                        + "Schwäche varchar(20) , Resistenz varchar(20) , Rückzugskosten int(3) , DexNummer int(5) ,"
                                                        + "KartenNummer int(5) , Booster varchar(15) , BasisEnergie int(1)); ";

            // Dem Verbindungsobjekt, welches der Funktion übergeben wurde, wird der SQL Befehlhinzugefügt, 
            // damit dieser Befehl ausgeführt werden und eine Tabelle erzeugt werden kann
            sqlBefehl = ver.CreateCommand();
            sqlBefehl.CommandText = tabelleErstellen;
            sqlBefehl.ExecuteNonQuery();

            ver.Close();
        }*/
        //###########################################################




        //###########################################################
        /*public void Daten_Einfügen(SQLiteConnection ver,string sql)
        {
            // Das Objekt für SQL Befehle wird erzeugt
            SQLiteCommand sqlBefehl;
            // Ein Befehl wird vorbereitet
            sqlBefehl = ver.CreateCommand();
            // Der SQL Befehl wird als Parameter übergeben und ausgeführt
            sqlBefehl.CommandText = sql;
            sqlBefehl.ExecuteNonQuery();
        }*/
        //###########################################################

    }
}
