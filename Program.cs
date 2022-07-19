using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using PokemonTCG.Datenbanken;
using PokemonTCG.Karten;
using PokemonTCG.Spielfeld;
using System.Windows;


namespace PokemonTCG
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {

            /* Datenabank erzeugen 
            DatenbankErstellen dbe = new DatenbankErstellen();
            try{dbe.Tabellen_Erstellen();}
            catch (Exception e){MessageBox.Show(e.ToString());}
            */

            using (var game = new Game1())
              game.Run();


        }

    }
}
