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





            KarteAbrufen abrufen = new KarteAbrufen();
            Karte a = abrufen.Abrufen(2);

            MessageBox.Show(a.kartenname);

        }
    }
}
