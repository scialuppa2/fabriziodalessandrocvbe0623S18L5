using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace progetto_settimanaleS18L5.Models
{
    public class Utente
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}