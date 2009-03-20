using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MPS
{
    public static class Netwerk
    {
        public static List<Apparaat> Apparatuur { get; private set; }
        public static List<Apparaat[]> Verbindingen { get; private set; }
        public static List<Malware> Malware { get; private set; }

        static Netwerk()
        {
            Apparatuur = new List<Apparaat>();
            Verbindingen = new List<Apparaat[]>();
            Malware = new List<Malware>();
        }

        public static void AddComputer(ApparaatType type, Vector2 positie)
        {
            Apparatuur.Add(new Computer(type, positie));
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
            foreach (Malware mw in Malware)
                mw.InfecteerBuren();
        }
    }
}
