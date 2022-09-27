using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using PokemonTCG.Spielfeld;
using System.Windows;
using System.Threading;
using PokemonTCG.Textboxen;
using PokemonTCG.ComputerGegner;
using PokemonTCG.MenschlicherSpieler;


namespace PokemonTCG
{
    public class Game1 : Game
    {
        //#############################
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Maße der Spielmatte ohne Skalierung
        private static int I_SpielmatteDefault = 1080;

        private SpriteFont font;

        private int I_bildschirm_H = (int)SystemParameters.PrimaryScreenHeight;
        private int I_bildschirm_W = (int)SystemParameters.PrimaryScreenWidth;

        // Der Wert um den die Spielmatte vom linken Rand verschoben wird
        private int I_verschiebungMatte;

        // Der Wert um den alle Sprites etc. skaliert werden
        private double D_skalierung;

        private Texture2D T2D_hintergrund;
        private Texture2D T2D_spielmatte;
        private Texture2D T2D_tranzparenz_weiß;

        private Texture2D T2D_holzAktionen;
        private Texture2D T2D_auswahlS;
        private Texture2D T2D_auswahlW;

        private Texture2D[] T2D_ja_nein = new Texture2D[4];
        private Texture2D[] T2D_confirm = new Texture2D[4];

        // Inhalt: 0 = Kartenrücken , 1-263 = Pokemon Karten nach ID , 264 = Leerer Slot
        private List<Texture2D> Lt2d_karten = new List<Texture2D>();
        private List<Texture2D> Lt2d_marken = new List<Texture2D>();
        private List<Texture2D> Lt2d_elemente = new List<Texture2D>();

        BankAuswahl O_bankAuswahl;

        Kartenslot slot = new Kartenslot();

        // Die Objekte für den Spieler und den Gegner
        Spieler SPIELER;
        KI GEGNER;

        //#############################
        //
        //
        //
        //
        //
        //
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

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
           
        }
        //#######################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        protected override void Initialize()
        {
            base.Initialize();
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Münze.spritebatch = spriteBatch;

            // Alle Karten Sprites werden in die Liste geladen
            for(int i = 0; i <= 263; i++)
            {Lt2d_karten.Add(Content.Load<Texture2D>("Karten/"+i));}
            // Der leere Slot kommt auch in die Liste
            Lt2d_karten.Add(Content.Load<Texture2D>("Spielfeld/Kartenslot"));

            KartenAnzeige.Lt2d_karten = Lt2d_karten;

            // Spielfeld bestehend aus Hintergrund und Matte
            T2D_hintergrund = Content.Load<Texture2D>("Spielfeld/Hintergrund");
            T2D_spielmatte = Content.Load<Texture2D>("Spielfeld/Spielfeld_leer");
            T2D_tranzparenz_weiß = Content.Load<Texture2D>("Spielfeld/Tranzparenz_Weiß");

            T2D_holzAktionen = Content.Load<Texture2D>("Bretter/Brett_aktionen");
            T2D_auswahlS = Content.Load<Texture2D>("Bretter/Brett_aktionen_schw");
            T2D_auswahlW = Content.Load<Texture2D>("Bretter/Brett_aktionen_weiß");

            // Für die selbst erstellte Confirm Box
            T2D_ja_nein[0] = Content.Load<Texture2D>("Buttons/JA_S");
            T2D_ja_nein[1] = Content.Load<Texture2D>("Buttons/JA_W");
            T2D_ja_nein[2] = Content.Load<Texture2D>("Buttons/NEIN_S");
            T2D_ja_nein[3] = Content.Load<Texture2D>("Buttons/NEIN_W");

            T2D_confirm[0] = Content.Load<Texture2D>("Buttons/OK_S");
            T2D_confirm[1] = Content.Load<Texture2D>("Buttons/OK_W");
            T2D_confirm[2] = Content.Load<Texture2D>("Buttons/Abbruch_S");
            T2D_confirm[3] = Content.Load<Texture2D>("Buttons/Abbruch_W");


            // Für die selbst erstellte Schriftart
            Schriftzeichen_Laden();


            // Die Münze mit Vorder- und Rückseite
            Münze.Lt2d_münzen.Add(Content.Load<Texture2D>("Coin Kopf"));
            Münze.Lt2d_münzen.Add(Content.Load<Texture2D>("Coin Nicht-Kopf"));

            Münze.T2D_brettKlein = Content.Load<Texture2D>("Bretter/Brett_klein");
            Textbox.T2D_textbox = Content.Load<Texture2D>("Bretter/Brett_textbox");

            ElementAuswahl.T2D_brett = Content.Load<Texture2D>("Bretter/Holz");

            string[] elemente = new string[] { "Elektro_Symbol", "Farblos_Symbol", "Feuer_Symbol", "Kampf_Symbol", "Pflanze_Symbol", "Psycho_Symbol", "Wasser_Symbol", "Verblasst" };
            for(int i = 0; i < elemente.Length; i++)
            {
                string s = "ElementSymbole/" + elemente[i];
                Lt2d_elemente.Add(Content.Load<Texture2D>(s));
            }

            font = Content.Load<SpriteFont>("File");
            

            // Die einzelnen Kartenslots, die auf dem Spielfeld platziert sind
            slot.Slots_Erstellen(D_skalierung, I_verschiebungMatte, Lt2d_karten, spriteBatch);


            // Spieler und Gegner
            SPIELER = new Spieler(6, D_skalierung, spriteBatch, Lt2d_karten, Content.Load<Texture2D>("Bretter/Holz"),slot);
            SPIELER.Starthand();
            GEGNER = new KI(slot);

        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private void Schriftzeichen_Laden()
        {
            string order = "?!.-+0123456789AÄaäBbCcDdEeFfGgHhIiJjKkLlMmNnOÖoöPpQqRrSsTtUÜuüVvWwXxYyZz";
            string kl = "abcdefghijklmnopqrstuvwxyzäöü";

            char[] Arr = order.ToCharArray();

            for (int i = 0; i < Arr.Length; i++)
            {

                string file;
                if (Arr[i] != '?')
                {
                    file = "Schriftzeichen/" + Arr[i];
                    if (kl.Contains(Arr[i]))
                    {
                        file += "_";
                    }
                }
                else
                {
                    file = "Schriftzeichen/Frgz";
                }


                Texture2D tx = Content.Load<Texture2D>(file);

                Buchstabe b = new Buchstabe(tx, Arr[i]);

                Textbox.Lo_buchstaben.Add(b);
            }
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        Thread thr;
        //###########################################################
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (thr==null)
            {
                thr = new Thread(Update_Funktionen);
                thr.Start();
            }

            if (thr.IsAlive == false)
            {
                thr = null;
            }

            base.Update(gameTime);
        }
        //###########################################################
        //
        //
        //
        //
        //
        //
        //###########################################################
        private void Update_Funktionen()
        {
            // Ein Münzwurf soll bestimmen, welcher Spieler anfangen darf
            if(Spielzug.B_spielStart == true) 
            {
                if(Spielzug.B_spielerAktiv == true)
                {
                    Spielzug.Spielstart();
                }
                else
                {
                    Textbox.Infobox("Bitte setze ein aktives Basis Pokemon");
                    SPIELER.Aktives_Pokemon_Setzen();
                }
            }


            // Eine Globale Variable bestimmt, ob der Spieler ...  
            if(Spielzug.B_spielerZug == true) 
            {            
                SPIELER. Spieler_Zug();
            }
            // ... oder der Gegner am Zug
            else
            {
                GEGNER.Gegner_Zug();
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
        protected override void Draw(GameTime gameTime)
        {
                         
            //GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            // Hintergrund und das Spielfeld
            spriteBatch.Draw(T2D_hintergrund, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(T2D_spielmatte, new Rectangle(I_verschiebungMatte, 0, I_bildschirm_H, I_bildschirm_H), Color.White);

            slot.Draw();

            if (SPIELER.O_aktionen != null)
            {
                SPIELER.O_aktionen.Draw(T2D_auswahlS, T2D_auswahlW, spriteBatch, font, Lt2d_elemente);
            }

            SPIELER.O_hand.Draw(SPIELER.I_karteHaltenPosition);


            if (KartenAnzeige.ID_gezogen != 0)
            {
                spriteBatch.Draw(T2D_tranzparenz_weiß, new Vector2(0, 0), Color.White);
            }
            KartenAnzeige.Draw(D_skalierung, spriteBatch);



            if (Münze.B_münzwurf == true)
            {
                spriteBatch.Draw(Münze.T2D_brettKlein, new Rectangle((int)(Münze.I_x * D_skalierung), (int)(Münze.I_y * D_skalierung), (int)(Münze.I_w * D_skalierung), (int)(Münze.I_h * D_skalierung)), Color.White);
                spriteBatch.Draw(Münze.Lt2d_münzen[Münze.I_münzeSeite], new Rectangle((int)(Münze.I_x * D_skalierung), (int)(Münze.I_y * D_skalierung), (int)(Münze.I_w * D_skalierung), (int)(Münze.I_h * D_skalierung)), Color.White);
            }



            if(Textbox.B_textbox == true)
            {
                spriteBatch.Draw(T2D_tranzparenz_weiß, new Vector2(0, 0), Color.White);
                Textbox.Draw(spriteBatch, font, D_skalierung, T2D_ja_nein);
            }


            if(ElementAuswahl.B_wählen == true)
            {
                spriteBatch.Draw(T2D_tranzparenz_weiß, new Vector2(0, 0), Color.White);
                ElementAuswahl.Draw(spriteBatch,Lt2d_elemente,T2D_confirm);
            }


            spriteBatch.End();
            base.Draw(gameTime);
            
        }
        //###########################################################
        //
        //
        //
        //
        //
        //



    }
}
