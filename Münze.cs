using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework;

namespace PokemonTCG
{

    // Globale Variablen für Probleme die nur so gelöst werden können
    static class Münze
    {

        public static List<Texture2D> Lt2d_münzen = new List<Texture2D>();
        public static Texture2D T2D_brettKlein;
        public static SpriteBatch spritebatch;

        public static int I_münzeSeite = 0;
        public static int I_münzeErgebnis;
        public static int I_timer = 0;
        public static bool B_münzwurf = false;

        public static int I_x = 1530;
        public static int I_y = 200;
        public static int I_w = 350;
        public static int I_h = 350;


        //###########################################################
        public static bool Münzwurf()
        {
            B_münzwurf = true;

            Random random = new Random();
            I_münzeErgebnis = random.Next(0, 2);

            I_timer = 0;


            Münze_Animation();

            I_timer = 0;
            B_münzwurf = false;

            if(I_münzeErgebnis == 0)
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
        private static void Münze_Animation()
        {

            while (I_timer <= 150)
            {
                if (I_timer < 100)
                {
                    if (I_timer % 10 == 0)
                    {
                        if (I_münzeSeite == 0)
                        {
                            I_münzeSeite = 1;
                        }
                        else
                        {
                            I_münzeSeite = 0;
                        }
                    }

                    I_timer += 1;
                }
                else if (I_timer < 150)
                {

                    I_münzeSeite = I_münzeErgebnis;
                    I_timer += 1;
                }
                else if (I_timer == 150)
                {
                    return;
                }

                Thread.Sleep(10);
            }
          
        }
        //###########################################################




    }
}
