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
        public static List<Malware> Malware { get; private set; }

        private static List<Apparaat[]> teInfecterenApp;
        private static List<Malware> teInfecterenMal;

        static Netwerk()
        {
            Apparatuur = new List<Apparaat>();
            Verbindingen = new List<Apparaat[]>();
            Malware = new List<Malware>();

            teInfecterenApp = new List<Apparaat[]>();
            teInfecterenMal = new List<Malware>();
        }

        public static void AddComputer(ApparaatType type, int firewall, Vector2 positie)
        {
            Apparatuur.Add(new Computer(type, firewall, positie));
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
            Malware.Add(new Malware(firewall, antivirus));
        }

        public static void Update()
        {
            // Infecteer alle te infecteren apparaten
            for (int i = 0; i < teInfecterenMal.Count; i++)
            {
                teInfecterenMal[i].Infecteer(teInfecterenApp[i][0]);
            }

            // Zoek nieuwe te infecteren apparaten
            SpriteManager.Animaties.Clear();
            foreach (Malware mal in Malware)
            {
                int vorige = teInfecterenApp.Count;
                teInfecterenApp.AddRange(mal.GetAlleBuren());
                for (int i = vorige; i < teInfecterenApp.Count; i++)
                {
                    teInfecterenMal.Add(mal);
                    SpriteManager.Animaties.Add(new MalwareAnimatie(teInfecterenApp[i][1], teInfecterenApp[i][0]));
                }
            }

            // Desinfecteer netwerkapparatuur
            foreach (NetwerkApparaat app in (from app in Apparatuur where app.GetType() == typeof(NetwerkApparaat) select app))
            {
                //app.Infecties.Clear();
            }
        }

        /// <summary>
        /// Reset en laad wat apparaten om te testen.
        /// </summary>
        public static void Test()
        {
            Apparatuur = new List<Apparaat>();
            Verbindingen = new List<Apparaat[]>();
            Malware = new List<Malware>();

            teInfecterenApp = new List<Apparaat[]>();
            teInfecterenMal = new List<Malware>();

            SpriteManager.Animaties = new List<MalwareAnimatie>();

            AddNetwerkApparaat(ApparaatType.Router, new Vector2(430, 0));
            AddComputer(ApparaatType.Mainframe, 0, new Vector2(960, 0));
            AddNetwerkApparaat(ApparaatType.Switch, new Vector2(430, 350));
            AddComputer(ApparaatType.Pc, 0, new Vector2(-100, 500));
            AddComputer(ApparaatType.Laptop, 0, new Vector2(630, 800));
            AddComputer(ApparaatType.Laptop, 0, new Vector2(960, 500));
            
            Verbind(Apparatuur[0], Apparatuur[1]);
            Verbind(Apparatuur[0], Apparatuur[2]);
            Verbind(Apparatuur[2], Apparatuur[3]);
            Verbind(Apparatuur[2], Apparatuur[4]);
            Verbind(Apparatuur[2], Apparatuur[5]);

            AddMalware(0, 0);
            Malware[0].Infecteer(Apparatuur[3]);
        }
    }
}