using ppedv.Zugfahrt.Model.Contracts;
using System;
using System.Linq;

namespace ppedv.Zugfahrt.Logic
{
    public class Core
    {
        public IRepository Repository { get; }

        public Core(IRepository repository)
        {
            Repository = repository;
        }

        public decimal CalcFahrtEinnahmen(Model.Zugfahrt fahrt)
        {
            //komplexe Business Logic
            return fahrt.Tickets.Sum(x => x.Preis);
        }

        public Model.Zugfahrt GetMostValueZugfahrt()
        {
            return Repository.Query<Model.Zugfahrt>().OrderBy(x => x.Tickets.Sum(y => y.Preis)).FirstOrDefault();
        }
    }
}
