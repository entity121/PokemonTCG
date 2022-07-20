using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PokemonTCG.Spielfeld
{
    class Brett
    {

        //VARIABLEN
        //#############################
        private static int I_abstandDEFAULT = 37;
        private static int I_WDEFAULT = 1846;
        private static int I_HDEFAULT = 360;
        private static int I_spielfeldDEFAULT = 1080;
        private static int I_ausfahrDistanzDEFAULT = 200;
        private static int I_mitteDEFAULT = (1846 / 2)+37;

        private static int I_karteWDEFAULT = 125;
        private static int I_karteHDEFAULT = 176;


        private int I_brettX;
        private int I_brettY;
        private int I_brettW;
        private int I_brettH;

        private int I_karteX;
        private int I_karteY;
        private int I_karteW;
        private int I_karteH;

        private int I_ausfahrDistanz;
        private bool B_brettAusgefahren = false;
        //#############################




        //#######################################
        public Brett(double skalierung)
        {
            this.I_brettX = (int)(I_abstandDEFAULT * skalierung);
            this.I_brettY = (int)((I_spielfeldDEFAULT - 50) * skalierung);
            this.I_brettW = (int)(I_WDEFAULT * skalierung);
            this.I_brettH = (int)(I_HDEFAULT * skalierung);

            this.I_karteW = (int)(I_karteWDEFAULT * skalierung);
            this.I_karteH = (int)(I_karteHDEFAULT * skalierung);

            this.I_karteX = (int)((I_mitteDEFAULT - (I_karteW / 2)) * skalierung);

            this.I_karteY = (int)((I_brettY + 20) * skalierung);

            this.I_ausfahrDistanz = (int)(I_ausfahrDistanzDEFAULT * skalierung);
        }
        //#######################################



        //###########################################################
        public Rectangle Brett_Position()
        {
            Rectangle r = new Rectangle(I_brettX, I_brettY, I_brettW, I_brettH);
            return r;
        }
        //###########################################################



        //###########################################################
        public Rectangle Karte_Position()
        {
            Rectangle r = new Rectangle(I_karteX, I_karteY, I_karteW, I_karteH);
            return r;
        }
        //###########################################################




        //###########################################################
        public void Brett_Verschieben()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var rectangle = Brett_Position();

            if (rectangle.Contains(mousePoint))
            {
                if (B_brettAusgefahren == false)
                {
                    I_brettY -= I_ausfahrDistanz;
                    I_karteY -= I_ausfahrDistanz;
                    B_brettAusgefahren = true;
                }
            }
            else
            {
                if (B_brettAusgefahren == true)
                {
                    I_brettY += I_ausfahrDistanz;
                    I_karteY += I_ausfahrDistanz;
                    B_brettAusgefahren = false;
                }
            }
        }
        //###########################################################









    }
}
