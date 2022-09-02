using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Spielfeld;


namespace PokemonTCG
{
    public static class Spielzug
    {
        public static bool B_spielStart = true;
        public static bool B_spielerZug = true;



        public static bool B_ziehen = true;
        public static bool B_energie = true;

        TODO : Ein angriff soll den zug beenden;

        //###########################################################
        public static void Spielstart()
        {
            B_spielerZug = Münze.Münzwurf();

            if (B_spielerZug == true)
            {
                B_spielerZug = Textbox.Messagebox("Möchtest du anfangen");
            }
            else
            {
                B_spielerZug = Münze.Münzwurf();
            }

            B_spielStart = false;
        }
        //###########################################################




        //###########################################################
        public static void Spielzug_Check()
        {






        }
        //###########################################################




    }
}
