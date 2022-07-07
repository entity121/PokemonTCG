using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace PokemonTCG
{
    public class Game1 : Game
    {
        //#############################
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Sehr praktische Arrays mit verschiedenen Bildschirmgrößen
        private static int[] Ai_fensterX = new int[] { 3840, 2560, 2560, 1920, 1366, 1280, 1280 };
        private static int[] Ai_fensterY = new int[] { 2160, 1440, 1080, 1080, 768, 1024, 720 };
        private int I_selectedX = 3;
        private int I_selectedY = 3;

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



        public System.Drawing.Color c;
        public int b;
        public int r;
        public int g;

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
            spriteBatch.Draw(T2D_spielmatte,new Rectangle((Ai_fensterX[I_selectedX] - Ai_fensterY[I_selectedY]) /2,0, Ai_fensterY[I_selectedY], Ai_fensterY[I_selectedY]),Color.White);
            spriteBatch.Draw(T2D_TestKarte, new Rectangle(165+((Ai_fensterX[I_selectedX] - Ai_fensterY[I_selectedY]) / 2),842,124,175), Color.White);
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
        //###########################################################
    }
}
