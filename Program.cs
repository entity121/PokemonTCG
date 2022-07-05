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

            Deck deck = datenbank.Deck_Abrufen(7);

            MessageBox.Show(deck.deckName);
            


            int[] inhalt = deck.Deck_Ausgeben();

            for(int i = 0; i < inhalt.Length; i++)
            {
                Karte k = datenbank.Karte_Abrufen(inhalt[i]);

                MessageBox.Show(k.kartenname);
            }



        }
    }
}
