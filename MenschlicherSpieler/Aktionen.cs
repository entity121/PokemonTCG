using System;
using System.Collections.Generic;
using System.Text;
using PokemonTCG.Karten;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PokemonTCG.MenschlicherSpieler;
using PokemonTCG.Spielfeld;



namespace PokemonTCG.MenschlicherSpieler
{
    class Aktionen
    {

        private int I_x;
        private int I_y;
        private int I_w;
        private int I_h;

        private int I_x2 = 1600;
        private int I_y2 = 800;

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

        List<Karte> Lo_abgeworfeneKarten;

        private string[] elementReihenfolge = new string[] { "Elektro", "Farblos", "Feuer", "Kampf", "Pflanze", "Psycho", "Wasser" };

        BankAuswahl O_bankauswahl;

        private MouseState lastState;


        //###########################################################
        public Aktionen(Karte karte, int x, int y, double scale,Karte[]bank)
        {
            this.O_karte = karte;

            this.O_bankauswahl = new BankAuswahl(bank);

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
            this.S_angriff1 = Umlaute_Übersetzen(karte.S_angriff1);


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

                this.S_angriff2 = Umlaute_Übersetzen(karte.S_angriff2);
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
        //
        //###########################################################
        public void Aktionen_Ausführen()
        {
            Hover();
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private bool Aktion_Einsetzbar(string[]arr)
        {
            List<string> l = Liste_Übertragen();

            int anzahl = arr.Length;
            int count = 0;

            for(int i = 0; i < anzahl; i++)
            {
                if (arr[i] != "Farblos")
                {
                    for (int j = 0; j < l.Count; j++)
                    {
                        if (l[j] == arr[i])
                        {
                            l.RemoveAt(j);
                            count++;
                        }
                    }
                }
                else
                {
                    if (l.Count > 0)
                    {
                        l.RemoveAt(0);
                        count++;
                    }
                }
            }

            if(count == anzahl)
            {
                return true;
            }
            else
            {
                return false;
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
        private void Click(int id)
        {
            var mouseState = Mouse.GetState();
            if(mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && lastState.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && Spielzug.B_spielStart == false)
            {
                switch (id)
                {
                    case 0:
                        { Angriff_1(); };
                        break;
                    case 1:
                        { Angriff_2(); };
                        break;
                    case 2:
                        { Pokemon_Power(); };
                        break;
                    case 3:
                        { Rückzug(); };
                        break;
                    case 4:
                        { Zug_Beenden(); };                       
                        break;
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
        private void Angriff_1()
        {
            if (Aktion_Einsetzbar(As_kosten1))
            {
                Textbox.Infobox(O_karte.S_angriff1);
            }
            else
            {
                Textbox.Infobox("Nicht genug Energie");
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
        private void Angriff_2()
        {
            if (Aktion_Einsetzbar(As_kosten2))
            {
                Textbox.Infobox(O_karte.S_angriff2);
            }
            else
            {
                Textbox.Infobox("Nicht genug Energie");
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
        private void Pokemon_Power()
        {
            Textbox.Infobox("Pokemon Power");
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private void Rückzug()
        {

            string[] rüKosten = new string[I_rückzugskosten];
            for (int i = 0; i < I_rückzugskosten; i++)
            {
                rüKosten[i] = "Farblos";
            }

            if (Aktion_Einsetzbar(rüKosten))
            {

                bool[] auswahl = ElementAuswahl.Energie_Auswählen(O_karte.Ls_energieAngelegt);

                int anzahl = 0;
                for(int i = 0; i < auswahl.Length; i++)
                {
                    if (auswahl[i] == true)
                    {
                        anzahl++;
                    }
                }

                if (anzahl == O_karte.I_rückzugskosten)
                {

                    for(int i = 0; i < auswahl.Length; i++)
                    {
                        if (auswahl[i] == true)
                        {
                            O_karte.Ls_energieAngelegt.RemoveAt(i);
                            this.Ls_kartenEnergie = O_karte.Ls_energieAngelegt;
                            //Rückzug
                        }
                    }

                }
                else
                {
                    Textbox.Infobox("Rückzug abgebrochen");
                }
                
                    this.Ls_kartenEnergie = O_karte.Ls_energieAngelegt;
            }
            else
            {
                Textbox.Infobox("Nicht genug Energie");
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
        private void Zug_Beenden()
        {
            Textbox.Infobox("Zug beendet");
            Spielzug.B_spielerZug = false;
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

            int felder = 2;
            if(B_angriff2 == true) { felder += 1; }
            if(B_fähigkeit == true) { felder += 1; }

            for(int i = 0; i < felder; i++)
            {
                if (Position(i).Contains(mousePoint))
                {
                    if (i == 0) { Click(0); }
                    else if (i == 1 && felder == 2) { Click(3); }
                    else if (i == 1 && felder == 3)
                    {
                        if (B_angriff2 == true) { Click(1); }
                        else if(B_fähigkeit==true){ Click(2); }
                    }
                    else if (i==2 && felder == 3) { Click(3); }
                    else if(i==1 && felder == 4) { Click(1); }
                    else if(i==2 && felder == 4) { Click(2); }
                    else if(i==3 && felder == 4) { Click(3); }

                    I_hover = i;
                    KartenAnzeige.Set_AnzeigeID("slot", O_karte.I_ID);
                    return true;
                }
            }

            if (new Rectangle(I_x2, I_y2, I_wAuswahl, I_hAuswahl).Contains(mousePoint))
            {
                I_hover = 4;
                Click(4);
                return true;
            }

            I_hover = -1;
            lastState = Mouse.GetState();
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
        //
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
        // Da Ä,Ö;Ü nicht als Buchstaben dargestellt weden können, müssen diese 
        // in die Entsprechenden Umlaute ae,oe,ue umgewandelt werden
        //###########################################################
        private string Umlaute_Übersetzen(string s)
        {
            char[] buchstaben = s.ToCharArray();
            string erg = "";

            for (int i = 0; i < buchstaben.Length; i++)
            {
                if (buchstaben[i] == 'ä')
                {
                    erg += "ae";
                }
                else if (buchstaben[i] == 'ö')
                {
                    erg += "oe";
                }
                else if (buchstaben[i] == 'ü')
                {
                    erg += "ue";
                }
                else
                {
                    erg += buchstaben[i];
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
            if(I_hover == 0 && Aktion_Einsetzbar(As_kosten1))
            {
                spriteBatch.Draw(auswahlW, Position(0), Color.White);
            }
            else
            {
                spriteBatch.Draw(auswahlS, Position(0), Color.White);
            }

            if (Aktion_Einsetzbar(As_kosten1))
            {
                spriteBatch.DrawString(font, S_angriff1, new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(font, S_angriff1, new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale)), Color.Gray, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);

            }


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
                if (Aktion_Einsetzbar(As_kosten1))
                {
                    spriteBatch.DrawString(font, I_schaden1.ToString(), DMG_Position(0), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);

                }
                else
                {
                    spriteBatch.DrawString(font, I_schaden1.ToString(), DMG_Position(0), Color.Gray, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);

                }
            }
            //###################################




            // 2. Auwahhlmöglichkeit einer Karte
            //###################################
            if (B_angriff2 == true)
            {
                if (I_hover == 1 && Aktion_Einsetzbar(As_kosten2))
                {
                    spriteBatch.Draw(auswahlW, Position(1), Color.White);
                }
                else
                {
                    spriteBatch.Draw(auswahlS, Position(1), Color.White);
                }


                if (Aktion_Einsetzbar(As_kosten2))
                {
                    spriteBatch.DrawString(font, S_angriff2, new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + I_hAuswahl), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.DrawString(font, S_angriff2, new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + I_hAuswahl), Color.Gray, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);

                }


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
                    if (Aktion_Einsetzbar(As_kosten2))
                    {
                        spriteBatch.DrawString(font, I_schaden2.ToString(), DMG_Position(1), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, I_schaden2.ToString(), DMG_Position(1), Color.Gray, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
                    }
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

            string[] rüKosten = new string[I_rückzugskosten];
            for(int i = 0; i < I_rückzugskosten; i++)
            {
                rüKosten[i] = "Farblos";
            }

            if (I_hover == v && Aktion_Einsetzbar(rüKosten))
            {
                spriteBatch.Draw(auswahlW, Position(v), Color.White);
            }
            else
            {
                spriteBatch.Draw(auswahlS, Position(v), Color.White);
            }


            if (Aktion_Einsetzbar(rüKosten))
            {
                spriteBatch.DrawString(font, "Rueckzug", new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + (v * I_hAuswahl)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(font, "Rueckzug", new Vector2(I_x + (int)(10 * D_scale), I_y + (int)(10 * D_scale) + (v * I_hAuswahl)), Color.Gray, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            }

            for (int i = 0; i < I_rückzugskosten; i++)
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



            // Zug beenden
            //###################################
            if(I_hover == 4)
            {
                spriteBatch.Draw(auswahlW, new Rectangle(I_x2, I_y2, I_wAuswahl, I_hAuswahl), Color.White);
            }
            else
            {
                spriteBatch.Draw(auswahlS, new Rectangle(I_x2, I_y2, I_wAuswahl, I_hAuswahl), Color.White);
            }

            spriteBatch.DrawString(font, "Zug Beenden", new Vector2(I_x2 + (int)(10 * D_scale), I_y2 + (int)(10 * D_scale)), Color.Black, 0, new Vector2(0, 0), (float)(1.4 * D_scale), SpriteEffects.None, 0);
            //###################################


        }
        //###########################################################
    }
}
