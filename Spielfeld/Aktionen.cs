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

        private Karte O_karte;

        private string S_angriff1;
        private string[] As_kosten1;
        private int I_schaden1;

        private string S_angriff2;
        private string[] S_kosten2;
        private int I_schaden2;

        private int I_fähigkeit;

        private int I_rückzugskosten;

        //###########################################################
        public Aktionen(Karte karte, int x, int y, double scale)
        {
            this.O_karte = karte;

            // Position
            this.I_x = x;
            this.I_y = y;
            this.I_w = (int)(200 * scale);
            this.I_h = (int)(265 * scale);



            // Angriff1
            this.S_angriff1 = karte.S_angriff1;
            this.As_kosten1 = new string[karte.I_kosten1];

            if (karte.S_energie1 != "")
            {
                for(int i=0; i < (karte.I_kosten1 - karte.I_farblos1); i++)
                {
                    As_kosten1[i] = karte.S_energie1;
                }

                for(int i= (karte.I_kosten1 - karte.I_farblos1); i < karte.I_kosten1; i++)
                {
                    As_kosten1[i] = "Farblos";
                }

            }
            else
            {
                for(int i=0; i<karte.I_kosten1; i++)
                {
                    As_kosten1[i] = "Farblos";
                }
            }

            this.I_schaden1 = karte.I_schaden1;



            // Angriff2
            this.S_angriff2 = karte.S_angriff2;
            this.S_kosten2 = new string[karte.I_kosten2];

            if (karte.S_energie2 != "")
            {
                for (int i = 0; i < (karte.I_kosten2 - karte.I_farblos2); i++)
                {
                    S_kosten2[i] = karte.S_energie2;
                }

                for (int i = (karte.I_kosten2 - karte.I_farblos2); i < karte.I_kosten2; i++)
                {
                    S_kosten2[i] = "Farblos";
                }

            }
            else
            {
                for (int i = 0; i < karte.I_kosten2; i++)
                {
                    S_kosten2[i] = "Farblos";
                }
            }

            this.I_schaden2 = karte.I_schaden2;


            this.I_fähigkeit = karte.I_fähigkeit;

            this.I_rückzugskosten = karte.I_rückzugskosten;
        }
        //###########################################################





        //###########################################################
        public bool Hover(Point mousePoint)
        {

            if (Position().Contains(mousePoint))
            {
                return true;
            }
            else
            {
                return false;
            }

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
           // spriteBatch.Draw(new Texture2D(GraphicsDevice, 100, 100), new Vector2(0, 0), Color.White);
        }
        //###########################################################
    }
}
