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


        // Inhalt: 0 = Kartenrücken , 1-263 = Pokemon Karten nach ID , 264 = Leerer Slot
        private List<Texture2D> Lt2d_karten = new List<Texture2D>();

        Kartenslot slot = new Kartenslot();

        // Die Objekte für den Spieler und den Gegner
        Spieler A;
        

        //#############################



        //#######################################
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = I_bildschirm_W;
            graphics.PreferredBackBufferHeight = I_bildschirm_H;
            graphics.ApplyChanges();
            //graphics.ToggleFullScreen();


            I_verschiebungMatte = (I_bildschirm_W - I_bildschirm_H) / 2;
            D_skalierung = (double)I_bildschirm_H / (double)I_SpielmatteDefault;


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


            // Alle Karten Sprites werden in die Liste geladen
            for(int i = 0; i <= 263; i++)
            {Lt2d_karten.Add(Content.Load<Texture2D>(i.ToString()));}
            // Der leere Slot kommt auch in die Liste
            Lt2d_karten.Add(Content.Load<Texture2D>("Kartenslot"));


            // Spielfeld bestehend aus Hintergrund und Matte
            T2D_hintergrund = Content.Load<Texture2D>("Hintergrund");
            T2D_spielmatte = Content.Load<Texture2D>("Spielfeld_leer");

            // Die einzelnen Kartenslots, die auf dem Spielfeld platziert sind
            slot.Slots_Erstellen(D_skalierung,I_verschiebungMatte,Lt2d_karten,spriteBatch);

            // Spieler und Gegner
            A = new Spieler(6, D_skalierung, spriteBatch, Lt2d_karten, Content.Load<Texture2D>("Holz"));
            // TODO : Gegner

            Lt2d_karten = null;
        }
        //###########################################################




        //###########################################################
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            if (A.Get_Hand().Hand_Zeigen().Length == 0)
            {
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
                A.Karte_Ziehen();
            }


            A.Get_Hand().Brett_Hover();
            

            base.Update(gameTime);
        }
        //###########################################################




        //###########################################################
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();


            // Hintergrund und das Spielfeld
            spriteBatch.Draw(T2D_hintergrund, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(T2D_spielmatte,new Rectangle(I_verschiebungMatte,0, I_bildschirm_H, I_bildschirm_H),Color.White);

            slot.Draw();

            A.Draw_Hand();          
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
        //###########################################################




    }
}
