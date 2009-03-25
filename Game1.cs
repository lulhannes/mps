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
        private XNA2dCamera camera;
        private float zoomStep, panStep; // Snelheid van camera
        private Vector2 oorsprong; // Beginpositie van camera
        private Vector2 schermMiddenBegin, schermMidden; // Midden van scherm (nodig voor coordinaten)

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
            schermMidden = new Vector2(form.Panel.Width / 2, form.Panel.Height / 2);
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

            camera = new XNA2dCamera(graphics);
            camera.Zoom = new Vector2(0.5F);
            zoomStep = 0.01F;
            panStep = 5;
            oorsprong = camera.Position;
            schermMiddenBegin = new Vector2(form.Panel.Width / 2, form.Panel.Height / 2);
            schermMidden = schermMiddenBegin;

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
            System.Threading.Thread.Sleep(5); // Geef je CPU wat rust =]

            form.label1.Text = String.Format("Camera.Zoom: {0}\nCamera.Position: {1}\nschermMidden: {2}\nschermPositie: {3}\nMuis.Position: {4}\nInfecties.Count: {5}",
                camera.Zoom.X, camera.Position, schermMidden, new Vector2(form.Left, form.Top),
                new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y) - schermMidden,
                SpriteManager.Geselecteerde != null ? SpriteManager.Geselecteerde.Infecties.Count : 0);

            UpdateInput();
            Netwerk.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            mouseStateCurrent = Mouse.GetState();
            keyStateCurrent = Keyboard.GetState();

            // Selecteren
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released
                && !keyStateCurrent.IsKeyDown(Keys.Space))
            {
                SpriteManager.Click(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y), schermMidden, schermMiddenBegin, camera.Position - oorsprong, camera.Zoom.X, true);
            }

            // Object slepen
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed
                && !keyStateCurrent.IsKeyDown(Keys.Space))
            {
                if (SpriteManager.Geselecteerde != null)
                {
                    SpriteManager.Geselecteerde.Positie -= (new Vector2(mouseStatePrevious.X, mouseStatePrevious.Y)
                        - new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y)) / camera.Zoom.X;
                }
            }

            // Camera slepen
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && keyStateCurrent.IsKeyDown(Keys.Space))
            {
                camera.Position += (new Vector2(mouseStatePrevious.X, mouseStatePrevious.Y) - new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y))
                    / camera.Zoom.X;
            }

            // Zoom
            if (keyStateCurrent.IsKeyDown(Keys.OemPlus))
                camera.Zoom += new Vector2(zoomStep);
            if (keyStateCurrent.IsKeyDown(Keys.OemMinus))
                camera.Zoom -= new Vector2(zoomStep);

            // Pan
            float pan = panStep / camera.Zoom.X;
            if (keyStateCurrent.IsKeyDown(Keys.LeftShift))
                pan *= 3;
            if (keyStateCurrent.IsKeyDown(Keys.W))
                camera.Position -= new Vector2(0, pan);
            if (keyStateCurrent.IsKeyDown(Keys.S))
                camera.Position += new Vector2(0, pan);
            if (keyStateCurrent.IsKeyDown(Keys.A))
                camera.Position -= new Vector2(pan, 0);
            if (keyStateCurrent.IsKeyDown(Keys.D))
                camera.Position += new Vector2(pan, 0);

            // Snelheid
            if (keyStateCurrent.IsKeyDown(Keys.OemOpenBrackets))
                Netwerk.Timer -= 100;

            // Reset
            if (keyStateCurrent.IsKeyDown(Keys.D0))
            {
                camera.Position = oorsprong;
                camera.Rotation = 0;
                Netwerk.Test();
            }

            // Verander muis
            if (keyStateCurrent.IsKeyDown(Keys.Space) &&
                mouseStateCurrent.X >= 0 && mouseStateCurrent.X <= schermMidden.X * 2 &&
                mouseStateCurrent.Y >= 0 && mouseStateCurrent.Y <= schermMidden.Y * 2)
            {
                form.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            }
            else if (SpriteManager.Click(new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y), schermMidden, schermMiddenBegin, camera.Position - oorsprong, camera.Zoom.X, false))
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

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState, camera.ViewTransformationMatrix());
            SpriteManager.DrawAll(spriteBatch, graphics.GraphicsDevice, camera.Zoom.X);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}