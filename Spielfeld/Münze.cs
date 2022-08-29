using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonTCG.Spielfeld
{
    class Münze
    {

        public bool Münzwurf()
        {
            Random random = new Random();

            int ergebnis = random.Next(0,2);

            if(ergebnis == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
