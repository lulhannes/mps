using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace MPS
{
    public static class Netwerk
    {
        public static List<Apparaat> Apparatuur { get; private set; }
        public static List<Malware> Malwares { get; private set; }
        public static int Timer { get; set; }

        private static double timePrev;

        static Netwerk()
        {
            Apparatuur = new List<Apparaat>();
            Malwares = new List<Malware>();
            Timer = 3000;
            timePrev = 0;
        }

        public static void AddComputer(ApparaatType type, int firewall, int antivirus, Vector2 positie)
        {
            Apparatuur.Add(new Computer(type, firewall, antivirus, positie));
        }

        public static void AddComputer(ApparaatType type, int firewall, int antivirus, int x, int y)
        {
            Apparatuur.Add(new Computer(type, firewall, antivirus, new Vector2(x, y)));
        }

        public static void AddNetwerkApparaat(ApparaatType type, Vector2 positie)
        {
            Apparatuur.Add(new NetwerkApparaat(type, positie));
        }

        public static void RemoveApparaat(int index)
        {
            Apparatuur.RemoveAt(index);
        }

        /// <summary>
        /// Verbind twee apparaten indien mogelijk.
        /// </summary>
        /// <param name="a1">Het kind.</param>
        /// <param name="a2">De ouder.</param>
        public static void Verbind(Apparaat a1, Apparaat a2)
        {
            if (KanVerbinden(a1, a2))
                a1.Parent = a2;
        }

        /// <summary>
        /// Controleer of twee apparaten verbonden kunnen worden.
        /// </summary>
        /// <param name="a1">Het kind.</param>
        /// <param name="a2">De ouder.</param>
        /// <returns></returns>
        public static bool KanVerbinden(Apparaat a1, Apparaat a2)
        {
            return (a2 != null && a1 != null && a2 != a1 && a1 != a2.Root && a2.Parent != a1 && a2 is NetwerkApparaat &&
                (a2 != a2.Root || a2.Children.Count == 0) && !a2.Parents.Contains(a1));
        }

        public static void AddMalware(int firewall, int antivirus)
        {
            Malwares.Add(new Malware(firewall, antivirus));
        }

        public static void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - timePrev > Timer)
            {
                // Voeg nieuwe animaties toe
                foreach (Apparaat a in Apparatuur)
                    foreach (Malware mal in (a.Infecties.Where(m => a.Firewall <= m.Firewall)).ToArray())
                        foreach (Apparaat b in mal.GetOngeinfecteerden())
                            if (a.RouteNaar(b) != null)
                                SpriteManager.Animaties.Add(new MalwareAnimatie(a.RouteNaar(b), mal));

                timePrev = gameTime.TotalGameTime.TotalMilliseconds;
            }

            Malware.Update(gameTime);
        }

        /// <summary>
        /// Reset en laad wat apparaten om te testen.
        /// </summary>
        public static void Test()
        {
            Apparatuur.Clear();
            Malwares.Clear();
            SpriteManager.Animaties.Clear();

            AddNetwerkApparaat(ApparaatType.Modem, new Vector2(-100, -100));
            AddNetwerkApparaat(ApparaatType.Router, new Vector2(430, 0));
            AddComputer(ApparaatType.Server, 0, 0, new Vector2(960, 0));
            AddNetwerkApparaat(ApparaatType.Switch, new Vector2(430, 350));
            AddComputer(ApparaatType.Pc, 0, 0, new Vector2(-100, 500));
            AddComputer(ApparaatType.Laptop, 0, 0, new Vector2(160, 800));
            AddComputer(ApparaatType.Pc, 0, 1, new Vector2(700, 800));
            AddComputer(ApparaatType.Laptop, 1, 0, new Vector2(960, 500));
            
            Apparatuur[0].Children.Add(Apparatuur[1]);
            Apparatuur[1].Children.Add(Apparatuur[2]);
            Apparatuur[1].Children.Add(Apparatuur[3]);
            Apparatuur[3].Children.Add(Apparatuur[4]);
            Apparatuur[3].Children.Add(Apparatuur[5]);
            Apparatuur[3].Children.Add(Apparatuur[6]);
            Apparatuur[3].Children.Add(Apparatuur[7]);

            AddMalware(0, 0);
            Malwares[0].Infecteer(Apparatuur[5]);
        }
    }
}