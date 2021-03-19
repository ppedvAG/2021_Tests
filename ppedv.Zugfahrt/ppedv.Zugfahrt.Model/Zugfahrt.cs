using System;
using System.Collections.Generic;

namespace ppedv.Zugfahrt.Model
{
    public class Zugfahrt : Entity
    {
        public DateTime Datum { get; set; } = DateTime.Now;
        public string Start { get; set; }
        public string Ende { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
