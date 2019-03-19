using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.DocType
{
    public static class Region
    {
        private static readonly List<pair> regions = new List<pair>
        {
            new pair("Gauteng", "GAU" ),
            new pair("Eastern Cape", "EC"),
            new pair("KwaZulu-Natal", "KZN"),
            new pair("Mpumalanga", "MPU"),
            new pair("North West Province", "NW"),
            new pair("Northern Cape", "NC"),
            new pair("Northern Province","NP"),
            new pair("Free State", "FS"),
            new pair("Western Cape", "WC")
        };
            
        public static string getName(string code)
        {
            foreach(pair prov in regions)
            {
                if (prov.code == code)
                    return prov.name;
            }
            return null;
        }

        public static string getCode(string name)
        {
            foreach (pair prov in regions)
            {
                if (prov.name == name)
                    return prov.code;
            }
            return null;
        }

        private class pair
        {
            public string name;
            public string code;

            public pair(string name, string code)
            {
                this.name = name;
                this.code = code;
            }
        }
    }
}
