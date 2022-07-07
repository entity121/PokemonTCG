using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using PokemonTCG.Spielfeld;



namespace PokemonTCG
{
    public class Game1 : Game
    {
        //#############################
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private static int I_SpielmatteDefault = 1080;
        // Sehr praktische Arrays mit verschiedenen Bildschirmgrößen
        private static int[] Ai_fensterX = new int[] { 3840, 2560, 2560, 1920, 1366, 1280, 1280 };
        private static int[] Ai_fensterY = new int[] { 2160, 1440, 1080, 1080, 768, 1024, 720 };

        private int I_selectedX = 6;// Muss ebenfalls dynamisch ermittelt werden, aber erstmal Okay
        private int I_selectedY = 6;

        // Der Wert um den die Spielmatte vom linken Rand verschoben wird
        private int I_verschiebungMatte;

        private double D_skalierung;

        private Texture2D T2D_hintergrund;
        private Texture2D T2D_spielmatte;

        private Texture2D T2D_TestKarte;

        private List<Texture2D> T2D_karten;
        //#############################



        //#######################################
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Ai_fensterX[I_selectedX];
            graphics.PreferredBackBufferHeight = Ai_fensterY[I_selectedY];
            graphics.ApplyChanges();
            //graphics.ToggleFullScreen();


            I_verschiebungMatte = (Ai_fensterX[I_selectedX] - Ai_fensterY[I_selectedY]) / 2;
            D_skalierung = Ai_fensterY[I_selectedY] / I_SpielmatteDefault;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        //#######################################




        //###########################################################
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        //###########################################################




        //###########################################################
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            T2D_hintergrund = Content.Load<Texture2D>("Hintergrund");
            T2D_spielmatte = Content.Load<Texture2D>("Spielfeld");
            T2D_TestKarte = Content.Load<Texture2D>("0");
        }
        //###########################################################



        Kartenslot slot1;
        Kartenslot slot2;
        //###########################################################
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            if (slot1 == null)
            {
                slot1 = new Kartenslot(0, D_skalierung,I_verschiebungMatte);
                slot2 = new Kartenslot(1, D_skalierung, I_verschiebungMatte);
                slot1.test(D_skalierung.ToString());
            }
            


            base.Update(gameTime);
        }
        //###########################################################




        //###########################################################
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();


            spriteBatch.Draw(T2D_hintergrund, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(T2D_spielmatte,new Rectangle(I_verschiebungMatte,0, Ai_fensterY[I_selectedY], Ai_fensterY[I_selectedY]),Color.White);
            spriteBatch.Draw(T2D_TestKarte,slot1.Slot_Platzieren(), Color.White);
            spriteBatch.Draw(T2D_TestKarte, slot2.Slot_Platzieren(), Color.White);
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
        //###########################################################
    }
}
