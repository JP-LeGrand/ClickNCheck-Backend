using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck
{
    public class Recruiters
    {
        public static IList<RecList> ReadDetails(string path)
        {
            var list = new List<RecList>();
            foreach (var item in File.ReadLines(path).Skip(1))
            {

                list.Add(RecList.FetchList(item));

            }

            return list;
        }

        public static List<string> getEmails(IEnumerable<RecList> _list)
        {
            List<string> email = new List<string>();
            foreach (var item in _list)
            {
                email.Add(item.Email);
            }

            return email;
        }

        public static List<string> getNames(IEnumerable<RecList> _list)
        {
            List<string> names = new List<string>();
            foreach (var item in _list)
            {
                names.Add(item.Name);
            }

            return names;
        }
    }

    public class RecList
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public static RecList FetchList(string item)
        {
            var data = item.Split(',');

            return new RecList()
            {
                Email = data[0],
                Name = data[1],
            };
        }

    }
}
