using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace PokemonTCG.Spielfeld
{
    class Aktionen
    {

        private int I_x;
        private int I_y;
        private int I_w;
        private int I_h;

        private Karte karte;

        private string angriff1;
        private string[] kosten1;
        private int schaden1;

        private string angriff2;
        private string[] kosten2;
        private int schaden2;

        private string fähigkeit;

        private string[] rückzugskosten;

        //###########################################################
        public Aktionen(Karte karte, int x, int y, double scale)
        {
            this.karte = karte;

            // Position
            this.I_x = x;
            this.I_y = y;
            this.I_w = (int)(200 * scale);
            this.I_h = (int)(265 * scale);

            // Angriff1
            this.angriff1 = karte.S_angriff1;
            this.kosten1 = new string[karte.I_kosten1];

            if (karte.S_energie1 != "")
            {
                for(int i=0; i < (karte.I_kosten1 - karte.I_farblos1); i++)
                {
                    kosten1[i] = karte.S_energie1;
                }

                for(int i= (karte.I_kosten1 - karte.I_farblos1); i < karte.I_kosten1; i++)
                {
                    kosten1[i] = "Farblos";
                }

            }
            else
            {
                for(int i=0; i<karte.I_kosten1; i++)
                {
                    kosten1[i] = "Farblos";
                }
            }

            this.schaden1 = karte.I_schaden1;

            // Angriff2
            hier geht es weiter
        }
        //###########################################################





        //###########################################################
        private Rectangle Position()
        {
            return new Rectangle(I_x, I_y, I_w, I_h);
        }
        //###########################################################





        //###########################################################
        public void Draw(Texture2D brett, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(brett, Position(), Color.White);
        }
        //###########################################################
    }
}
