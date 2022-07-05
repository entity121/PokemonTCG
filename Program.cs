using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using PokemonTCG.Datenbanken;
using PokemonTCG.Karten;


namespace PokemonTCG
{
    public static class Program
    {
        [STAThread]
        static void Main()
        
        {
            // using (var game = new Game1())
            //     game.Run();


            DatenbankErstellen dbe = new DatenbankErstellen();
            SQLiteDatenbank sqlite = new SQLiteDatenbank();

            //dbe.Tabellen_Erstellen();

            //dbe.Daten_Einfügen();

            string str;
            Karte karte;
            for (int i = 1; i < 6; i++)
            {
                karte = sqlite.Karte_Abrufen(i);

                str = "ID: " + karte.ID + "\nName: " + karte.kartenname + "\nPokedex: " + karte.dexNummer;

                MessageBox.Show(str);
            }



        }
    }
}
