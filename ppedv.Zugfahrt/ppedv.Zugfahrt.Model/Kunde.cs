using System.Collections.Generic;

namespace ppedv.Zugfahrt.Model
{
    public class Kunde : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();
    }
}
