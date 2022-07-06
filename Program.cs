using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using PokemonTCG.Datenbanken;
using PokemonTCG.Karten;
using PokemonTCG.Spielfeld;


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
            

            try
            {
                dbe.Tabellen_Erstellen();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }


            Spieler ich = new Spieler(6);



            ich.Karte_Ziehen();

            ich.Karten_Zählen();

            ich.Karte_Ziehen();

            ich.Karten_Zählen();

            ich.Karte_Ziehen();

            ich.Karten_Zählen();

            ich.Karte_Zurücklegen(0);

            ich.Karten_Zählen();


        }
    }
}
