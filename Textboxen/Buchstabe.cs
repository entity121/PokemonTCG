using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.Textboxen
{
    public class Buchstabe
    {

        public Texture2D T2D_buchstabe;
        public char C_buchstabe;

        public Buchstabe(Texture2D Texture, char Char)
        {
            this.T2D_buchstabe = Texture;
            this.C_buchstabe = Char;
        }



    }
}
