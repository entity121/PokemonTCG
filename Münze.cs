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
        public static int münzeSeite = 0;
        public static int münzeErgebnis;
        public static int timer = 0;
        public static bool münzwurf = false;
        public static SpriteBatch spritebatch;


        public static int Münzwurf()
        {

            Random random = new Random();
            münzeErgebnis = random.Next(0, 2);

            timer = 0;
            münzwurf = true;

            Thread thr = new Thread(() => Münze.Münze_Animation());
            thr.Start();

            Thread.Sleep(5000);

            return münzeErgebnis;
        }



        private static int Münze_Berechnen()
        {
            

            

            return münzeErgebnis;
        } 





        public static void Münze_Animation()
        {

            spritebatch.Begin();
            spritebatch.Draw(Lt2d_münzen[0], new Vector2(0, 0), Color.White);
            spritebatch.End();

            /*
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
                timer = 0;
                münzwurf = false;
            }    */           

        }


        


    }
}
