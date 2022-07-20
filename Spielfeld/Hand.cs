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

        private static int I_abstandDEFAULT = 37;
        private static int I_WDEFAULT = 1846;
        private static int I_HDEFAULT = 360;
        private static int I_spielfeldDEFAULT = 1080;
        private static int I_ausfahrDistanzDEFAULT = 150;
        private static int I_mitteDEFAULT = (1920 / 2);
        private static int I_karteWDEFAULT = 125;
        private static int I_karteHDEFAULT = 176;


        private int I_brettX;
        private int I_brettY;
        private int I_brettW;
        private int I_brettH;

        private int[] Ai_karteX;
        private int I_karteY;
        private int I_karteW;
        private int I_karteH;


        private List<Karte> Lo_karten;

        private int I_abstand;
        private int I_mitte;
        private int I_ausfahrDistanz;
        private bool B_brettAusgefahren = false;
        //#############################




        //#######################################
        public Hand(double skalierung)
        {      
            this.I_brettX = (int)(I_abstandDEFAULT * skalierung);
            this.I_brettY = (int)((I_spielfeldDEFAULT - 50) * skalierung);
            this.I_brettW = (int)(I_WDEFAULT * skalierung);
            this.I_brettH = (int)(I_HDEFAULT * skalierung);

            this.I_karteW = (int)(I_karteWDEFAULT * skalierung);
            this.I_karteH = (int)(I_karteHDEFAULT * skalierung);

            this.I_karteY = (int)((I_brettY + 20) * skalierung);



            this.Lo_karten = new List<Karte>();


            this.I_mitte = (int)(I_mitteDEFAULT * skalierung);
            this.I_ausfahrDistanz = (int)(I_ausfahrDistanzDEFAULT * skalierung);
        }
        //#######################################




        // Bildschirmposition des Brettes
        //###########################################################
        public Rectangle Brett_Position()
        {
            Rectangle r = new Rectangle(I_brettX, I_brettY, I_brettW, I_brettH);
            return r;
        }
        //###########################################################




        // Bildschirmposition einer Karte
        //###########################################################
        public Rectangle Karte_Position(int karte)
        {             
            Rectangle r = new Rectangle(Ai_karteX[karte], I_karteY, I_karteW, I_karteH);
            return r;
        }
        //###########################################################



        // Die X-Koordinaten der Karten auf dem Brett sollen so berechnet werden,
        // dass egal wie viele Karten darauf liegen, sie immer in der Mitte des Brettes angezeigt werden
        // Die berechneten Werte kommen in die Liste des Brett-Objektes
        //###########################################################
        public void Karten_Platzieren()
        {         
            int anzahl = Lo_karten.Count;
            this.Ai_karteX = new int[anzahl];

            // Es wird unterschieden, ob es eine gerade...
            if (anzahl % 2 == 0)
            {
                int hälfte = anzahl / 2;

                for (int i = 1; i <= hälfte; i++)
                {
                    Ai_karteX[i-1] = I_mitte - (I_karteW * (i+1-1));
                }
                for (int i = hälfte; i < anzahl; i++)
                {
                    Ai_karteX[i] = I_mitte + (I_karteW * (i-hälfte));
                }
            }
            //... oder ungerade Anzahl ist
            else
            {
                int hälfte = anzahl / 2;
                int halbeKarte = I_karteW / 2;

                Ai_karteX[hälfte] = I_mitte - halbeKarte;

                if (anzahl >= 3)
                {
                    for (int i = 0; i < hälfte; i++)
                    {
                        Ai_karteX[i] = I_mitte - ((I_karteW * (hälfte-i)) + halbeKarte);
                    }
                    for (int i = hälfte; i < anzahl-1; i++)
                    {
                        Ai_karteX[i+1] = I_mitte + ((I_karteW*(i-hälfte)) + halbeKarte);
                    }
                }

            }

        }
        //###########################################################



        // Während man sich mit der Maus überhalb des Brettes befindet wird es ausgefahren
        // Geht man mit der Maus weg, dann fährt es wieder ein
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




        // Eine Karte in die Hand aufnehmen
        //###########################################################
        public void Karten_Aufnehmen(Karte k)
        {
            Lo_karten.Add(k);
        }
        //###########################################################



        // Eine Karte aus der Hand entfernen
        //###########################################################
        public Karte Karte_Entfernen(int index)
        {
            Karte karte = Lo_karten[index];

            Lo_karten.RemoveAt(index);

            return karte;
        }
        //###########################################################



        // Den gesammten Inhalt der Hand als Array ausgeben
        //###########################################################
        public Karte[] Hand_Zeigen()
        {
            Karte[] k = new Karte[Lo_karten.Count];

            for (int i = 0; i < Lo_karten.Count; i++)
            {
                k[i] = Lo_karten[i];
            }

            return k;
        }
        //###########################################################






    }
}
