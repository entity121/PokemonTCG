using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json.Linq;  // install-package Newtonsoft.Json

namespace PokemonTCG.Datenbanken
{
    class DatenbankErstellen
    {

        PokemonTCGDatenbank datenbank = new PokemonTCGDatenbank();


        // In dieser Funktion werden die verschiedenen Tabellen der Datenbank angelegt
        // Dies geschieht nur dann, wenn die Anwendung zum ersten mal ausgeführt wird
        // Durch try-catch wird überprüft, ob die Tabellen bereits existieren
        //###########################################################
        public void Tabellen_Erstellen()
        {

            SQLiteConnection verbindung = datenbank.Verbindung_Herstellen();

            // Das Objekt für SQL Befehle wird erzeugt
            SQLiteCommand sqlBefehl;

            string tabelleErstellen;

            try
            {
                // Der SQL-Befehl zum erstellen der Karten Tabelle in Form eines Strings
                tabelleErstellen = "CREATE TABLE karten ("
                                                            + "ID int(5) , Art varchar(15) , Kartenname varchar(100) , Vorentwicklung varchar(100) ,"
                                                            + "Weiterentwicklung varchar(100) , Typ varchar(20) , KP int(5) , Fähigkeit int(1) , Angriff1 varchar(100) ,"
                                                            + "Kosten1 int(3) , Energie1 varchar(20) , Farblos1 int(3) , Schaden1 int(5) , Fähigkeit1 int(1) , Angriff2 varchar(100) ,"
                                                            + "Kosten2 int(3) , Energie2 varchar(20) , Farblos2 int(3) , Schaden2 int(5) , Fähigkeit2 int(1) ,"
                                                            + "Schwäche varchar(20) , Resistenz varchar(20) , Rückzugskosten int(3) , DexNummer int(5) ,"
                                                            + "KartenNummer int(5) , Booster varchar(15) , BasisEnergie int(1)); ";
                // Dem Verbindungsobjekt, welches der Funktion übergeben wurde, wird der SQL Befehlhinzugefügt, 
                // damit dieser Befehl ausgeführt werden und eine Tabelle erzeugt werden kann
                sqlBefehl = verbindung.CreateCommand();
                sqlBefehl.CommandText = tabelleErstellen;
                sqlBefehl.ExecuteNonQuery();

                MessageBox.Show("Karten erstellt");
                Karten_Einfügen();
                MessageBox.Show("Karten befüllt");
            }
            catch (System.Data.SQLite.SQLiteException e)
            { MessageBox.Show(e.ToString()); };

            try
            {
                // Deck Tabelle
                tabelleErstellen = "CREATE TABLE decks ("
                    + "ID int(3) , Name varchar(20) , "
                    + "k1 int(3) , k2 int(3) , k3 int(3) , k4 int(3) , k5 int(3) , k6 int(3) , k7 int(3) , k8 int(3) , k9 int(3) , k10 int(3) , "
                    + "k11 int(3) , k12 int(3) , k13 int(3) , k14 int(3) , k15 int(3) , k16 int(3) , k17 int(3) , k18 int(3) , k19 int(3) , k20 int(3) , "
                    + "k21 int(3) , k22 int(3) , k23 int(3) , k24 int(3) , k25 int(3) , k26 int(3) , k27 int(3) , k28 int(3) , k29 int(3) , k30 int(3) , "
                    + "k31 int(3) , k32 int(3) , k33 int(3) , k34 int(3) , k35 int(3) , k36 int(3) , k37 int(3) , k38 int(3) , k39 int(3) , k40 int(3) , "
                    + "k41 int(3) , k42 int(3) , k43 int(3) , k44 int(3) , k45 int(3) , k46 int(3) , k47 int(3) , k48 int(3) , k49 int(3) , k50 int(3) , "
                    + "k51 int(3) , k52 int(3) , k53 int(3) , k54 int(3) , k55 int(3) , k56 int(3) , k57 int(3) , k58 int(3) , k59 int(3) , k60 int(3) ); ";

                sqlBefehl = verbindung.CreateCommand();
                sqlBefehl.CommandText = tabelleErstellen;
                sqlBefehl.ExecuteNonQuery();

                MessageBox.Show("Decks erstellt");
                Decks_Einfügen();
                MessageBox.Show("Decks befüllt");
            }
            catch (System.Data.SQLite.SQLiteException e)
            { MessageBox.Show(e.ToString()); };


            verbindung.Close();
        }
        //###########################################################



        // Die leeren Tabellen der Datenbank werden mit den Inhalten aus den Text Dokumenten gefüllt
        //###########################################################
        private void Karten_Einfügen()
        {
            SQLiteConnection verbindung = datenbank.Verbindung_Herstellen();
            SQLiteCommand sqlBefehl;

            string[] json = File.ReadAllLines(@"..\..\..\Datenbank_Karten.txt");

            for (int i = 0; i < json.Length; i++)
            {

                var eintrag = JObject.Parse(json[i]);

                string tabelleFüllen = "INSERT INTO karten (ID, Art, Kartenname, Vorentwicklung, Weiterentwicklung, Typ, KP, Fähigkeit, " +
                                            "Angriff1, Kosten1, Energie1, Farblos1, Schaden1, Fähigkeit1, Angriff2, Kosten2, Energie2, Farblos2, Schaden2, " +
                                            "Fähigkeit2, Schwäche, Resistenz, Rückzugskosten, DexNummer, KartenNummer, Booster, BasisEnergie)" +
                                            "VALUES (" + eintrag["ID"] + ",'" + eintrag["Art"] + "','" + eintrag["Kartenname"] + "','" + eintrag["Vorentwicklung"] +
                                            "','" + eintrag["Weiterentwicklung"] + "','" + eintrag["Typ"] + "'," + eintrag["KP"] + "," + eintrag["Fähigkeit"] +
                                            ",'" + eintrag["Angriff1"] + "'," + eintrag["Kosten1"] + ",'" + eintrag["Energie1"] + "'," + eintrag["Farblos1"] +
                                            "," + eintrag["Schaden1"] + "," + eintrag["Fähigkeit1"] + ",'" + eintrag["Angriff2"] + "'," + eintrag["Kosten2"] +
                                            ",'" + eintrag["Energie2"] + "'," + eintrag["Farblos2"] + "," + eintrag["Schaden2"] + "," + eintrag["Fähigkeit2"] +
                                            ",'" + eintrag["Schwäche"] + "','" + eintrag["Resistenz"] + "'," + eintrag["Rückzugskosten"] + "," + eintrag["DexNummer"] +
                                            "," + eintrag["KartenNummer"] + ",'" + eintrag["Booster"] + "'," + eintrag["BasisEnergie"] + ")";

                sqlBefehl = verbindung.CreateCommand();
                sqlBefehl.CommandText = tabelleFüllen;
                sqlBefehl.ExecuteNonQuery();
            }
        }
        //###########################################################



        //###########################################################
        private void Decks_Einfügen()
        {
            SQLiteConnection verbindung = datenbank.Verbindung_Herstellen();
            SQLiteCommand sqlBefehl;

            string[] json = File.ReadAllLines(@"..\..\..\Datenbank_Decks.txt");

            for (int i = 0; i < json.Length; i++)
            {

                var eintrag = JObject.Parse(json[i]);


                string tabelleFüllen = "INSERT INTO decks (ID, Name, k1,k2,k3,k4,k5,k6,k7,k8,k9,k10,k11,k12,k13,k14,k15,k16,k17,k18,k19,k20," +
                                                            "k21,k22,k23,k24,k25,k26,k27,k28,k29,k30,k31,k32,k33,k34,k35,k36,k37,k38,k39,k40,k41,k42,k43,k44,k45,k46,k47,k48,k49,k50," +
                                                            "k51,k52,k53,k54,k55,k56,k57,k58,k59,k60)" +
                            "VALUES (" + eintrag["ID"] + ",'" + eintrag["Name"] + "'," + eintrag["k1"] + "," + eintrag["k2"] + "," + eintrag["k3"] + "," + eintrag["k4"]
                            + "," + eintrag["k5"] + "," + eintrag["k6"] + "," + eintrag["k7"] + "," + eintrag["k8"] + "," + eintrag["k9"] + "," + eintrag["k10"] + "," + eintrag["k11"]
                            + "," + eintrag["k12"] + "," + eintrag["k13"] + "," + eintrag["k14"] + "," + eintrag["k15"] + "," + eintrag["k16"] + "," + eintrag["k17"] + "," + eintrag["k18"]
                            + "," + eintrag["k19"] + "," + eintrag["k20"] + "," + eintrag["k21"] + "," + eintrag["k22"] + "," + eintrag["k23"] + "," + eintrag["k24"] + "," + eintrag["k25"]
                            + "," + eintrag["k26"] + "," + eintrag["k27"] + "," + eintrag["k28"] + "," + eintrag["k29"] + "," + eintrag["k30"] + "," + eintrag["k31"] + "," + eintrag["k32"]
                            + "," + eintrag["k33"] + "," + eintrag["k34"] + "," + eintrag["k35"] + "," + eintrag["k36"] + "," + eintrag["k37"] + "," + eintrag["k38"] + "," + eintrag["k39"]
                            + "," + eintrag["k40"] + "," + eintrag["k41"] + "," + eintrag["k42"] + "," + eintrag["k43"] + "," + eintrag["k44"] + "," + eintrag["k45"] + "," + eintrag["k46"]
                            + "," + eintrag["k47"] + "," + eintrag["k48"] + "," + eintrag["k49"] + "," + eintrag["k50"] + "," + eintrag["k51"] + "," + eintrag["k52"] + "," + eintrag["k53"]
                            + "," + eintrag["k54"] + "," + eintrag["k55"] + "," + eintrag["k56"] + "," + eintrag["k57"] + "," + eintrag["k58"] + "," + eintrag["k59"] + "," + eintrag["k60"]
                            + ")";


                sqlBefehl = verbindung.CreateCommand();
                sqlBefehl.CommandText = tabelleFüllen;
                sqlBefehl.ExecuteNonQuery();
            }
        }
        //###########################################################



    }
}
