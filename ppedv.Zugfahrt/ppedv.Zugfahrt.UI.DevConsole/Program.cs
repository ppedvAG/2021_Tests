using ppedv.Zugfahrt.Data.Ef;
using ppedv.Zugfahrt.Logic;
using System;
using System.Linq;
using System.Text;

namespace ppedv.Zugfahrt.UI.DevConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Hello World!");

            var core = new Core(new EfRepository());

            foreach (var zf in core.Repository.Query<Model.Zugfahrt>().Where(x => x.Datum.Month < 15).ToList())
            {
                Console.WriteLine($"{zf.Start} - { zf.Ende} {zf.Datum:dd.MMM yyyy}");
                foreach (var t in zf.Tickets)
                {
                    Console.WriteLine($"\t{t.Kunde.Name} {t.Preis:c}");
                }
            }


            Console.ReadLine();
        }
    }
}
