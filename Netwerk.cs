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
            //foreach (NetwerkApparaat app in (from app in Apparatuur where app is NetwerkApparaat select app))
            //{
            //    app.Infecties.Clear();
            //}

            // Voeg random computers toe
            //Random rand = new Random();
            //AddComputer(ApparaatType.Pc, 0, new Vector2(rand.Next(-2000, 3000), rand.Next(-2000, 2700)));
            //Verbind(Apparatuur[Apparatuur.Count - 1], Apparatuur[rand.Next(Apparatuur.Count - 1)]);
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

            AddNetwerkApparaat(ApparaatType.Modem, new Vector2(-100, -100));
            AddNetwerkApparaat(ApparaatType.Router, new Vector2(430, 0));
            AddComputer(ApparaatType.Server, 0, new Vector2(960, 0));
            AddNetwerkApparaat(ApparaatType.Switch, new Vector2(430, 350));
            AddComputer(ApparaatType.Pc, 0, new Vector2(-100, 500));
            AddComputer((ApparaatType)Enum.Parse(typeof(ApparaatType), "Laptop"), 0, new Vector2(160, 800));
            AddComputer(ApparaatType.Pc, 0, new Vector2(700, 800));
            AddComputer(ApparaatType.Laptop, 0, new Vector2(960, 500));
            
            Verbind(Apparatuur[0], Apparatuur[1]);
            Verbind(Apparatuur[1], Apparatuur[2]);
            Verbind(Apparatuur[1], Apparatuur[3]);
            Verbind(Apparatuur[3], Apparatuur[4]);
            Verbind(Apparatuur[3], Apparatuur[5]);
            Verbind(Apparatuur[3], Apparatuur[6]);
            Verbind(Apparatuur[3], Apparatuur[7]);

            AddMalware(0, 0);
            Malware[0].Infecteer(Apparatuur[5]);
        }
    }
}