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
            PokemonTCGDatenbank datenbank = new PokemonTCGDatenbank();


            dbe.Tabellen_Erstellen();


            Karte karte = datenbank.Karte_Abrufen(6);

            MessageBox.Show(karte.kartenname);

            Deck deck = datenbank.Deck_Abrufen(6);

            MessageBox.Show(deck.kartenAnzahl.ToString());
            

            //MessageBox.Show(deck.ToString());

            /*int[] inhalt = deck.Deck_Ausgeben();

            for(int i = 0; i < inhalt.Length; i++)
            {
                MessageBox.Show(inhalt[i].ToString());
            }*/




        }
    }
}
