using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    public enum ObjectType
    {
        Laptop,
        Mainframe,
        Pc,
        Router,
        Switch
    };

    public class NetwerkObject
    {
        public ObjectType Soort { get; set; }
        public Texture2D TextureNormaal { get; set; }
        public Texture2D TextureGeinfecteerd { get; private set; }
        public Texture2D TextureGeselecteerd { get; private set; }
        public Texture2D Texture { get; set; }
        public Vector2 Positie { get; set; }
        public Vector2 Midden { get; set; }

        public NetwerkObject(ObjectType type, Vector2 positie)
        {
            Soort = type;
            TextureNormaal = Textures.GetNormaal(Soort);
            TextureGeselecteerd = Textures.GetGeselecteerd(Soort);
            Texture = TextureNormaal;
            Positie = positie;
            Midden = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }
    }
}