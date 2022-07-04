using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using PokemonTCG.Datenbanken;
using PokemonTCG.Karten;
using Newtonsoft.Json.Linq;  // install-package Newtonsoft.Json

namespace PokemonTCG
{
    public static class Program
    {
        [STAThread]
        static void Main()
        
        {
            // using (var game = new Game1())
            //     game.Run();


            string[] json = File.ReadAllLines(@"..\..\..\Datenbank_Inhalt.txt");

            var erg = JObject.Parse(json[0]);

            MessageBox.Show(erg["Kartenname"].ToString());
        }
    }
}
