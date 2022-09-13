using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.Textboxen
{
    class Textfeld
    {

        public Texture2D buchstabe;
        public int x;
        public int y;



        public Textfeld(Texture2D b, int x, int y)
        {
            this.buchstabe = b;
            this.x = x;
            this.y = y;
        }




    }
}
