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
        public static Texture2D ModemNormaal;
        public static Texture2D ModemGeselecteerd;
        public static Texture2D PcNormaal;
        public static Texture2D PcGeselecteerd;
        public static Texture2D RouterNormaal;
        public static Texture2D RouterGeselecteerd;
        public static Texture2D ServerNormaal;
        public static Texture2D ServerGeselecteerd;
        public static Texture2D SwitchNormaal;
        public static Texture2D SwitchGeselecteerd;
        public static Texture2D Geinfecteerd;
        public static Texture2D Malware;

        /// <summary>
        /// Laad alle textures.
        /// </summary>
        public static void Load(ContentManager content)
        {
            LaptopNormaal = content.Load<Texture2D>("Sprites\\laptop");
            LaptopGeselecteerd = content.Load<Texture2D>("Sprites\\laptop_sel");
            ModemNormaal = content.Load<Texture2D>("Sprites\\modem");
            ModemGeselecteerd = content.Load<Texture2D>("Sprites\\modem_sel");
            PcNormaal = content.Load<Texture2D>("Sprites\\pc");
            PcGeselecteerd = content.Load<Texture2D>("Sprites\\pc_sel");
            RouterNormaal = content.Load<Texture2D>("Sprites\\router");
            RouterGeselecteerd = content.Load<Texture2D>("Sprites\\router_sel");
            ServerNormaal = content.Load<Texture2D>("Sprites\\server");
            ServerGeselecteerd = content.Load<Texture2D>("Sprites\\server_sel");
            SwitchNormaal = content.Load<Texture2D>("Sprites\\switch");
            SwitchGeselecteerd = content.Load<Texture2D>("Sprites\\switch_sel");
            Geinfecteerd = content.Load<Texture2D>("Sprites\\geinfecteerd");
            Malware = content.Load<Texture2D>("Sprites\\malware");
        }

        /// <summary>
        /// Returnt de normale texture van een object.
        /// </summary>
        public static Texture2D GetNormaal(ApparaatType type)
        {
            switch (type)
            {
                case ApparaatType.Laptop:
                    return LaptopNormaal;
                case ApparaatType.Modem:
                    return ModemNormaal;
                case ApparaatType.Pc:
                    return PcNormaal;
                case ApparaatType.Router:
                    return RouterNormaal;
                case ApparaatType.Server:
                    return ServerNormaal;
                case ApparaatType.Switch:
                    return SwitchNormaal;
            }
            return null;
        }

        /// <summary>
        /// Returnt de geselecteerde texture van een object.
        /// </summary>
        public static Texture2D GetGeselecteerd(ApparaatType type)
        {
            switch (type)
            {
                case ApparaatType.Laptop:
                    return LaptopGeselecteerd;
                case ApparaatType.Modem:
                    return ModemGeselecteerd;
                case ApparaatType.Pc:
                    return PcGeselecteerd;
                case ApparaatType.Router:
                    return RouterGeselecteerd;
                case ApparaatType.Server:
                    return ServerGeselecteerd;
                case ApparaatType.Switch:
                    return SwitchGeselecteerd;
            }
            return null;
        }
    }
}
