using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MPS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private formMain form;
        private MouseState mouseStateCurrent, mouseStatePrevious;
        private KeyboardState keyStateCurrent, keyStatePrevious;
        private float zoomStep, panStep; // Snelheid van SpriteManager.Camera
        private Vector2 oorsprong; // Beginpositie van SpriteManager.Camera
        private bool cameraSlepen;
        private bool verbinden;
        private Vector2[] verbindingslijn;

        public Game1(formMain form)
        {
            this.form = form;
            System.Windows.Forms.Form xnaWindow = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle((this.Window.Handle));
            xnaWindow.GotFocus += new EventHandler(xnaWindow_GotFocus);
            form.Panel.Resize += new EventHandler(Panel_Resize);
            graphics = new GraphicsDeviceManager(this);
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            graphics.PreferredBackBufferWidth = form.Panel.Width;
            graphics.PreferredBackBufferHeight = form.Panel.Height;
            Content.RootDirectory = "Content";
        }

        void Panel_Resize(object sender, EventArgs e)
        {
            graphics.PreferredBackBufferWidth = form.Panel.Width;
            graphics.PreferredBackBufferHeight = form.Panel.Height;
            SpriteManager.SchermMidden = new Vector2(form.Panel.Width / 2, form.Panel.Height / 2);
            graphics.ApplyChanges();
        }

        void xnaWindow_GotFocus(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Form)sender).Visible = false;
            form.TopMost = false;
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = form.Panel.Handle;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteManager.Camera = new XNA2dCamera(graphics);
            SpriteManager.Camera.Zoom = new Vector2(0.5F);
            zoomStep = 0.01F;
            panStep = 5;
            oorsprong = SpriteManager.Camera.Position;
            SpriteManager.SchermMiddenBegin = new Vector2(form.Panel.Width / 2, form.Panel.Height / 2);
            SpriteManager.SchermMidden = SpriteManager.SchermMiddenBegin;

            cameraSlepen = false;
            verbinden = false;
            verbindingslijn = null;

            Mouse.WindowHandle = IntPtr.Zero;
            Mouse.WindowHandle = form.Panel.Handle;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Textures.Load(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Netwerk.Test();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            System.Threading.Thread.Sleep(10); // Geef je CPU wat rust =]

            form.label1.Text = String.Format("Camera.Zoom: {0}\nCamera.Position: {1}\nInfecties.Count: {2}\ngeselecteerde: {3}",
                SpriteManager.Camera.Zoom.X, SpriteManager.Camera.Position,
                SpriteManager.Geselecteerde != null ? SpriteManager.Geselecteerde.Infecties.Count : 0,
                SpriteManager.Geselecteerde != null ? SpriteManager.Geselecteerde.ToString() : string.Empty);

            UpdateInput();
            Netwerk.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            mouseStateCurrent = Mouse.GetState();
            keyStateCurrent = Keyboard.GetState();

            // Apparaten verbinden
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && keyStateCurrent.IsKeyDown(Keys.LeftControl) || verbinden)
            {
                if (!verbinden)
                    SpriteManager.Click(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y));

                if (SpriteManager.Geselecteerde != null)
                {
                    verbindingslijn = new Vector2[] {SpriteManager.Geselecteerde.Positie, new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y)};
                }
                verbinden = true;
            }
            // Camera slepen
            else if (mouseStateCurrent.LeftButton == ButtonState.Pressed && keyStateCurrent.IsKeyDown(Keys.Space) || cameraSlepen)
            {
                SpriteManager.Camera.Position += (new Vector2(mouseStatePrevious.X, mouseStatePrevious.Y) - new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y))
                    / SpriteManager.Camera.Zoom.X;
                cameraSlepen = true;
            }
            // Selecteren
            else if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released &&
                !form.MenuOpened)
            {
                SpriteManager.Click(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y));
            }
            // Object slepen
            else if (mouseStateCurrent.LeftButton == ButtonState.Pressed &&
                !cameraSlepen &&
                !form.MenuOpened)
            {
                if (SpriteManager.Geselecteerde != null)
                {
                    SpriteManager.Geselecteerde.Positie -= (new Vector2(mouseStatePrevious.X, mouseStatePrevious.Y)
                        - new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y)) / SpriteManager.Camera.Zoom.X;
                }
            }

            // Muisknop losgelaten
            if (mouseStateCurrent.LeftButton == ButtonState.Released && mouseStatePrevious.LeftButton == ButtonState.Pressed)
            {
                cameraSlepen = false;
                verbinden = false;
                if (verbindingslijn != null)
                {
                    Apparaat app = SpriteManager.Click(verbindingslijn[1], false);
                    Netwerk.Verbind(app, SpriteManager.Geselecteerde);
                }
                verbindingslijn = null;
            }

            // Zoom
            if (keyStateCurrent.IsKeyDown(Keys.OemPlus))
                SpriteManager.Camera.Zoom += new Vector2(zoomStep);
            if (keyStateCurrent.IsKeyDown(Keys.OemMinus))
                SpriteManager.Camera.Zoom -= new Vector2(zoomStep);

            // Pan
            float pan = panStep / SpriteManager.Camera.Zoom.X;
            if (keyStateCurrent.IsKeyDown(Keys.LeftShift))
                pan *= 3;
            if (keyStateCurrent.IsKeyDown(Keys.W))
                SpriteManager.Camera.Position -= new Vector2(0, pan);
            if (keyStateCurrent.IsKeyDown(Keys.S))
                SpriteManager.Camera.Position += new Vector2(0, pan);
            if (keyStateCurrent.IsKeyDown(Keys.A))
                SpriteManager.Camera.Position -= new Vector2(pan, 0);
            if (keyStateCurrent.IsKeyDown(Keys.D))
                SpriteManager.Camera.Position += new Vector2(pan, 0);

            // Snelheid
            if (keyStateCurrent.IsKeyDown(Keys.OemCloseBrackets))
                Netwerk.Timer -= Netwerk.Timer > 500 ? 100 : 0;
            if (keyStateCurrent.IsKeyDown(Keys.OemOpenBrackets))
                Netwerk.Timer += Netwerk.Timer < 5000 ? 100 : 0;

            // Reset
            if (keyStateCurrent.IsKeyDown(Keys.D0))
            {
                SpriteManager.Camera.Position = oorsprong;
                SpriteManager.Camera.Rotation = 0;
                Netwerk.Test();
            }

            // Verander muis
            if (form.MenuOpened)
            {
                form.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else if ((keyStateCurrent.IsKeyDown(Keys.Space) &&
                mouseStateCurrent.X >= 0 && mouseStateCurrent.X <= SpriteManager.SchermMidden.X * 2 &&
                mouseStateCurrent.Y >= 0 && mouseStateCurrent.Y <= SpriteManager.SchermMidden.Y * 2) || cameraSlepen)
            {
                form.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            }
            else if (SpriteManager.Click(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y), false) != null)
            {
                form.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            else
            {
                form.Cursor = System.Windows.Forms.Cursors.Default;
            }

            mouseStatePrevious = mouseStateCurrent;
            keyStatePrevious = keyStateCurrent;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState, SpriteManager.Camera.ViewTransformationMatrix());
            SpriteManager.DrawAll(spriteBatch, graphics.GraphicsDevice);
            if (verbindingslijn != null)
                SpriteManager.DrawVerbindingslijn(spriteBatch, graphics.GraphicsDevice, verbindingslijn[0], verbindingslijn[1]);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}