using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace PokemonTCG.Spielfeld
{
    class Hand
    {

        //VARIABLEN
        //#############################
        // Default Werte sind statisch und werden mit der Skalierung an die richtige Größe angepasst
        private static int I_abstandDEFAULT = 37;
        private static int I_WDEFAULT = 1846;
        private static int I_HDEFAULT = 360;
        private static int I_spielfeldDEFAULT = 1080;
        private static int I_ausfahrDistanzDEFAULT = 150;
        private static int I_mitteDEFAULT = (1920 / 2);
        private static int I_karteWDEFAULT = 125;
        private static int I_karteHDEFAULT = 175;
        


        private int I_brettX;
        private int I_brettY;
        private int I_brettW;
        private int I_brettH;

        private int[] Ai_karteX = new int[0];
        private int I_karteY;
        private int I_karteW;
        private int I_karteH;


        private List<Karte> Lo_karten;
        private Karte[,] A2o_karten;
        private int I_reiheAngezeigt = 0;

        private SpriteBatch spriteBatch;
        private List<Texture2D> Lt2d_karten;
        private Texture2D T2D_holzbrett;

        private int I_abstand;
        private int I_mitte;
        private int I_ausfahrDistanz;
        private bool B_brettAusgefahren = false;

        private KartenAnzeige O_kartenAnzeige;

        private MouseState lastState = new MouseState();

        //#############################




        //#######################################
        public Hand(double skalierung,SpriteBatch spriteBatch, List<Texture2D> list, Texture2D holz)
        {      
            this.I_brettX = (int)(I_abstandDEFAULT * skalierung);
            this.I_brettY = (int)((I_spielfeldDEFAULT - 50) * skalierung);
            this.I_brettW = (int)(I_WDEFAULT * skalierung);
            this.I_brettH = (int)(I_HDEFAULT * skalierung);

            this.I_karteW = (int)(I_karteWDEFAULT * skalierung);
            this.I_karteH = (int)(I_karteHDEFAULT * skalierung);
            this.I_karteY = (int)((I_brettY + 20) * skalierung);

            this.Lo_karten = new List<Karte>();

            this.spriteBatch = spriteBatch;
            this.T2D_holzbrett = holz;
            this.Lt2d_karten = list;

            this.O_kartenAnzeige = new KartenAnzeige(spriteBatch, skalierung,list);

            this.I_mitte = (int)(I_mitteDEFAULT * skalierung);
            this.I_ausfahrDistanz = (int)(I_ausfahrDistanzDEFAULT * skalierung);
        }
        //#######################################




        //###########################################################
        public bool Basis_Pokemon(int id)
        {
            if (Lo_karten[id].S_vorentwicklung == "")
            {
                return true;
            }
            return false;
        }
        //###########################################################





        // Während man sich mit der Maus überhalb des Brettes befindet wird es ausgefahren
        // Geht man mit der Maus weg, dann fährt es wieder ein
        //###########################################################
        public bool Brett_Hover(Point mousePoint)
        {

            Rectangle rectangle = Brett_Position();

            if (rectangle.Contains(mousePoint))
            {
                if (B_brettAusgefahren == false)
                {
                    I_brettY -= I_ausfahrDistanz;
                    I_karteY -= I_ausfahrDistanz;
                    B_brettAusgefahren = true;
                }
                Mausrad_Bewegen();
                return true;
            }
            else
            {
                if (B_brettAusgefahren == true)
                {
                    I_brettY += I_ausfahrDistanz;
                    I_karteY += I_ausfahrDistanz;
                    B_brettAusgefahren = false;
                }
                return false;
            }
        }
        //###########################################################






        // Bildschirmposition des Brettes
        //###########################################################
        public Rectangle Brett_Position()
        {
            Rectangle r = new Rectangle(I_brettX, I_brettY, I_brettW, I_brettH);
            return r;
        }
        //###########################################################





        //###########################################################
        public Karte Get_Karte_In_Hand(int id)
        {
            return Lo_karten[id];
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





        //###########################################################
        public char Karten_Art(int id)
        {

            switch (Lo_karten[id].S_art)
            {
                case "Pokémon": { return 'p'; }; break;
                case "Trainer": { return 't'; }; break;
                case "Energie": { return 'e'; }; break;
            }

            return 'n';
        }
        //###########################################################





        // Eine Karte in die Hand aufnehmen
        //###########################################################
        public void Karten_Aufnehmen(Karte k)
        {
            Lo_karten.Add(k);
            Karten_Platzieren();
        }
        //###########################################################




        private bool B_halten = false;
        private Rectangle R_gehaltenPosition;
        private int I_gehaltenID;
        //###########################################################
        public void Karte_Bewegen(int karte, Point mousePoint)
        {

            B_halten = true;
            R_gehaltenPosition = new Rectangle(mousePoint.X, mousePoint.Y, I_karteW, I_karteH);
            I_gehaltenID = karte;

        }
        //###########################################################





        // Eine Karte aus der Hand entfernen
        //###########################################################
        public Karte Karte_Entfernen(int index)
        {
            Karte karte = Lo_karten[index];

            Lo_karten.RemoveAt(index);
            Karten_Platzieren();

            return karte;
        }
        //###########################################################





        // Wenn man mit der Maus über eine Karte auf dem Brett hovert, 
        // dann soll diese groß am Bildschirm angezeigt werden
        //###########################################################
        public int Karte_Hover(Point mousePoint)
        {

            for (int i = 0; i < Ai_karteX.Length; i++)
            {
                Rectangle karte = Karte_Position(i);

                if (karte.Contains(mousePoint))
                {
                    O_kartenAnzeige.Set_AnzeigeID(Lo_karten[i].I_ID);
                    return i;
                }
                else
                {
                    O_kartenAnzeige.Set_AnzeigeID(0);
                }
            }
            return -1;
        }
        //###########################################################





        // Die X-Koordinaten der Karten auf dem Brett sollen so berechnet werden,
        // dass egal wie viele Karten darauf liegen, sie immer in der Mitte des Brettes angezeigt werden
        // Die berechneten Werte kommen in die Liste des Brett-Objektes
        //###########################################################
        public void Karten_Platzieren()
        {


            int reihen = Lo_karten.Count / 14;
            if (Lo_karten.Count % 14 != 0) { reihen += 1; }

            A2o_karten = new Karte[reihen, 14];

            for (int k = 0; k < Lo_karten.Count; k++)
            {
                A2o_karten[k / 14, k % 14] = Lo_karten[k];
            }


            int anzahl = 0;

            for (int i = 0; i < 14; i++)
            {
                if (A2o_karten[I_reiheAngezeigt, i] != null)
                {
                    anzahl += 1;
                }
                else
                {
                    break;
                }
            }


            this.Ai_karteX = new int[anzahl];



            // Es wird unterschieden, ob es eine gerade...
            if (anzahl % 2 == 0)
            {
                int hälfte = anzahl / 2;

                for (int i = 1; i <= hälfte; i++)
                {
                    Ai_karteX[i - 1] = I_mitte - (I_karteW * (i + 1 - 1));
                }
                for (int i = hälfte; i < anzahl; i++)
                {
                    Ai_karteX[i] = I_mitte + (I_karteW * (i - hälfte));
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
                        Ai_karteX[i] = I_mitte - ((I_karteW * (hälfte - i)) + halbeKarte);
                    }
                    for (int i = hälfte; i < anzahl - 1; i++)
                    {
                        Ai_karteX[i + 1] = I_mitte + ((I_karteW * (i - hälfte)) + halbeKarte);
                    }
                }

            }

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




        //###########################################################
        public void Mausrad_Bewegen()
        {

            MouseState newState = new MouseState();

            if(newState.ScrollWheelValue > lastState.ScrollWheelValue)
            {
                I_reiheAngezeigt += 1;
            }
            else if(newState.ScrollWheelValue < lastState.ScrollWheelValue)
            {
                I_reiheAngezeigt -= 1;
            }

            lastState = newState;
        }
        //###########################################################






        //###########################################################
        public void Draw()
        {

            spriteBatch.Draw(T2D_holzbrett, Brett_Position(), Color.White);

            for (int i = 0; i < Ai_karteX.Length; i++)
            {
                spriteBatch.Draw(Lt2d_karten[A2o_karten[I_reiheAngezeigt,i].I_ID], Karte_Position(i), Color.White);
            }

            O_kartenAnzeige.Draw();

            if (B_halten == true)
            {
                spriteBatch.Draw(Lt2d_karten[I_gehaltenID], R_gehaltenPosition, Color.White);
                B_halten = false;
            }

        }
        //###########################################################
    }
}
