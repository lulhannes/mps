using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPS
{
    public static class Netwerk
    {
        public static List<object> Apparatuur { get; private set; }
        public static List<object[]> Verbindingen { get; private set; }

        static Netwerk()
        {
            Apparatuur = new List<object>();
            Verbindingen = new List<object[]>();
        }
    }
}
