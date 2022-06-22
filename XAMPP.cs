using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace PokemonTCG
{
    class XAMPP
    {

        public void Verbindung()
        {

            string datenbankInformationen = System.IO.File.ReadAllText(@"..\..\..\Datenbank_Informationen(c#).txt");
            string sqlBefehl = "SELECT * FROM karten WHERE ID<5";

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
                        string[] row = { reader.GetString(0), reader.GetString(1), reader.GetString(2) };
                        MessageBox.Show(row[0]);
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


    




    }
}
