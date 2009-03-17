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
        private Vector2 schermPositieBegin, schermPositieVerschil, schermMiddenBegin, schermMidden; // Positie scherm (nodig voor coordinaten)

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
            zoomStep = 0.01F;
            panStep = 5;
            oorsprong = camera.Position;
            schermPositieBegin = new Vector2(form.Left, form.Top);
            schermMiddenBegin = new Vector2(form.Panel.Width / 2, form.Panel.Height / 2);
            schermMidden = schermMiddenBegin;
            camera.Zoom = new Vector2(0.5F);
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

            Netwerk.AddApparatuur(ObjectType.Router, new Vector2(440, 0));
            Netwerk.AddApparatuur(ObjectType.Mainframe, new Vector2(960, 0));
            Netwerk.AddApparatuur(ObjectType.Switch, new Vector2(440, 350));
            Netwerk.AddApparatuur(ObjectType.Pc, new Vector2(-100, 500));
            Netwerk.AddApparatuur(ObjectType.Laptop, new Vector2(960, 500));

            Netwerk.Verbind(Netwerk.Apparatuur[0], Netwerk.Apparatuur[1]);
            Netwerk.Verbind(Netwerk.Apparatuur[0], Netwerk.Apparatuur[2]);
            Netwerk.Verbind(Netwerk.Apparatuur[2], Netwerk.Apparatuur[3]);
            Netwerk.Verbind(Netwerk.Apparatuur[2], Netwerk.Apparatuur[4]);
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
            System.Threading.Thread.Sleep(3); // CPU van 50% naar 3% D:
            UpdateInput();
            if (camera.Zoom.X > 1)
            { camera.Zoom = new Vector2(1); }
            else if (camera.Zoom.X < 0.2F)
            { camera.Zoom = new Vector2(0.2F); }
            schermPositieVerschil = new Vector2(form.Left, form.Top) - schermPositieBegin;
            form.label1.Text = String.Format("Camera.Zoom: {0}\nCamera.Position: {1}\nschermMidden: {2}\nschermPositie: {3}\nschermPositieBegin: {4}\nMuis.Position: {5}",
                camera.Zoom.X, camera.Position, schermMidden, new Vector2(form.Left, form.Top), schermPositieBegin,
                new Vector2(mouseStateCurrent.X - 121, mouseStateCurrent.Y + 20) - schermPositieVerschil - schermMidden);
            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            #region Muis
            mouseStateCurrent = Mouse.GetState();

            // Selecteren
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                SpriteManager.Click(new Vector2(mouseStateCurrent.X - 121, mouseStateCurrent.Y + 20) - schermPositieVerschil,
                    schermMidden, schermMiddenBegin, camera.Position - oorsprong, camera.Zoom.X);
            }

            // Object slepen
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Pressed)
            {
                if (SpriteManager.Geselecteerde >= 0)
                {
                    Netwerk.Apparatuur[SpriteManager.Geselecteerde].Positie -= (new Vector2(mouseStatePrevious.X, mouseStatePrevious.Y)
                        - new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y)) / camera.Zoom.X;
                }
            }

            // Camera slepen
            if (mouseStateCurrent.RightButton == ButtonState.Pressed && mouseStatePrevious.RightButton == ButtonState.Pressed)
            {
                camera.Position += (new Vector2(mouseStatePrevious.X, mouseStatePrevious.Y) - new Vector2(mouseStateCurrent.X, mouseStateCurrent.Y))
                    / camera.Zoom.X;
            }
            mouseStatePrevious = mouseStateCurrent;
            #endregion Muis

            #region Toetsenbord
            keyStateCurrent = Keyboard.GetState();
            // Zoom
            if (keyStateCurrent.IsKeyDown(Keys.OemPlus)) { camera.Zoom += new Vector2(zoomStep); }
            if (keyStateCurrent.IsKeyDown(Keys.OemMinus)) { camera.Zoom -= new Vector2(zoomStep); }
            // Pan
            float pan = panStep / camera.Zoom.X;
            if (keyStateCurrent.IsKeyDown(Keys.LeftShift)) { pan *= 3; }
            if (keyStateCurrent.IsKeyDown(Keys.W)) { camera.Position -= new Vector2(0, pan); }
            if (keyStateCurrent.IsKeyDown(Keys.S)) { camera.Position += new Vector2(0, pan); }
            if (keyStateCurrent.IsKeyDown(Keys.A)) { camera.Position -= new Vector2(pan, 0); }
            if (keyStateCurrent.IsKeyDown(Keys.D)) { camera.Position += new Vector2(pan, 0); }
            // Andere
            if (keyStateCurrent.IsKeyDown(Keys.D0)) { camera.Position = oorsprong; camera.Rotation = 0; }
            keyStatePrevious = keyStateCurrent;
            #endregion Camera
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState, camera.ViewTransformationMatrix());
            SpriteManager.Draw(spriteBatch, graphics.GraphicsDevice, camera.Zoom.X, form.label1);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}