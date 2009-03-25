using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace MPS
{
    public static class Netwerk
    {
        public static List<Apparaat> Apparatuur { get; private set; }
        public static List<Apparaat[]> Verbindingen { get; private set; }
        public static List<Malware> Malwares { get; private set; }
        public static int Timer { get; set; }

        private static double timePrev;

        static Netwerk()
        {
            Apparatuur = new List<Apparaat>();
            Verbindingen = new List<Apparaat[]>();
            Malwares = new List<Malware>();
            Timer = 3000;
            timePrev = 0;
        }

        public static void AddComputer(ApparaatType type, int firewall, int antivirus, Vector2 positie)
        {
            Apparatuur.Add(new Computer(type, firewall, antivirus, positie));
        }

        public static void AddNetwerkApparaat(ApparaatType type, Vector2 positie)
        {
            Apparatuur.Add(new NetwerkApparaat(type, positie));
        }

        public static void RemoveApparaat(int index)
        {
            Apparatuur.RemoveAt(index);
        }

        public static void Verbind(Apparaat app1, Apparaat app2)
        {
            Verbindingen.Add(new Apparaat[] { app1, app2 });
        }

        public static void AddMalware(int firewall, int antivirus)
        {
            Malwares.Add(new Malware(firewall, antivirus));
        }

        public static void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - timePrev > Timer || timePrev == 0)
            {
                // Voeg nieuwe animaties toe
                foreach (Apparaat a in Netwerk.Apparatuur)
                    foreach (Malware mal in (from mal in a.Infecties where a.Firewall <= mal.Firewall select mal))
                        foreach (Apparaat b in mal.GetOngeinfecteerden())
                            SpriteManager.Animaties.Add(new MalwareAnimatie(a.RouteNaar(b), mal));

                timePrev = gameTime.TotalGameTime.TotalMilliseconds + 0.001;
            }

            Malware.Update(gameTime);
        }

        /// <summary>
        /// Reset en laad wat apparaten om te testen.
        /// </summary>
        public static void Test()
        {
            Apparatuur.Clear();
            Verbindingen.Clear();
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