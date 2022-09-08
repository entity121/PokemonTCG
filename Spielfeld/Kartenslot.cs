using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;
using PokemonTCG.Karten;

namespace PokemonTCG.Spielfeld
{
    class Kartenslot
    {


        //private static int[] Ai_slotX = new int[] { 165, 314, 463, 612, 762, 60, 80, 100, 120, 140, 160, 463, 800, 935, 20, 155, 463, 795, 815, 835, 855, 875, 895, 165, 314, 463, 612, 762 };
        //private static int[] Ai_slotY = new int[] { 842, 842, 842, 842, 842, 604, 604, 604, 604, 604, 604, 569, 604, 604, 316, 316, 336, 316, 316, 316, 316, 316, 316, 80, 80, 80, 80, 80 };
        
        // Die Koordinaten der Kartenslots visuell von links nach rechts und unten nach oben
        // Werte müssen anhand Bildschirmgröße skaliert werden
        private static int[] Ai_slotX_weiß = new int[] { 800, 463, 165, 314, 463, 612, 762, 935, 60, 80, 100, 120, 140, 160 };
        private static int[] Ai_slotY_weiß = new int[] { 604, 569, 842, 842, 842, 842, 842, 604, 604, 604, 604, 604, 604, 604 };

        private static int[] Ai_slotX_rot = new int[] { 155, 463, 762, 612, 463, 314, 165, 20, 795, 815, 835, 855, 875, 895};
        private static int[] Ai_slotY_rot = new int[] { 316, 336, 80, 80, 80, 80, 80, 316, 316, 316, 316, 316, 316, 316};

        
        
        // Höhe und Breite der Slots werden evenfalls skaliert bei veränderten Bildschirmmaßen
        private static int I_slotW = 125;
        private static int I_slotH = 175;


        //VARIABLEN
        //#############################
        private int I_ID;

        private int I_X;
        private int I_Y;
        private int I_W;
        private int I_H;

        private double skalierung;
        private int verschiebung;

        private bool B_besetzt;
        private int I_karteID;

        private Texture2D T2D_auswahlS;
        private Texture2D T2D_auswahlW;

        private Karte karte; 

        // Alle Kartenslots auf der Spielmatte, weiße und rote Seite getrennt
        private List<Kartenslot> L_slotsWeiß;
        private List<Kartenslot> L_slotsRot;

        private List<Texture2D> Lt2d_karten;
        

        private SpriteBatch spriteBatch;

        private KartenAnzeige O_kartenAnzeige;
        private Aktionen O_aktionen;


        private MouseState lastState = Mouse.GetState();
        //#############################



        //#######################################
        public Kartenslot() { }

        // Die Kartenslots der weißen und roten Seite werden mit den selben ID's hoch gezählt
        //#######################################
        public Kartenslot(int id, double skalierung,int verschiebung,char farbe)
        {
            this.I_ID = id;

            if (farbe == 'w')
            {
                this.I_X = (int)(Ai_slotX_weiß[id] * skalierung) + verschiebung;
                this.I_Y = (int)(Ai_slotY_weiß[id] * skalierung);
            }
            else
            {
                this.I_X = (int)(Ai_slotX_rot[id] * skalierung) + verschiebung;
                this.I_Y = (int)(Ai_slotY_rot[id] * skalierung);
            }

            this.I_W = (int)(I_slotW * skalierung);
            this.I_H = (int)(I_slotH * skalierung);

            this.B_besetzt = false;
            this.I_karteID = 264;

        }
        //#######################################




        // Ein Kartenslot wird anhand der Nummer under Farbe ausgegeben
        //###########################################################
        public Kartenslot Get_Kartenslot(int slot,char farbe)
        {
            Kartenslot kartenslot;

            if (farbe == 'w')
            {
                kartenslot = L_slotsWeiß[slot];
            }
            else
            {
                kartenslot = L_slotsRot[slot];
            }

            return kartenslot;
        }
        //###########################################################




        // Die Karte die sich in einem bestimmten Slot befindet wird ausgegeben
        // Dazu wird die Slot Nummer benötigt
        //###########################################################
        public Karte Get_Karte(int slot)
        {
            return L_slotsWeiß[slot].karte;
        }
        //###########################################################




        // Alle 14 Slot Objekte für eine Spielfeldseite werden erstellt
        // Sie werden in einer Liste untergebracht
        //###########################################################
        public void Slots_Erstellen(double skalierung, int verschiebung, List<Texture2D>list, SpriteBatch spriteBatch)
        {
            Lt2d_karten = list;
            this.spriteBatch = spriteBatch;

            this.skalierung = skalierung;
            this.verschiebung = verschiebung;

            L_slotsWeiß = new List<Kartenslot>();
            L_slotsRot = new List<Kartenslot>();

            for(int i = 0; i < 14; i++)
            {
                L_slotsWeiß.Add(new Kartenslot(i, skalierung, verschiebung, 'w'));
                L_slotsRot.Add(new Kartenslot(i, skalierung, verschiebung, 'r'));
            }

            O_kartenAnzeige = new KartenAnzeige(spriteBatch, skalierung, list);
        }
        //###########################################################





        // In einen leeren Slot wird eine Karte gelegt
        // oder eine vorhandene Karte wird durch eine andere ersetzt
        //###########################################################
        public void Slot_Ändern(int slotID,Karte karte,char farbe)
        {
            if (farbe == 'w')
            {
                L_slotsWeiß[slotID].I_karteID = karte.I_ID;
                L_slotsWeiß[slotID].karte = karte;
                L_slotsWeiß[slotID].B_besetzt = true;
            }
            else
            {
                L_slotsRot[slotID].I_karteID = karte.I_ID;
                L_slotsRot[slotID].karte = karte;
                L_slotsRot[slotID].B_besetzt = true;
            }
        }
        //###########################################################




        // Eine Überladung der Funktion um einen Slot Inhalt zu ändern
        // Diese nimmt anstatt einer Karte lediglich eine ID an
        // Das wird benötigt um den Sprite für einen Leeren Slot oder eine Kartenrückseite einzufügen 
        //###########################################################
        public void Slot_Ändern(int slotID, int id, char farbe)
        {
            if (farbe == 'w')
            {
                L_slotsWeiß[slotID].I_karteID = id;
                L_slotsWeiß[slotID].B_besetzt = true;
            }
            else
            {
                L_slotsRot[slotID].I_karteID = id;
                L_slotsRot[slotID].B_besetzt = true;
            }
        }
        //###########################################################





        // Für jeden Slot auf dem Spielfeld soll geprüft werden, ob die Maus drüber ist
        // Wenn die Maus über einem Slot ist, soll die Karte darin angezeigt werden
        //###########################################################
        public void Slot_Hover(Point mousePoint)
        {
            for (int i = 0; i < L_slotsWeiß.Count; i++)
            {

                if (L_slotsWeiß[i].Slot_Position().Contains(mousePoint) && i>=1 && i<=6)
                {
                    O_kartenAnzeige.Set_AnzeigeID(L_slotsWeiß[i].I_karteID);

                    MouseState newState = Mouse.GetState();

                    lastState = newState;
                    break;
                }
                else if (L_slotsRot[i].Slot_Position().Contains(mousePoint))
                {
                    O_kartenAnzeige.Set_AnzeigeID(L_slotsRot[i].I_karteID);
                    break;
                }
                else if (O_aktionen!=null && O_aktionen.Hover(mousePoint))
                {
                    O_kartenAnzeige.Set_AnzeigeID(L_slotsWeiß[1].I_karteID);
                }
                else
                {
                    O_kartenAnzeige.Set_AnzeigeID(0);
                }
            }
        }
        //###########################################################




        // Die Slots werden überprüft ob die Maus darüber ist
        // Die Slots werden auf bestimmte Kriterien geprüft um zu beurteilen,
        // ob und welche Karten in den Slot gelegt werden und wie
        //###########################################################
        public int Slot_Hover(Point mousePoint,Karte karte,bool energie)
        {
            for (int i = 0; i < L_slotsWeiß.Count; i++)
            {

                if (L_slotsWeiß[i].Slot_Position().Contains(mousePoint) && i >= 1 && i <= 6)
                {

                    if (Spielzug.B_ziehen == false)
                    {
                        if (L_slotsWeiß[i].B_besetzt == false && energie == false && karte.S_vorentwicklung == "")
                        {
                            if (i > 0 && i < 7)
                            {
                                Slot_Ändern(i, karte, 'w');

                                if (i == 1)
                                {
                                    O_aktionen = new Aktionen(L_slotsWeiß[1].karte, (Ai_slotX_weiß[1] + I_slotW + verschiebung), Ai_slotY_weiß[1], skalierung);
                                }

                                return i;
                            }

                        }
                        else if (L_slotsWeiß[i].B_besetzt == true && energie == true)
                        {

                            if (Spielzug.B_energie == true)
                            {
                                Spielzug.B_energie = false;
                                return i;
                            }
                            else
                            {
                                Textbox.Infobox("Es kann nur eine Energiekarte pro Zug gespielt werden");
                            }

                        }
                        else if (L_slotsWeiß[i].B_besetzt == true && Get_Karte(i).S_kartenname == karte.S_vorentwicklung)
                        {
                            karte.Ls_energieAngelegt = Get_Karte(i).Ls_energieAngelegt;
                            Get_Karte(i).Ls_energieAngelegt = null;
                            Slot_Ändern(i, karte, 'w');
                            return i;
                        }
                    }
                    else
                    {
                        Textbox.Infobox("Bitte zuerst eine Karte ziehen");
                    }

                }
                else
                {
                    
                }

            }
            return -1;
        }
        //###########################################################



        // Die Koordinaten eines einzelnen Slots am Bildschirm werden ausgegeben
        //###########################################################
        public Rectangle Slot_Position()
        {
            Rectangle r = new Rectangle(I_X, I_Y, I_W, I_H);
            return r;
        }
        //###########################################################




        //###########################################################
        public void Draw(Texture2D auswahlS, Texture2D auswahlW, SpriteFont font)
        {
            // Die Kartenfelder nach ID. Rote und weiße Seite getrennt
            for (int i = 0; i < L_slotsWeiß.Count; i++)
            {

                if (i >= 8 && i <= 13 && L_slotsWeiß[i].B_besetzt ==true) 
                {
                    spriteBatch.Draw(Lt2d_karten[0], L_slotsWeiß[i].Slot_Position(), Color.White);
                }
                else
                {
                    spriteBatch.Draw(Lt2d_karten[L_slotsWeiß[i].I_karteID], L_slotsWeiß[i].Slot_Position(), Color.White);
                }

                spriteBatch.Draw(Lt2d_karten[L_slotsRot[i].I_karteID], L_slotsRot[i].Slot_Position(), Color.White);
            }


            if (O_aktionen != null)
            {
                O_aktionen.Draw(auswahlS,auswahlW,spriteBatch,font);
            }

            O_kartenAnzeige.Draw();
        }
        //###########################################################
    }
}
