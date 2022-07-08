using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using PokemonTCG.Spielfeld;
using System.Windows;


namespace PokemonTCG
{
    public class Game1 : Game
    {
        //#############################
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Maße der Spielmatte ohne Skalierung
        private static int I_SpielmatteDefault = 1080;

        private int I_bildschirm_H = (int)SystemParameters.PrimaryScreenHeight;
        private int I_bildschirm_W = (int)SystemParameters.PrimaryScreenWidth;

        // Der Wert um den die Spielmatte vom linken Rand verschoben wird
        private int I_verschiebungMatte;

        // Der Wert um den alle Sprites etc. skaliert werden
        private double D_skalierung;

        private Texture2D T2D_hintergrund;
        private Texture2D T2D_spielmatte;
        private Texture2D T2D_TestKarte;

        // Inhalt: 0 = Kartenrücken , 1-263 = Pokemon KArten nach ID , 264 = Leerer Slot
        private List<Texture2D> L_T2D_karten = new List<Texture2D>();

        // Alle Kartenslots auf der Spielmatte, weiße und rote Seite getrennt
        private List<Kartenslot> L_slotsWeiß;
        private List<Kartenslot> L_slotsRot;

        //#############################



        //#######################################
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = I_bildschirm_W;
            graphics.PreferredBackBufferHeight = I_bildschirm_H;
            graphics.ApplyChanges();
            graphics.ToggleFullScreen();

            I_verschiebungMatte = (I_bildschirm_W - I_bildschirm_H) / 2;
            D_skalierung = (double)I_bildschirm_H / (double)I_SpielmatteDefault;

            Kartenslot kartenslot = new Kartenslot();
            L_slotsWeiß = kartenslot.Slots_Erstellen(D_skalierung, I_verschiebungMatte, 'w');
            L_slotsRot = kartenslot.Slots_Erstellen(D_skalierung, I_verschiebungMatte, 'r');

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        //#######################################




        //###########################################################
        protected override void Initialize()
        {
     

            base.Initialize();
        }
        //###########################################################




        //###########################################################
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            T2D_hintergrund = Content.Load<Texture2D>("Hintergrund");
            T2D_spielmatte = Content.Load<Texture2D>("Spielfeld_leer");

            // Alle Karten Sprites werden in die Liste geladen
            for(int i = 0; i <= 263; i++)
            {L_T2D_karten.Add(Content.Load<Texture2D>(i.ToString()));}
            // Der leere Slot kommt auch in die Liste
            L_T2D_karten.Add(Content.Load<Texture2D>("Kartenslot"));


        }
        //###########################################################




        //###########################################################
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();






            base.Update(gameTime);
        }
        //###########################################################




        //###########################################################
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();


            spriteBatch.Draw(T2D_hintergrund, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(T2D_spielmatte,new Rectangle(I_verschiebungMatte,0, I_bildschirm_H, I_bildschirm_H),Color.White);

            for(int i = 0; i < L_slotsWeiß.Count; i++)
            {
                spriteBatch.Draw(L_T2D_karten[L_slotsWeiß[i].Karte_im_Slot()], L_slotsWeiß[i].Slot_Position(), Color.White);
                spriteBatch.Draw(L_T2D_karten[L_slotsRot[i].Karte_im_Slot()], L_slotsRot[i].Slot_Position(), Color.White);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
        //###########################################################
    }
}
