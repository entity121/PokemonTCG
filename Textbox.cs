using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace PokemonTCG
{
    public static class Textbox
    {

        public static string S_text;

        public static Texture2D T2D_textbox;

        public static int I_x;
        public static int I_y;
        public static int I_w;
        public static int I_h;


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
            MessageBox.Show(text, "");
        }
        //###########################################################

    }
}
