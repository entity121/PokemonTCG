using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

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

        private bool B_besetzt;
        private int I_karte;

        // Alle Kartenslots auf der Spielmatte, weiße und rote Seite getrennt
        private List<Kartenslot> L_slotsWeiß;
        private List<Kartenslot> L_slotsRot;

        private SpriteBatch spriteBatch;
        private List<Texture2D> Lt2d_karten;
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
            this.I_karte = 264;
        }
        //#######################################



        //###########################################################
        public void Slots_Erstellen(double skalierung, int verschiebung, List<Texture2D>l,SpriteBatch s)
        {
            Lt2d_karten = l;
            spriteBatch = s;

            L_slotsWeiß = new List<Kartenslot>();
            L_slotsRot = new List<Kartenslot>();

            for(int i = 0; i < 14; i++)
            {
                L_slotsWeiß.Add(new Kartenslot(i, skalierung, verschiebung, 'w'));
                L_slotsRot.Add(new Kartenslot(i, skalierung, verschiebung, 'r'));
            }

        }
        //###########################################################



        //###########################################################
        public Rectangle Slot_Position()
        {
            Rectangle r = new Rectangle(I_X, I_Y, I_W, I_H);
            return r;
        }
        //###########################################################


        //###########################################################
        public int Karte_im_Slot()
        {
            return I_karte;
        }
        //###########################################################



        //###########################################################
        public void Draw()
        {
            // Die Kartenfelder nach ID. Rote und weiße Seite getrennt
            for (int i = 0; i < L_slotsWeiß.Count; i++)
            {
                spriteBatch.Draw(Lt2d_karten[L_slotsWeiß[i].Karte_im_Slot()], L_slotsWeiß[i].Slot_Position(), Color.White);
                spriteBatch.Draw(Lt2d_karten[L_slotsRot[i].Karte_im_Slot()], L_slotsRot[i].Slot_Position(), Color.White);
            }
        }
        //###########################################################
    }
}
