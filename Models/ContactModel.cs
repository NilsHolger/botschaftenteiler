using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageSharer.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Comment { get; set; }
    }
}