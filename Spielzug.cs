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

        

        //###########################################################
        public static void Spielstart()
        {
            //B_spielerZug = Münze.Münzwurf();

            /*if (B_spielerZug == true)
            {
                B_spielerZug = Textbox.Auswahlbox("Moechtest du anfangen´?");
            }
            else
            {
                B_spielerZug = Münze.Münzwurf();
            }*/
            //B_spielerZug = Textbox.Auswahlbox("Möchtest du anfangen?");
            B_spielerZug = Textbox.Auswahlbox("Möchtest du anfangen?");
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
