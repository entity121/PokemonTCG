using System;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace PokemonTCG
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // using (var game = new Game1())
            //     game.Run();


            XAMPP x = new XAMPP();
            x.Verbindung();

        }
    }
}
