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
        public static int ID_gezogen;
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
            else if(herkunft == "ziehen")
            {
                ID_gezogen = ID;
            }

        }
        //#################################################




        //#################################################
        private static Rectangle Position(double skalierung)
        {
            if(ID_gezogen == 0)
            {
                return new Rectangle(0, (int)(100 * skalierung), (int)(I_anzeigeWDEFAULT * skalierung), (int)((I_anzeigeWDEFAULT * 1.4) * skalierung));
            }
            else
            {
                return new Rectangle((int)((1920/2)-(I_anzeigeWDEFAULT/2)*skalierung), (int)((1080/4)*skalierung), (int)(I_anzeigeWDEFAULT * skalierung), (int)((I_anzeigeWDEFAULT * 1.4) * skalierung));
            }
           
        }
        //#################################################




        // Anzeige darstellen
        //#################################################
        public static void Draw(double scale,SpriteBatch spriteBatch)
        {
            if (ID_hand > 0)
            {
                spriteBatch.Draw(Lt2d_karten[ID_hand], Position(scale), Color.White);
                if(Spielzug.B_spielerAktiv == false)
                {
                    ID_hand = 0;
                }
            }
            else if(ID_slot > 0 && ID_slot < 264)
            {
                spriteBatch.Draw(Lt2d_karten[ID_slot], Position(scale), Color.White);
                if (Spielzug.B_spielerAktiv == false)
                {
                    ID_slot = 0;
                }
            }
            else if (ID_gezogen > 0)
            {
                spriteBatch.Draw(Lt2d_karten[ID_gezogen], Position(scale), Color.White);
                if (Spielzug.B_spielerAktiv == false)
                {
                    ID_gezogen = 0;
                }
            }
        }
        //#################################################
    }
}
