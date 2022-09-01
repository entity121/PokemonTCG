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

        public static int münzeSeite = 0;
        public static int münzeErgebnis;
        public static int timer = 0;
        public static bool münzwurf = false;

        public static int x = 1530;
        public static int y = 200;
        public static int w = 350;
        public static int h = 350;



        public static int Münzwurf()
        {
            münzwurf = true;

            Random random = new Random();
            münzeErgebnis = random.Next(0, 2);

            timer = 0;


            Münze_Animation();

            timer = 0;
            münzwurf = false;

            return münzeErgebnis;

        }





        public static void Münze_Animation()
        {

            while (timer <= 150)
            {
                if (timer < 100)
                {
                    if (timer % 10 == 0)
                    {
                        if (münzeSeite == 0)
                        {
                            münzeSeite = 1;
                        }
                        else
                        {
                            münzeSeite = 0;
                        }
                    }

                    timer += 1;
                }
                else if (timer < 150)
                {

                    münzeSeite = münzeErgebnis;
                    timer += 1;
                }
                else if (timer == 150)
                {
                    return;
                }

                Thread.Sleep(10);
            }
          
        }


        


    }
}
