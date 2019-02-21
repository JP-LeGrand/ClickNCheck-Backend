using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Models
{
    public class LinkCode
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
        public int Admin_ID { get; set; }
        public User Administrator { get; set; }
    }
}
