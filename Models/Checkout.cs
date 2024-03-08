using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace progetto_settimanaleS18L5.Models
{
    public class Checkout
    {
        public string NomeTitolare { get; set; }
        public int NumeroStanza { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public decimal TariffaApplicata { get; set; }
        public decimal CaparraConfirmatoria { get; set; }
        public List<ServizioAggiuntivo> ServiziAggiuntivi { get; set; }
        public decimal ImportoDaSaldare { get; set; }
    }
}