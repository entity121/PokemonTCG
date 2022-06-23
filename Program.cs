using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using PokemonTCG.Datenbanken;

namespace PokemonTCG
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // using (var game = new Game1())
            //     game.Run();


            //XAMPP x = new XAMPP();
            //x.alte_Datenbank_Übertragen();

            //SQLiteDatenbank sql = new SQLiteDatenbank();
            //var x = sql.Verbindung_Herstellen();
            //sql.Tabelle_Erstellen(x);

            SQLiteDatenbank s = new SQLiteDatenbank();
            var v = s.Verbindung_Herstellen();
            s.Test(v);

        }
    }
}
