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
    public enum ObjectType
    {
        Hub,
        Laptop,
        Mainframe,
        Pc,
        Router
    };

    /// <summary>
    /// Een static class voor het opslaan en aanvragen van textures.
    /// </summary>
    static class Textures
    {
        public static Texture2D HubNormaal;
        public static Texture2D HubGeselecteerd;
        public static Texture2D LaptopNormaal;
        public static Texture2D LaptopGeselecteerd;
        //public static Texture2D LaptopGeinfecteerd;
        public static Texture2D MainframeNormaal;
        public static Texture2D MainframeGeselecteerd;
        //public static Texture2D MainframeGeinfecteerd;
        public static Texture2D PcNormaal;
        public static Texture2D PcGeselecteerd;
        //public static Texture2D PcGeinfecteerd;
        public static Texture2D RouterNormaal;
        public static Texture2D RouterGeselecteerd;

        public static Texture2D Normaal(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Hub:
                    return HubNormaal;
                case ObjectType.Laptop:
                    return LaptopNormaal;
                case ObjectType.Mainframe:
                    return MainframeNormaal;
                case ObjectType.Pc:
                    return PcNormaal;
                case ObjectType.Router:
                    return RouterNormaal;
            }
            return null;
        }

        public static Texture2D Geselecteerd(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Hub:
                    return HubGeselecteerd;
                case ObjectType.Laptop:
                    return LaptopGeselecteerd;
                case ObjectType.Mainframe:
                    return MainframeGeselecteerd;
                case ObjectType.Pc:
                    return PcGeselecteerd;
                case ObjectType.Router:
                    return RouterGeselecteerd;
            }
            return null;
        }
    }

    class NetwerkObject
    {
        public NetwerkObject(ObjectType type, Vector2 positie)
        {
            Soort = type;
            TextureNormaal = Textures.Normaal(Soort);
            TextureGeselecteerd = Textures.Geselecteerd(Soort);
            Texture = TextureNormaal;
            Positie = positie;
            Midden = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public ObjectType Soort { get; set; }
        public Texture2D Texture { get; set; }
        public Texture2D TextureNormaal { get; set; }
        public Texture2D TextureGeinfecteerd { get; private set; }
        public Texture2D TextureGeselecteerd { get; private set; }
        public Vector2 Positie { get; set; }
        public Vector2 Midden { get; set; }
    }

    /// <summary>
    /// De class die verantwoordelijk is voor alles wat met sprites te maken heeft,
    /// zoals tekenen en selecteren.
    /// </summary>
    class Sprites
    {
        private List<NetwerkObject> objecten;

        public Sprites()
        {
            objecten = new List<NetwerkObject>();
        }

        public void Add(ObjectType type, Vector2 positie)
        {
            objecten.Add(new NetwerkObject(type, positie));
        }

        public NetwerkObject Click(Vector2 positie, Vector2 midden, Vector2 middenBegin, Vector2 offset, float zoom)
        {
            NetwerkObject selectie = null;
            positie -= midden;
            foreach (NetwerkObject obj in objecten)
            {
                obj.Texture = obj.TextureNormaal;
                Vector2 coord = (obj.Positie - middenBegin - offset) * zoom;
                if (System.Math.Abs(coord.X - positie.X) < obj.Texture.Width / 2 * zoom
                    && System.Math.Abs(coord.Y - positie.Y) < obj.Texture.Height / 2 * zoom)
                {
                    //System.Windows.Forms.MessageBox.Show(obj.Soort.ToString());
                    obj.Texture = obj.TextureGeselecteerd;
                    selectie = obj;
                }
            }
            return selectie;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            PrimitiveLine brush = new PrimitiveLine(graphicsDevice);
            foreach (NetwerkObject obj in objecten)
            {
                spriteBatch.Draw(obj.Texture, obj.Positie, null, Color.White, 0, obj.Midden, 1, SpriteEffects.None, 0);
                //brush.CreateLine(obj.Positie, obj.Positie*2);
                brush.Position = obj.Midden - new Vector2(obj.Texture.Width, obj.Texture.Height) / 2;
                brush.Render(spriteBatch);
            }
        }
    }

    /// <summary>
    /// A class to make primitive 2D objects out of lines.
    /// </summary>
    public class PrimitiveLine
    {
        Texture2D pixel;
        List<Vector2> vectors;

        /// <summary>
        /// Gets/sets the colour of the primitive line object.
        /// </summary>
        public Color Colour;

        /// <summary>
        /// Gets/sets the position of the primitive line object.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Gets/sets the render depth of the primitive line object (0 = front, 1 = back)
        /// </summary>
        public float Depth;

        /// <summary>
        /// Gets the number of vectors which make up the primtive line object.
        /// </summary>
        public int CountVectors
        {
            get
            {
                return vectors.Count;
            }
        }

        /// <summary>
        /// Creates a new primitive line object.
        /// </summary>
        /// <param name="graphicsDevice">The Graphics Device object to use.</param>
        public PrimitiveLine(GraphicsDevice graphicsDevice)
        {
            // create pixels
            pixel = new Texture2D(graphicsDevice, 1, 1, 1, TextureUsage.None, SurfaceFormat.Color);
            //Color[] pixels = new Color[1];
            //pixels[0] = Color.Red;
            Color[] pixels = { Color.Black };
            pixel.SetData<Color>(pixels);

            Colour = Color.White;
            Position = new Vector2(0, 0);
            Depth = 0;

            vectors = new List<Vector2>();
        }

        /// <summary>
        /// Called when the primive line object is destroyed.
        /// </summary>
        ~PrimitiveLine()
        {
        }

        /// <summary>
        /// Adds a vector to the primive live object.
        /// </summary>
        /// <param name="vector">The vector to add.</param>
        public void AddVector(Vector2 vector)
        {
            vectors.Add(vector);
        }

        /// <summary>
        /// Insers a vector into the primitive line object.
        /// </summary>
        /// <param name="index">The index to insert it at.</param>
        /// <param name="vector">The vector to insert.</param>
        public void InsertVector(int index, Vector2 vector)
        {
            vectors.Insert(index, vector);
        }

        /// <summary>
        /// Removes a vector from the primitive line object.
        /// </summary>
        /// <param name="vector">The vector to remove.</param>
        public void RemoveVector(Vector2 vector)
        {
            vectors.Remove(vector);
        }

        /// <summary>
        /// Removes a vector from the primitive line object.
        /// </summary>
        /// <param name="index">The index of the vector to remove.</param>
        public void RemoveVector(int index)
        {
            vectors.RemoveAt(index);
        }

        /// <summary>
        /// Clears all vectors from the primitive line object.
        /// </summary>
        public void ClearVectors()
        {
            vectors.Clear();
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

                // calculate the distance between the two vectors
                float distance = Vector2.Distance(vector1, vector2);

                // calculate the angle between the two vectors
                float angle = (float)Math.Atan2((double)(vector2.Y - vector1.Y),
                    (double)(vector2.X - vector1.X));

                // stretch the pixel between the two vectors
                spriteBatch.Draw(pixel,
                    Position + vector1,
                    null,
                    Colour,
                    angle,
                    Vector2.Zero,
                    new Vector2(distance, 10),
                    SpriteEffects.None,
                    Depth);
            }
        }

        /// <summary>
        /// Creates a circle starting from 0, 0.
        /// </summary>
        /// <param name="radius">The radius (half the width) of the circle.</param>
        /// <param name="sides">The number of sides on the circle (the more the detailed).</param>
        public void CreateCircle(float radius, int sides)
        {
            vectors.Clear();

            float max = 2 * (float)Math.PI;
            float step = max / (float)sides;

            for (float theta = 0; theta < max; theta += step)
            {
                vectors.Add(new Vector2(radius * (float)Math.Cos((double)theta),
                    radius * (float)Math.Sin((double)theta)));
            }

            // then add the first vector again so it's a complete loop
            vectors.Add(new Vector2(radius * (float)Math.Cos(0),
                    radius * (float)Math.Sin(0)));
        }

        public void CreateLine(Vector2 start, Vector2 end)
        {
            vectors.Clear();
            vectors.Add(start);
            vectors.Add(end);
        }
    }
}