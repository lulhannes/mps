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

        static Netwerk()
        {
            Apparatuur = new List<Apparaat>();
            Verbindingen = new List<Apparaat[]>();
        }

        public static void AddApparatuur(ObjectType type, Vector2 positie)
        {
            Apparatuur.Add(new Apparaat(type, positie));
        }

        public static void Verbind(Apparaat app1, Apparaat app2)
        {
            Verbindingen.Add(new Apparaat[] { app1, app2 });
        }
    }
}
