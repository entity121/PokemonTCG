using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;
using System.Threading;

namespace PokemonTCG
{
    public static class Textbox
    {

        public static Texture2D T2D_textbox;

        public static bool B_textbox;
        private static bool B_frage;
        public static string S_text;

        public static int I_x = (1920/2)-300;
        public static int I_y = (1080/2)-150;
        public static int I_w = 600;
        public static int I_h = 300;

        public static int I_xJ = ((1920 / 2) - 300) + 100;
        public static int I_yJ = ((1080 / 2) - 150) + 200;
        public static int I_wJ = 100;
        public static int I_hJ = 60;

        public static int I_xN = ((1920 / 2) - 300) + 300;
        public static int I_yN = ((1080 / 2) - 150) + 200;
        public static int I_wN=100;
        public static int I_hN=60;

        private static double D_scale = 0;

        private static bool B_jHover = false;
        private static bool B_nHover = false;

        private static MouseState lastState = Mouse.GetState();

        


        //###########################################################
        public static bool Auswahlbox(string text)
        {
            S_text = text;
            B_textbox = true;
            B_frage = true;
            bool B_beantwortet = false;
            bool B_ergebnis = true;

            while (B_beantwortet == false)
            {
                Point mousePoint = MausPunkt.MausPoint();

                if(new Rectangle((int)(I_xJ * D_scale), (int)(I_yJ * D_scale), (int)(I_wJ * D_scale), (int)(I_hJ * D_scale)).Contains(mousePoint))
                {
                    B_jHover = true;

                    if(Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {

                        B_textbox = false;
                        B_ergebnis = true;
                        B_beantwortet = true;

                    }


                }
                else
                {
                    B_jHover = false;

                }

                if(new Rectangle((int)(I_xN * D_scale), (int)(I_yN * D_scale), (int)(I_wN * D_scale), (int)(I_hN * D_scale)).Contains(mousePoint))
                {
                    B_nHover = true;

                    if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {

                        B_textbox = false;
                        B_ergebnis = false;
                        B_beantwortet = true;

                    }

                }
                else
                {
                    B_nHover = false;
                }

                lastState = Mouse.GetState();
            
            }

            return B_ergebnis;

        }
        //###########################################################





        //###########################################################
        public static void Infobox(string text)
        {
            S_text = text;
            B_textbox = true;
            B_frage = false;

            Thread.Sleep(2000);

            S_text = "";
            B_textbox = false;
        }
        //###########################################################





        //###########################################################
        public static void Draw(SpriteBatch spritebatch, Texture2D brett, SpriteFont font, double scale, Texture2D[] confirm)
        {

            if (D_scale == 0)
            {
                D_scale = scale;
            }

            if(B_frage == true)
            {
                spritebatch.Draw(brett, new Rectangle((int)(I_x*scale), (int)(I_y*scale), (int)(I_w*scale), (int)(I_h*scale)), Color.White);

                spritebatch.DrawString(font, S_text, new Vector2((int)((I_x + 20)*scale), I_y + (I_h / 2)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * scale), SpriteEffects.None, 0);


                if(B_jHover == true)
                {
                    spritebatch.Draw(confirm[1], new Rectangle((int)(I_xJ * scale), (int)(I_yJ * scale), (int)(I_wJ * scale), (int)(I_hJ * scale)), Color.White);
                }
                else
                {
                    spritebatch.Draw(confirm[0], new Rectangle((int)(I_xJ * scale), (int)(I_yJ * scale), (int)(I_wJ * scale), (int)(I_hJ * scale)), Color.White);
                }

                
                if(B_nHover == true)
                {
                    spritebatch.Draw(confirm[3], new Rectangle((int)(I_xN * scale), (int)(I_yN * scale), (int)(I_wN * scale), (int)(I_hN * scale)), Color.White);
                }
                else
                {
                    spritebatch.Draw(confirm[2], new Rectangle((int)(I_xN * scale), (int)(I_yN * scale), (int)(I_wN * scale), (int)(I_hN * scale)), Color.White);
                }


                
            }
            else
            {
                spritebatch.Draw(brett, new Rectangle((int)(I_x * scale), (int)(I_y * scale), (int)(I_w * scale), (int)(I_h * scale)), Color.White);
                spritebatch.DrawString(font, S_text, new Vector2(I_x + 20, I_y + (I_h / 2)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * scale), SpriteEffects.None, 0);
            }





        }
        //###########################################################

    }
}
