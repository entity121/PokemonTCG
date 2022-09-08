using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace PokemonTCG
{
    public static class Textbox
    {

        public static Texture2D T2D_textbox;

        public static bool B_textbox;
        public static string S_text;

        public static int I_x = 500;
        public static int I_y = 500;
        public static int I_w = 600;
        public static int I_h = 300;


        //###########################################################
        public static bool Auswahlbox(string text)
        {
            DialogResult res = MessageBox.Show(text, "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(res == DialogResult.Yes)
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
        public static void Infobox(string text)
        {
            S_text = text;
            B_textbox = true;
        }
        //###########################################################





        //###########################################################
        public static void Draw(SpriteBatch spritebatch, Texture2D brett, SpriteFont font, double scale)
        {
            spritebatch.Draw(brett, new Rectangle(I_x, I_y, I_w, I_h), Color.White);
            spritebatch.DrawString(font,S_text,new Vector2(I_x+20, I_y+20),Color.Black,0,new Vector2(0,0),(float)(1.4*scale),SpriteEffects.None,0);

        }
        //###########################################################

    }
}
