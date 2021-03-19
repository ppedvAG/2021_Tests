using System;

namespace ppedv.Zugfahrt.Model
{
    public class Ticket : Entity
    {
        public DateTime Datum { get; set; } = DateTime.Now;
        public decimal Preis { get; set; }
        public virtual Kunde Kunde { get; set; }
        public virtual Zugfahrt Fahrt { get; set; }
    }
}
