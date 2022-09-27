using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PokemonTCG.MenschlicherSpieler
{
    public static class ElementAuswahl
    {
        public static bool B_wählen = false;
        public static Texture2D T2D_brett;

        private static List<int> Li_draw = new List<int>();
        private static List<bool> Lb_wahl = new List<bool>();


        private static int I_elW = 150;
        private static int I_elH = 150;
        private static int I_elX = 100;
        private static int I_elY = 100;

        private static int I_el2X = 155;

        private static int I_brW = 1000;
        private static int I_brH = 400;
        private static int I_brX = (1920/2)-(500);
        private static int I_brY = 300;

        private static int I_buW = 150;
        private static int I_buH = 50;
        private static int I_buX = 200;
        private static int I_buY = 300;

        private static int I_bu2X = 400;

        private static bool B_OK_hover = false;
        private static bool B_Abb_hover = false;

        private static MouseState lastState = Mouse.GetState();
        //
        //
        //
        //
        //
        //
        //###########################################################
        public static bool[] Energie_Auswählen(List<string>l)
        {
            B_wählen = true;
            Li_draw = new List<int>();
            Lb_wahl = new List<bool>();

            for(int i = 0; i < l.Count; i++)
            {
                Li_draw.Add(Energie_Übersetzen(l[i]));
                Lb_wahl.Add(false);
            }

            while(B_wählen == true)
            {
                Element_Hover();
                Button_Hover();
            }

            return Lb_wahl.ToArray();

        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private static void Button_Hover()
        {
            var mouseState = Mouse.GetState();
            Point point = MausPunkt.MausPoint();

            // OK Button
            if(new Rectangle(I_brX + I_buX, I_brY + I_buY, I_buW, I_buH).Contains(point))
            {
                B_OK_hover = true;
                if(mouseState.LeftButton==Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton!= Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    B_wählen = false;
                }
            }
            else
            {
                B_OK_hover = false;
            }

            // Abbrechen Button
            if (new Rectangle(I_brX + I_buX + I_bu2X, I_brY + I_buY, I_buW, I_buH).Contains(point))
            {
                B_Abb_hover = true;
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    for(int i = 0; i < Lb_wahl.Count; i++)
                    {
                        Lb_wahl[i] = false;
                    }

                    B_wählen = false;
                }
            }
            else
            {
                B_Abb_hover = false;
            }

        } 
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private static void Element_Hover()
        {
            var mouseState = Mouse.GetState();

            for(int i = 0; i < Lb_wahl.Count; i++)
            {
                Point point = MausPunkt.MausPoint();
                if (Element_Position(i).Contains(point))
                {
                    if(mouseState.LeftButton==Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton!= Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        Lb_wahl[i] = !Lb_wahl[i];
                    }
                }
            }

            lastState = mouseState;
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private static Rectangle Element_Position(int id)
        {
            return new Rectangle(I_brX + (I_el2X * id) + 50, I_brY + I_elY, I_elW, I_elH);
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private static int Energie_Übersetzen(string s)
        {
            string[] elementReihenfolge = new string[] { "Elektro", "Farblos", "Feuer", "Kampf", "Pflanze", "Psycho", "Wasser" };
        
            for(int i = 0; i < elementReihenfolge.Length; i++)
            {
                if (elementReihenfolge[i] == s)
                {
                    return i;
                }
            }
            return -1;
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        public static void Draw(SpriteBatch sb, List<Texture2D>elemente, Texture2D[]buttons)
        {

            sb.Draw(T2D_brett, new Rectangle(I_brX, I_brY, I_brW, I_brH), Color.White);

            for(int i = 0; i < Li_draw.Count; i++)
            {
                sb.Draw(elemente[Li_draw[i]],Element_Position(i),Color.White);
                if (Lb_wahl[i] == false)
                {
                    sb.Draw(elemente[7], Element_Position(i), Color.White);
                }
            }


            // OK Button
            if (B_OK_hover == true)
            {
                sb.Draw(buttons[1], new Rectangle(I_brX + I_buX, I_brY + I_buY, I_buW, I_buH), Color.White);
            }
            else
            {
                sb.Draw(buttons[0], new Rectangle(I_brX + I_buX, I_brY + I_buY, I_buW, I_buH), Color.White);
            }

            // Abbruch Button
            if(B_Abb_hover == true)
            {
                sb.Draw(buttons[3], new Rectangle(I_brX + I_buX + I_bu2X, I_brY + I_buY, I_buW, I_buH), Color.White);
            }
            else
            {
                sb.Draw(buttons[2], new Rectangle(I_brX + I_buX + I_bu2X, I_brY + I_buY, I_buW, I_buH), Color.White);
            }


        }
        //###########################################################
        //
        //
        //
        //
        //
        //

    }
}
