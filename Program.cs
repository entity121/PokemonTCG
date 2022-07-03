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


            // Spieler A
            Deck deckA = new Deck();  
            deckA.Deck_Füllen(1);
            MessageBox.Show(deckA.Deck_Zeigen());

            // Spieler B
            Deck deckB = new Deck();
            deckB.Deck_Füllen(2);
            MessageBox.Show(deckB.Deck_Zeigen());


            int karteA = deckA.Karte_Ziehen();
            MessageBox.Show("Gezogene Karte A: " + karteA);
            MessageBox.Show(deckA.Deck_Zeigen());

            int karteB = deckB.Karte_Ziehen();
            MessageBox.Show("Gezogene Karte B: " + karteB);
            MessageBox.Show(deckB.Deck_Zeigen());

            deckA.Karte_Zurücklegen(karteA);
            deckB.Karte_Zurücklegen(karteB);

            MessageBox.Show(deckA.Deck_Zeigen());
            MessageBox.Show(deckB.Deck_Zeigen());

        }
    }
}
