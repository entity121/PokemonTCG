using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.Spielfeld
{
    class Münze
    {
        private SpriteBatch spritebatch;

        public bool Münzwurf()
        {
            Random random = new Random();

            int ergebnis = random.Next(0,2);

            if(ergebnis == 0)
            {
                Draw(ergebnis);
                return false;
            }
            else
            {
                Draw(ergebnis);
                return true;
            }

        }


        public void Draw(int ergebnis)
        {

            for(int i = 0; i<10 ; i++)
            {
                f
            }


        }

    }
}
