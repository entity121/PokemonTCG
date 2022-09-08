using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PokemonTCG
{
    public static class MausPunkt
    {

        public static Point MausPoint()
        {
            Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
            return p;
        }

    }
}
