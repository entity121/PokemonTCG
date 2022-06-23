using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PokemonTCG.Datenbanken
{
    class XAMPP
    {

        // DIESE FUNKTION IST DAZU DA, DIE DATEN AUS DER XAMPP DATENBANK AUSZULESEN UND IN DIE SQLITE DATENBANK ZU SCHREIBEN
        // STANDARTMÄßIG NICHT NÖTIG ABER FÜR NOTFÄLLE DA LASSEN

        //###########################################################
        /*
        public void alte_Datenbank_Übertragen()
        {
            // Aus der ehemaligen Datenbank werden die Einträge ausgelesen
            string datenbankInformationen = System.IO.File.ReadAllText(@"..\..\..\Datenbank_Informationen(c#).txt");
            string sqlBefehl = "SELECT * FROM karten";

            MySqlConnection datenbankVerbindung = new MySqlConnection(datenbankInformationen);
            MySqlCommand befehlAusführen = new MySqlCommand(sqlBefehl, datenbankVerbindung);
            befehlAusführen.CommandTimeout = 60;
            MySqlDataReader reader;


            try
            {

                datenbankVerbindung.Open();

                reader = befehlAusführen.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                     {

                        // Der SQL Befehl um einen Karteneintrag zu tätigen
                        string sql = "INSERT INTO karten (ID, Art, Kartenname, Vorentwicklung, Weiterentwicklung, Typ, KP, Fähigkeit, " +
                            "Angriff1, Kosten1, Energie1, Farblos1, Schaden1, Fähigkeit1, Angriff2, Kosten2, Energie2, Farblos2, Schaden2, " +
                            "Fähigkeit2, Schwäche, Resistenz, Rückzugskosten, DexNummer, KartenNummer, Booster, BasisEnergie)" +
                            "VALUES (" + reader.GetString(0) + ",'" + reader.GetString(1) + "','" + reader.GetString(2) + "','" + reader.GetString(3) + 
                            "','" + reader.GetString(4) + "','" + reader.GetString(5) + "'," + reader.GetString(6) + "," + reader.GetString(7) + 
                            ",'" + reader.GetString(8) + "'," + reader.GetString(9) + ",'" + reader.GetString(10) + "'," + reader.GetString(11) + 
                            "," + reader.GetString(12) + "," + reader.GetString(13) + ",'" + reader.GetString(14) + "'," + reader.GetString(15) + 
                            ",'" + reader.GetString(16) + "'," + reader.GetString(17) + "," + reader.GetString(18) + "," + reader.GetString(19) + 
                            ",'" + reader.GetString(20) + "','" + reader.GetString(21) + "'," + reader.GetString(22) + "," + reader.GetString(23) +
                            "," + reader.GetString(24) + ",'" + reader.GetString(25) + "'," + reader.GetString(26) + ")";

                        SQLiteDatenbank s = new SQLiteDatenbank();
                        var verbindung = s.Verbindung_Herstellen();
                        s.Daten_Einfügen(verbindung, sql);

                     }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                //MessageBox.Show();

                datenbankVerbindung.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */
        //###########################################################
    }
}
