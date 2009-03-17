using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    /// <summary>
    /// A class to make primitive 2D objects out of lines.
    /// </summary>
    public class PrimitiveBrush
    {
        private Texture2D pixel;
        private List<Vector2> vectors;
        private float zoom;

        /// <summary>
        /// Gets/sets the position of the primitive line object.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Gets/sets the render depth of the primitive line object (0 = front, 1 = back)
        /// </summary>
        public float Depth;

        /// <summary>
        /// Gets/sets the color of the lines
        /// </summary>
        public Color LineColor;

        /// <summary>
        /// Creates a new primitive line object.
        /// </summary>
        /// <param name="graphicsDevice">The Graphics Device object to use.</param>
        public PrimitiveBrush(Color lineColor, GraphicsDevice graphicsDevice, float zoom)
        {
            // Create pixels
            pixel = new Texture2D(graphicsDevice, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            Color[] pixels = { Color.White };
            pixel.SetData<Color>(pixels);

            Position = new Vector2(0, 0);
            Depth = 0;
            LineColor = lineColor;
            vectors = new List<Vector2>();
            this.zoom = zoom;            
        }

        /// <summary>
        /// Creates a circle starting from 0, 0.
        /// </summary>
        /// <param name="radius">The radius (half the width) of the circle.</param>
        /// <param name="sides">The number of sides on the circle (the more the detailed).</param>
        public void DrawCircle(float radius, int sides, SpriteBatch spriteBatch)
        {
            float max = 2 * (float)Math.PI;
            float step = max / (float)sides;

            for (float theta = 0; theta < max; theta += step)
            {
                vectors.Add(new Vector2(radius * (float)Math.Cos((double)theta),
                    radius * (float)Math.Sin((double)theta)));
            }

            // Then add the first vector again so it's a complete loop
            vectors.Add(new Vector2(radius * (float)Math.Cos(0),
                    radius * (float)Math.Sin(0)));

            Render(spriteBatch);
        }

        public void DrawLine(Vector2 start, Vector2 end, SpriteBatch spriteBatch)
        {
            vectors.Add(start);
            vectors.Add(end);

            Render(spriteBatch);
        }

        public void DrawRectangle(Vector2 topLeft, Vector2 bottomRight, SpriteBatch spriteBatch)
        {
            vectors.Add(topLeft);
            vectors.Add(new Vector2(bottomRight.X, topLeft.Y));
            vectors.Add(bottomRight);
            vectors.Add(new Vector2(topLeft.X, bottomRight.Y));
            vectors.Add(topLeft);
            
            Render(spriteBatch);
        }

        public void DrawSquare(Vector2 center, int size, SpriteBatch spriteBatch)
        {
            DrawRectangle(center - new Vector2(size), center + new Vector2(size), spriteBatch);
        }

        /// <summary>
        /// Renders the primtive line object.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use to render the primitive line object.</param>
        public void Render(SpriteBatch spriteBatch)
        {
            if (vectors.Count < 2)
                return;

            for (int i = 1; i < vectors.Count; i++)
            {
                Vector2 vector1 = (Vector2)vectors[i - 1];
                Vector2 vector2 = (Vector2)vectors[i];

                // Calculate the distance between the two vectors
                float distance = Vector2.Distance(vector1, vector2);

                // Calculate the angle between the two vectors
                float angle = (float)Math.Atan2((double)(vector2.Y - vector1.Y), (double)(vector2.X - vector1.X));

                // Stretch the pixel between the two vectors
                spriteBatch.Draw(pixel,
                    Position + vector1,
                    null,
                    LineColor,
                    angle,
                    Vector2.Zero,
                    new Vector2(distance, 1 / zoom),
                    SpriteEffects.None,
                    Depth);
            }
            vectors.Clear();
        }
    }
}
