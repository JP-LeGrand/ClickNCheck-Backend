using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.RefCheck.DocType
{
    public static class EmploymentType
    {
        private static readonly List<pair> employmentType = new List<pair>
        {
            new pair("Fulltime", "FT" ),
            new pair("Contract", "CON"),
            new pair("Part-time", "PT"),
            new pair("Temporary", "TEMP")
        };

        public static string getName(string code)
        {
            foreach(pair pa in employmentType)
            {
                if (pa.code == code)
                    return pa.name;
            }
            return null;
        }

        public static string getCode(string name)
        {
            foreach (pair pa in employmentType)
            {
                if (pa.name == name)
                    return pa.code;
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
