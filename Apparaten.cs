using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    public enum ApparaatType
    {
        Laptop,
        Modem,
        Pc,
        Router,
        Server,
        Switch
    };

    public abstract class Apparaat : TreeNode<Apparaat>
    {
        public ApparaatType Type { get; private set; }
        public Texture2D TextureNormaal { get; private set; }
        public Texture2D TextureGeselecteerd { get; private set; }
        public Texture2D Texture { get; set; }
        public Vector2 Positie { get; set; }
        public Vector2 Midden { get; private set; }
        public int Firewall { get; set; }
        public int Antivirus { get; set; }
        public List<Malware> Infecties { get; set; }

        public Apparaat(ApparaatType type, Vector2 positie)
        {
            Type = type;
            TextureNormaal = Textures.GetNormaal(Type);
            TextureGeselecteerd = Textures.GetGeselecteerd(Type);
            Texture = TextureNormaal;
            Positie = positie;
            Midden = new Vector2(Texture.Width / 2, Texture.Height / 2);
            Firewall = 0;
            Antivirus = 0;
            Infecties = new List<Malware>();
        }
    }

    public class Computer : Apparaat
    {
        public Computer(ApparaatType type, int firewall, int antivirus, Vector2 positie)
            : base(type, positie)
        {
            Firewall = firewall;
            Antivirus = antivirus;
        }
    }

    public class NetwerkApparaat : Apparaat
    {
        public NetwerkApparaat(ApparaatType type, Vector2 positie)
            : base(type, positie)
        {
        }
    }
}