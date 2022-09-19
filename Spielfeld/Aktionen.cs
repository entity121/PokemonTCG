using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;



namespace PokemonTCG.Spielfeld
{
    class Aktionen
    {

        private int I_x;
        private int I_y;
        private int I_w;
        private int I_h;

        private int I_wAuswahl;
        private int I_hAuswahl;
        private int I_hover = -1;

        private double D_scale;

        private Karte O_karte;

        private string S_angriff1;
        private string[] As_kosten1;
        private int I_schaden1;

        private bool B_angriff2;
        private string S_angriff2;
        private string[] As_kosten2;
        private int I_schaden2;

        private bool B_fähigkeit;
        private int I_fähigkeit;

        private int I_rückzugskosten;

        private List<string> Ls_kartenEnergie;
        List<string> Ls_e1 = new List<string>();
        List<string> Ls_e2 = new List<string>();

        private string[] elementReihenfolge = new string[] { "Elektro", "Farblos", "Feuer", "Kampf", "Pflanze", "Psycho", "Wasser" };




        //###########################################################
        public Aktionen(Karte karte, int x, int y, double scale)
        {
            this.O_karte = karte;

            this.Ls_kartenEnergie = karte.Ls_energieAngelegt;

            // Position
            this.I_x = x;
            this.I_y = y;
            this.I_w = (int)(200 * scale);
            this.I_h = (int)(265 * scale);

            this.I_wAuswahl = (int)(200 * scale);
            this.I_hAuswahl = (int)(66 * scale);

            this.D_scale = scale;


            // Angriff1
            this.S_angriff1 = karte.S_angriff1;
            this.As_kosten1 = new string[karte.I_kosten1];

            if (karte.S_energie1 != "")
            {
                for (int i = 0; i < (karte.I_kosten1 - karte.I_farblos1); i++)
                {
                    As_kosten1[i] = karte.S_energie1;
                }

                for (int i = (karte.I_kosten1 - karte.I_farblos1); i < karte.I_kosten1; i++)
                {
                    As_kosten1[i] = "Farblos";
                }

            }
            else
            {
                for (int i = 0; i < karte.I_kosten1; i++)
                {
                    As_kosten1[i] = "Farblos";
                }
            }

            this.I_schaden1 = karte.I_schaden1;




            // Angriff2
            if (karte.S_angriff2 != "")
            {
                B_angriff2 = true;

                this.S_angriff2 = karte.S_angriff2;
                this.As_kosten2 = new string[karte.I_kosten2];

                if (karte.S_energie2 != "")
                {
                    for (int i = 0; i < (karte.I_kosten2 - karte.I_farblos2); i++)
                    {
                        As_kosten2[i] = karte.S_energie2;
                    }

                    for (int i = (karte.I_kosten2 - karte.I_farblos2); i < karte.I_kosten2; i++)
                    {
                        As_kosten2[i] = "Farblos";
                    }

                }
                else
                {
                    for (int i = 0; i < karte.I_kosten2; i++)
                    {
                        As_kosten2[i] = "Farblos";
                    }
                }

                this.I_schaden2 = karte.I_schaden2;
            }
           

            // Fähigkeit
            if(karte.I_fähigkeit == 1)
            {
                this.I_fähigkeit = karte.I_fähigkeit;
            }
            
            // Rückzugskosten
            this.I_rückzugskosten = karte.I_rückzugskosten;
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Welchen Index hat der Element String in der Liste mit den Element Namen
        //###########################################################
        private int Get_Element_Position(string s)
        {
            int erg;

            if (s == "")
            {
                return 1;
            }
            else
            {
                for (int i = 0; i < elementReihenfolge.Length; i++)
                {
                    if (elementReihenfolge[i] == s)
                    {
                        return i;
                    }
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
        // Es wird für die 4 Aswahlfelder geschaut ob die Maus darüber ist, um dieses ggf. Hervorzuheben
        //###########################################################
        public bool Hover()
        {
            Point mousePoint = MausPunkt.MausPoint();

            for(int i = 0; i < 4; i++)
            {
                if (Position(i).Contains(mousePoint))
                {
                    I_hover = i;
                    return true;
                }
            }

            I_hover = -1;
            return false;
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Die Koordinate von einer der 4 Auswahlmöglichkeiten
        //###########################################################
        private Rectangle Position(int position)
        {
            return new Rectangle(I_x, (I_y+(position*I_hAuswahl)), I_wAuswahl, I_hAuswahl);                   
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Die Koordinate der ElementSymbole zu den entsprechenden Attacken
        //###########################################################
        private Rectangle Position(int x, int y)
        {
            return new Rectangle(I_x + (15 * x)+15, I_y + (I_hAuswahl * y)+(I_hAuswahl-25), 15, 15);
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Die Koordinate der Scadenszahl einer Attacke
        //###########################################################
        private Vector2 DMG_Position(int y)
        {
            return new Vector2(I_x + I_wAuswahl - 50, I_y + 35 + (I_hAuswahl * y));
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Der Inhalt der Liste mit den an die Karte angelegten Energien
        // wird in eine andere Liste übertragen, da einträge später entfernt werden müssen
        //###########################################################
        private List<string> Liste_Übertragen()
        {
            List<String> l = new List<string>();

            for(int i = 0; i < Ls_kartenEnergie.Count; i++)
            {
                l.Add(Ls_kartenEnergie[i]);
            }

            return l;
        }
        //###########################################################
        //
        //
        //
        //
        //
        // Es wird geschaut, ob sich ein bestimmtes Element an dem Pokemon befindet
        // Wenn ja wird es entfernt und das ElementSymbol am Bildschirm hervorgehoben
        // Bei einem Farblosen Element wird das erste entfernt
        //###########################################################
        private bool Listen_Vergleichen(string element, List<string> list)
        {
            bool erg = false;


            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == element)
                    {
                        list.RemoveAt(i);
                        erg = true;
                        break;
                    }
                }

                if (element == "Farblos")
                {
                    list.RemoveAt(0);
                    erg = true;
                }
            }


            return erg;
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        public void Draw(Texture2D auswahlS, Texture2D auswahlW, SpriteBatch spriteBatch, SpriteFont font, List<Texture2D> elemente)
        {

            
            // 1. Auwahhlmöglichkeit einer Karte
            //###################################
            if(I_hover == 0)
            {
                spriteBatch.Draw(auswahlW, Position(0), Color.White);
            }
            else
            {
                spriteBatch.Draw(auswahlS, Position(0), Color.White);
            }
            

            spriteBatch.DrawString(font, S_angriff1, new Vector2(I_x + (int)(10*D_scale), I_y + (int)(10 * D_scale)), Color.Black,0,new Vector2(0,0), (float)(1.4*D_scale), SpriteEffects.None,0);


            Ls_e1 = Liste_Übertragen();

            for(int i = 0; i < As_kosten1.Length; i++)
            {

                spriteBatch.Draw(elemente[Get_Element_Position(As_kosten1[i])], Position(i, 0), Color.White);

                if (Listen_Vergleichen(As_kosten1[i],Ls_e1) == false)
                {
                    spriteBatch.Draw(elemente[7], Position(i, 0), Color.White);
                }
      
            }

            if (I_schaden1 > 0)
            {
                spriteBatch.DrawString(font, I_schaden1.ToString(),DMG_Position(0),Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            }
            //###################################




            // 2. Auwahhlmöglichkeit einer Karte
            //###################################
            if (B_angriff2 == true)
            {
                if (I_hover == 1)
                {
                    spriteBatch.Draw(auswahlW, Position(1), Color.White);
                }
                else
                {
                    spriteBatch.Draw(auswahlS, Position(1), Color.White);
                }

                spriteBatch.DrawString(font, S_angriff2, new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + I_hAuswahl), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);


                Ls_e2 = Liste_Übertragen();

                for (int i = 0; i < As_kosten2.Length; i++)
                {

                    spriteBatch.Draw(elemente[Get_Element_Position(As_kosten2[i])], Position(i, 1), Color.White);

                    if (Listen_Vergleichen(As_kosten2[i], Ls_e2) == false)
                    {
                        spriteBatch.Draw(elemente[7], Position(i, 1), Color.White);
                    }

                }


                if (I_schaden2 > 0)
                {
                    spriteBatch.DrawString(font, I_schaden2.ToString(), DMG_Position(1), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
                }

            }
            //###################################



            // 3. Auwahhlmöglichkeit einer Karte
            //###################################
            int v = 1;
            if (B_fähigkeit == true)
            {
                
                if(B_angriff2 == true)
                {
                    v += 2;
                }

                if (I_hover == v)
                {
                    spriteBatch.Draw(auswahlW, Position(v), Color.White);
                }
                else
                {
                    spriteBatch.Draw(auswahlS, Position(v), Color.White);
                }

                spriteBatch.DrawString(font, "Pokemon-Power", new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + (v*I_hAuswahl)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            }
            //###################################



            // 4. Auwahhlmöglichkeit einer Karte
            //###################################
            v = 1;
            if(B_angriff2 == true) { v += 1; }
            if(B_fähigkeit == true) { v += 1; }

            if (I_hover == v)
            {
                spriteBatch.Draw(auswahlW, Position(v), Color.White);
            }
            else
            {
                spriteBatch.Draw(auswahlS, Position(v), Color.White);
            }

            spriteBatch.DrawString(font, "Rueckzug", new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + (v * I_hAuswahl)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            

            for(int i = 0; i < I_rückzugskosten; i++)
            {
                spriteBatch.Draw(elemente[1], Position(i,v), Color.White);
                try
                {
                    string check = Ls_kartenEnergie[i];
                }
                catch
                {
                    spriteBatch.Draw(elemente[7], Position(i, v), Color.White);
                }             
                
            }         
            //###################################



        }
        //###########################################################
    }
}
