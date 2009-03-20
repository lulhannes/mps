using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    public enum ApparaatType
    {
        Laptop,
        Mainframe,
        Pc,
        Router,
        Switch
    };

    public abstract class Apparaat
    {
        public ApparaatType Type { get; private set; }
        public Texture2D TextureNormaal { get; private set; }
        public Texture2D TextureGeinfecteerd { get; private set; }
        public Texture2D TextureGeselecteerd { get; private set; }
        public Texture2D Texture { get; set; }
        public Vector2 Positie { get; set; }
        public Vector2 Midden { get; private set; }

        public List<Malware> Infecties { get; set; }

        public Apparaat(ApparaatType type, Vector2 positie)
        {
            Type = type;
            TextureNormaal = Textures.GetNormaal(Type);
            TextureGeselecteerd = Textures.GetGeselecteerd(Type);
            Texture = TextureNormaal;
            Positie = positie;
            Midden = new Vector2(Texture.Width / 2, Texture.Height / 2);

            Infecties = new List<Malware>();
        }
    }

    public class Computer : Apparaat
    {
        public Computer(ApparaatType type, Vector2 positie)
            : base(type, positie)
        {
            //TODO
        }
    }

    public class NetwerkApparaat : Apparaat
    {
        public NetwerkApparaat(ApparaatType type, Vector2 positie)
            : base(type, positie)
        {
            //TODO
        }
    }
}