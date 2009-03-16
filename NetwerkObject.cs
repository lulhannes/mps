using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        /// <summary>
        /// Laadt alle textures.
        /// </summary>
        public static void Init(ContentManager content)
        {
            HubNormaal = content.Load<Texture2D>("Sprites\\hub");
            HubGeselecteerd = content.Load<Texture2D>("Sprites\\hub_sel");
            LaptopNormaal = content.Load<Texture2D>("Sprites\\laptop");
            LaptopGeselecteerd = content.Load<Texture2D>("Sprites\\laptop_sel");
            MainframeNormaal = content.Load<Texture2D>("Sprites\\mainframe");
            MainframeGeselecteerd = content.Load<Texture2D>("Sprites\\mainframe_sel");
            PcNormaal = content.Load<Texture2D>("Sprites\\pc");
            PcGeselecteerd = content.Load<Texture2D>("Sprites\\pc_sel");
            RouterNormaal = content.Load<Texture2D>("Sprites\\router");
            RouterGeselecteerd = content.Load<Texture2D>("Sprites\\router_sel");
        }

        /// <summary>
        /// Returnt de normale texture van een object.
        /// </summary>
        public static Texture2D GetNormaal(ObjectType type)
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

        /// <summary>
        /// Returnt de geselecteerde texture van een object.
        /// </summary>
        public static Texture2D GetGeselecteerd(ObjectType type)
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
            TextureNormaal = Textures.GetNormaal(Soort);
            TextureGeselecteerd = Textures.GetGeselecteerd(Soort);
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
            PrimitiveLine brush = new PrimitiveLine(Color.Red, graphicsDevice);
            foreach (NetwerkObject obj in objecten)
            {
                spriteBatch.Draw(obj.Texture, obj.Positie, null, Color.White, 0, obj.Midden, 1, SpriteEffects.None, 0);
                brush.CreateLine(obj.Positie, obj.Positie * 2);
                brush.Render(spriteBatch);
            }
        }
    }
}