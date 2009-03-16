using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    /// <summary>
    /// Een static class voor het opslaan en aanvragen van textures.
    /// </summary>
    public static class Textures
    {
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
        public static Texture2D SwitchNormaal;
        public static Texture2D SwitchGeselecteerd;

        /// <summary>
        /// Laadt alle textures.
        /// </summary>
        public static void Load(ContentManager content)
        {
            LaptopNormaal = content.Load<Texture2D>("Sprites\\laptop");
            LaptopGeselecteerd = content.Load<Texture2D>("Sprites\\laptop_sel");
            MainframeNormaal = content.Load<Texture2D>("Sprites\\mainframe");
            MainframeGeselecteerd = content.Load<Texture2D>("Sprites\\mainframe_sel");
            PcNormaal = content.Load<Texture2D>("Sprites\\pc");
            PcGeselecteerd = content.Load<Texture2D>("Sprites\\pc_sel");
            RouterNormaal = content.Load<Texture2D>("Sprites\\router");
            RouterGeselecteerd = content.Load<Texture2D>("Sprites\\router_sel");
            SwitchNormaal = content.Load<Texture2D>("Sprites\\switch");
            SwitchGeselecteerd = content.Load<Texture2D>("Sprites\\switch_sel");
        }

        /// <summary>
        /// Returnt de normale texture van een object.
        /// </summary>
        public static Texture2D GetNormaal(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Laptop:
                    return LaptopNormaal;
                case ObjectType.Mainframe:
                    return MainframeNormaal;
                case ObjectType.Pc:
                    return PcNormaal;
                case ObjectType.Router:
                    return RouterNormaal;
                case ObjectType.Switch:
                    return SwitchNormaal;
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
                case ObjectType.Laptop:
                    return LaptopGeselecteerd;
                case ObjectType.Mainframe:
                    return MainframeGeselecteerd;
                case ObjectType.Pc:
                    return PcGeselecteerd;
                case ObjectType.Router:
                    return RouterGeselecteerd;
                case ObjectType.Switch:
                    return SwitchGeselecteerd;
            }
            return null;
        }
    }
}
