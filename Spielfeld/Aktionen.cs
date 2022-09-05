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

        //###########################################################
        public Aktionen(Karte karte, int x, int y, double scale)
        {
            this.karte = karte;

            this.I_x = x;
            this.I_y = y;
            this.I_w = (int)(200 * scale);
            this.I_h = (int)(265 * scale);

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
