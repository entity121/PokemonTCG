using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.Spielfeld
{
    public static class KartenAnzeige
    {
        // VARIABLEN
        //#############################
        private static int I_anzeigeWDEFAULT = (1920 - 1080) / 2;
        public static List<Texture2D> Lt2d_karten = new List<Texture2D>();
        private static int ID_hand;
        private static int ID_slot;
        //#############################

        

        // Es kann immer nur eine Karte angezeigt werden.
        // Diese Karte wird anhand der Karten ID in einer Variablen definiert
        //#################################################
        public static void Set_AnzeigeID(string herkunft,int ID)                 
        {

            if(herkunft == "hand")
            {
                ID_hand = ID;
            }
            else if(herkunft == "slot")
            {
                ID_slot = ID;
            }

        }
        //#################################################




        //#################################################
        private static Rectangle Position(double skalierung)
        {
           return new Rectangle(0, (int)(100 * skalierung), (int)(I_anzeigeWDEFAULT * skalierung), (int)((I_anzeigeWDEFAULT * 1.4) * skalierung));
        }
        //#################################################




        // Anzeige darstellen
        //#################################################
        public static void Draw(double scale,SpriteBatch spriteBatch)
        {
            if (ID_hand > 0)
            {
                spriteBatch.Draw(Lt2d_karten[ID_hand], Position(scale), Color.White);
            }
            else if(ID_slot > 0 && ID_slot < 264)
            {
                spriteBatch.Draw(Lt2d_karten[ID_slot], Position(scale), Color.White);
            }
        }
        //#################################################
    }
}
