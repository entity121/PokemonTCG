using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PokemonTCG.Spielfeld
{
    class KartenAnzeige
    {
        //#############################
        private static int I_anzeigeWDEFAULT = (1920 - 1080) / 2;
        private Rectangle R_karteAnzeige;
        private SpriteBatch spriteBatch;
        private List<Texture2D> Lt2d_karten;
        private int I_anzeigeID;
        //#############################


        // Ein Feld am linken Bildschirmrand, in dem Karten angezeigt werden sollen, die gehovert werden
        //#######################################
        public KartenAnzeige(SpriteBatch spriteBatch,double skalierung,List<Texture2D>list)
        {
            this.R_karteAnzeige = new Rectangle(0, (int)(100 * skalierung), (int)(I_anzeigeWDEFAULT * skalierung), (int)((I_anzeigeWDEFAULT * 1.4) * skalierung));
            this.spriteBatch = spriteBatch;
            this.Lt2d_karten = list;
            this.I_anzeigeID = 0;
        }
        //#######################################


    
        //#################################################
        public void Set_AnzeigeID(int ID)                 
        {
            this.I_anzeigeID = ID;
        }
        //#################################################



        //#################################################
        public void Draw()
        {
            if (I_anzeigeID != 0 && I_anzeigeID != 264)
            {
                spriteBatch.Draw(Lt2d_karten[I_anzeigeID], R_karteAnzeige, Color.White);
            }
        }
        //#################################################
    }
}
