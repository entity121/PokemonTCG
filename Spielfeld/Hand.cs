using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PokemonTCG.Spielfeld
{
    class Hand
    {

        //VARIABLEN
        //#############################
        private List<Karte> L_karten;

        private static int I_abstand = 37;
        private static int I_brettW = 1846;
        private static int I_brettH = 360;
        private static int I_spielfeldDefault = 1080;

        private int I_X;
        private int I_Y;
        private int I_W;
        private int I_H;
        //#############################


        //KONSTRUKTOR
        //#######################################
        public Hand()
        {
            this.L_karten = new List<Karte>();
        }
        //#####
        public Hand(double skalierung)
        {
            this.I_X = (int)(I_abstand * skalierung);
            this.I_Y = (int)((I_spielfeldDefault - 50)*skalierung);
            this.I_W = (int)(I_brettW * skalierung);
            this.I_H = (int)(I_brettH * skalierung);
        }
        //#######################################



        //###########################################################
        public Rectangle Brett_Position()
        {
            Rectangle r = new Rectangle(I_X, I_Y, I_W, I_H);
            return r;
        }
        //###########################################################




        //###########################################################
        public void Brett_Hover()
        {
            var mouseState = Mouse.GetState();
            var mousePoint = new Point(mouseState.X, mouseState.Y);
            var rectangle = new Rectangle(mousePoint.X, mousePoint.Y, I_W, I_H);

            if (rectangle.Contains(mousePoint))
            {
                System.Windows.Forms.MessageBox.Show(mouseState.X.ToString()+"--"+mouseState.Y.ToString());
            }
        }
        //###########################################################





        //###########################################################
        public void Karten_Aufnehmen(Karte k)
        {
            L_karten.Add(k);
        }
        //###########################################################



        //###########################################################
        public Karte Karte_Entfernen(int index)
        {
            Karte karte = L_karten[index];

            L_karten.RemoveAt(index);

            return karte;
        }
        //###########################################################




        //###########################################################
        public int[] Hand_Zeigen()
        {
            int[] k = new int[L_karten.Count];

            for (int i = 0; i < L_karten.Count; i++)
            {
                k[i] = L_karten[i].I_ID;
            }

            return k;

        }
        //###########################################################






    }
}
