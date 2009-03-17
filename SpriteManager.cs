using System;
using System.Collections.Generic;
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
        public static int Geselecteerde { get; private set; }

        static SpriteManager()
        {
            Geselecteerde = -1;
        }

        /// <summary>
        /// Zoekt een object op deze positie en selecteert deze.
        /// Returnt de index van dit object in Netwerk.Apparatuur, of -1.
        /// </summary>
        public static int Click(Vector2 positie, Vector2 midden, Vector2 middenBegin, Vector2 offset, float zoom)
        {
            int selectie = -1;
            positie -= midden;

            // Deselecteer alles
            foreach (Apparaat app in Netwerk.Apparatuur)
            {
                app.Texture = app.TextureNormaal;
            }

            // Zoek geklikt object
            for (int i = 0; i < Netwerk.Apparatuur.Count; i++)
            {
                Apparaat app = Netwerk.Apparatuur[i];
                Vector2 coord = (app.Positie - middenBegin - offset) * zoom;
                if (System.Math.Abs(coord.X - positie.X) < app.Texture.Width / 2 * zoom
                    && System.Math.Abs(coord.Y - positie.Y) < app.Texture.Height / 2 * zoom)
                {
                    Selecteer(app);
                    Apparaat swap = Netwerk.Apparatuur[0];
                    Netwerk.Apparatuur[0] = Netwerk.Apparatuur[i];
                    Netwerk.Apparatuur[i] = swap;

                    selectie = 0;
                    break;
                }
            }
            Geselecteerde = selectie;
            return selectie;
        }

        /// <summary>
        /// Selecteert het gegeven object.
        /// </summary>
        public static void Selecteer(Apparaat obj)
        {
            obj.Texture = obj.TextureGeselecteerd;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float zoom, System.Windows.Forms.Label lbl)
        {
            // Teken sprites en vierkanten (van achter naar voren zodat de geselecteerde boven staat)
            PrimitiveBrush brush = new PrimitiveBrush(Color.Gray, graphicsDevice, zoom);
            for (int i = Netwerk.Apparatuur.Count - 1; i >= 0; i--)
            {
                Apparaat obj = Netwerk.Apparatuur[i];
                spriteBatch.Draw(obj.Texture, obj.Positie, null, Color.White, 0, obj.Midden, 1, SpriteEffects.None, 0);
                if (i == Geselecteerde)
                    brush.LineColor = Color.Blue;
                brush.DrawSquare(obj.Positie, 128, spriteBatch);
            }

            // Teken verbindingen
            brush = new PrimitiveBrush(Color.Black, graphicsDevice, zoom / 2);
            foreach (Apparaat[] bnd in Netwerk.Verbindingen)
            {
                // Oh lawd, is dat sum geometry? D:
                double hoek = Math.Atan2((double)(bnd[1].Positie.Y - bnd[0].Positie.Y), (double)(bnd[1].Positie.X - bnd[0].Positie.X));

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
                if ((x && bnd[0].Positie.X > bnd[1].Positie.X) || (!x && bnd[0].Positie.Y > bnd[1].Positie.Y))
                {
                    xRichting = -1;
                    yRichting = 1;
                }

                float xOffset1 = xOffset * xRichting;
                float yOffset1 = yOffset * -yRichting;
                float xOffset2 = xOffset * -xRichting;
                float yOffset2 = yOffset * yRichting;

                if (System.Math.Abs(bnd[0].Positie.X - bnd[1].Positie.X) < bnd[0].Texture.Width &&
                    System.Math.Abs(bnd[0].Positie.Y - bnd[1].Positie.Y) < bnd[0].Texture.Width)
                {
                    brush.DrawLine(bnd[0].Positie, bnd[1].Positie, spriteBatch);
                }
                else
                {
                    brush.DrawLine(bnd[0].Positie + new Vector2(xOffset1, yOffset1), bnd[1].Positie + new Vector2(xOffset2, yOffset2), spriteBatch);
                }
            }
        }
    }
}
