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
        public static List<NetwerkObject> Objecten { get; set; }
        public static int Geselecteerde { get; private set; }

        static SpriteManager()
        {
            Objecten = new List<NetwerkObject>();
            Geselecteerde = -1;
        }

        public static void Add(ObjectType type, Vector2 positie)
        {
            Objecten.Add(new NetwerkObject(type, positie));
        }

        /// <summary>
        /// Zoekt een object op deze positie en selecteert deze.
        /// Returnt de index van dit object in Objecten, of -1.
        /// </summary>
        public static int Click(Vector2 positie, Vector2 midden, Vector2 middenBegin, Vector2 offset, float zoom)
        {
            int selectie = -1;
            positie -= midden;

            // deselecteer alles
            foreach (NetwerkObject obj in Objecten)
            {
                obj.Texture = obj.TextureNormaal;
            }

            // zoek geklikt object
            for (int i = 0; i < Objecten.Count; i++)
            {
                NetwerkObject obj = Objecten[i];
                Vector2 coord = (obj.Positie - middenBegin - offset) * zoom;
                if (System.Math.Abs(coord.X - positie.X) < obj.Texture.Width / 2 * zoom
                    && System.Math.Abs(coord.Y - positie.Y) < obj.Texture.Height / 2 * zoom)
                {
                    Selecteer(obj);
                    NetwerkObject temp = Objecten[0];
                    Objecten[0] = Objecten[i];
                    Objecten[i] = temp;

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
        public static void Selecteer(NetwerkObject obj)
        {
            obj.Texture = obj.TextureGeselecteerd;
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, float zoom)
        {
            PrimitiveBrush brush = new PrimitiveBrush(Color.Gray, graphicsDevice, zoom);

            // teken sprites en vierkanten (van achter naar voren zodat de geselecteerde boven staat)
            for (int i = Objecten.Count - 1; i >= 0; i--)
            {
                NetwerkObject obj = Objecten[i];
                spriteBatch.Draw(obj.Texture, obj.Positie, null, Color.White, 0, obj.Midden, 1, SpriteEffects.None, 0);
                if (i == Geselecteerde)
                    brush.LineColor = Color.Blue;
                brush.DrawSquare(obj.Positie, 128, spriteBatch);
            }

            // teken verbindingen

        }
    }
}
