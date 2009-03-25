using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    /// <summary>
    /// De class die verantwoordelijk is voor alles wat met sprites te maken heeft,
    /// zoals tekenen en selecteren.
    /// </summary>
    public static class SpriteManager
    {
        public static Apparaat Geselecteerde { get; private set; }
        public static List<MalwareAnimatie> Animaties { get; set; }

        static SpriteManager()
        {
            Geselecteerde = null;
            Animaties = new List<MalwareAnimatie>();
        }

        /// <summary>
        /// Zoekt een object onder de muis en selecteert deze.
        /// Returnt de index van dit object in Netwerk.Apparatuur, of -1.
        /// </summary>
        public static bool Click(Vector2 muis, Vector2 midden, Vector2 middenBegin, Vector2 offset, float zoom, bool selecteer)
        {
            muis -= midden;

            // Deselecteer alles
            if (selecteer)
                Deselecteer();

            // Zoek en selecteer geklikt object
            for (int i = 0; i < Netwerk.Apparatuur.Count; i++)
            {
                Apparaat app = Netwerk.Apparatuur[i];
                Vector2 coord = (app.Positie - middenBegin - offset) * zoom;
                if (System.Math.Abs(coord.X - muis.X) < app.Texture.Width / 2 * zoom
                    && System.Math.Abs(coord.Y - muis.Y) < app.Texture.Height / 2 * zoom)
                {
                    if (selecteer)
                    {
                        Selecteer(app);
                        Apparaat swap = Netwerk.Apparatuur[0];
                        Netwerk.Apparatuur[0] = Netwerk.Apparatuur[i];
                        Netwerk.Apparatuur[i] = swap;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Selecteert het gegeven apparaat.
        /// </summary>
        public static void Selecteer(Apparaat app)
        {
            app.Texture = app.TextureGeselecteerd;
            Geselecteerde = app;
        }

        /// <summary>
        /// Deselecteert alle apparaten.
        /// </summary>
        public static void Deselecteer()
        {
            Geselecteerde = null;
            foreach (Apparaat app in Netwerk.Apparatuur)
            {
                app.Texture = app.TextureNormaal;
            }
        }

        public static void DrawAll(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float zoom)
        {
            DrawApparaten(spriteBatch, graphicsDevice, zoom);
            DrawVerbindingen(spriteBatch, graphicsDevice, zoom);
            DrawMalware(spriteBatch);
        }

        private static void DrawApparaten(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float zoom)
        {
            // Teken sprites en vierkanten (van achter naar voren zodat de geselecteerde boven staat)
            PrimitiveBrush brush = new PrimitiveBrush(Color.Gray, graphicsDevice, zoom);
            for (int i = Netwerk.Apparatuur.Count - 1; i >= 0; i--)
            {
                Apparaat app = Netwerk.Apparatuur[i];
                // Sprite
                spriteBatch.Draw(app.Texture, app.Positie, null, Color.White, 0, app.Midden, 1, SpriteEffects.None, 0);

                // Infectie-logo
                if (app.Infecties.Count > 0 && app.GetType() == typeof(Computer)) // Als app een geinfecteerde computer is
                    spriteBatch.Draw(Textures.Geinfecteerd, app.Positie + new Vector2(-124, 64), Color.White); // Teken het teken in de hoek

                // Vierkant
                if (app == Geselecteerde)
                    brush.LineColor = Color.Blue;
                brush.DrawSquare(app.Positie, 128, spriteBatch);
            }
        }

        private static void DrawVerbindingen(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float zoom)
        {
            // Teken verbindingen
            PrimitiveBrush brush = new PrimitiveBrush(Color.Black, graphicsDevice, zoom / 2);
            foreach (Apparaat a1 in (from app in Netwerk.Apparatuur where app.Parent != null select app))
            {
                Apparaat a2 = a1.Parent;

                // Bereken hoek tussen de 2 apparaten
                double hoek = Math.Atan2((double)(a2.Positie.Y - a1.Positie.Y), (double)(a2.Positie.X - a1.Positie.X));

                // Bereken daarmee de gewenste offset van de lijn
                float xOffset = 128;
                float yOffset = xOffset * (float)Math.Tan(hoek);
                bool x = true;

                double graden = hoek * 180 / Math.PI;
                if ((graden >= 45 && graden < 135) || (graden >= -135 && graden < -45))
                {
                    yOffset = 128;
                    xOffset = yOffset / (float)Math.Tan(hoek);
                    x = false;
                }

                int xRichting = 1;
                int yRichting = -1;
                if ((x && a1.Positie.X > a2.Positie.X) || (!x && a1.Positie.Y > a2.Positie.Y))
                {
                    xRichting = -1;
                    yRichting = 1;
                }

                float xOffset1 = xOffset * xRichting;
                float yOffset1 = yOffset * -yRichting;
                float xOffset2 = xOffset * -xRichting;
                float yOffset2 = yOffset * yRichting;

                if (System.Math.Abs(a1.Positie.X - a2.Positie.X) < a1.Texture.Width &&
                    System.Math.Abs(a1.Positie.Y - a2.Positie.Y) < a1.Texture.Width)
                {
                    brush.DrawLine(a1.Positie, a2.Positie, spriteBatch);
                }
                else
                {
                    brush.DrawLine(a1.Positie + new Vector2(xOffset1, yOffset1), a2.Positie + new Vector2(xOffset2, yOffset2), spriteBatch);
                }
            }
        }

        private static void DrawMalware(SpriteBatch spriteBatch)
        {
            // Teken verspreidende Malware
            foreach (MalwareAnimatie mal in Animaties)
            {
                // Bereken hoek tussen de 2 apparaten, en bepaal daarmee of de texture moet worden gespiegeld
                spriteBatch.Draw(Textures.Malware, mal.Positie, null, Color.White, mal.Hoek, new Vector2(32), 1, mal.Effect, 0);
            }
        }
    }

    public class MalwareAnimatie
    {
        public Apparaat Start { get; private set; }
        public Apparaat Eind { get; private set; }
        public float Hoek { get; private set; }
        public SpriteEffects Effect { get; private set; }
        public Vector2 Positie { get; private set; }
        public bool Klaar { get; private set; }
        public Malware Malware { get; private set; }

        private List<Apparaat> route;
        private int aantal;
        private double procent;

        public MalwareAnimatie(List<Apparaat> route, Malware malware)
        {
            Positie = route[0].Positie;
            Klaar = false;
            Malware = malware;
            this.route = route;
            aantal = route.Count - 1;
            procent = 0;
        }

        public void Update(Double time)
        {
            procent += time / Netwerk.Timer;
            if (procent >= 1)
            {
                Klaar = true;
                return;
            }

            int i = (int)(aantal * procent);
            Start = route[i];
            Eind = route[i + 1];
            Hoek = (float)Math.Atan2((float)(Eind.Positie.Y - Start.Positie.Y), (float)(Eind.Positie.X - Start.Positie.X));
            Effect = (Hoek > Math.PI / 2 || Hoek < Math.PI / -2) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            Positie = Start.Positie + (Eind.Positie - Start.Positie) * new Vector2((float)(procent - (float)i / aantal) * aantal);
        }
    }
}
