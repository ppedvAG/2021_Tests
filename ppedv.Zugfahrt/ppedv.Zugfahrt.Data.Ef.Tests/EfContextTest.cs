using ppedv.Zugfahrt.Model;
using Xunit;

namespace ppedv.Zugfahrt.Data.Ef.Tests
{
    public class EfContextTest
    {
        [Fact]
        public void Can_create_DB()
        {
            var con = new EfContext();

            if (con.Database.Exists())
                con.Database.Delete();

            con.Database.Create();

            Assert.True(con.Database.Exists());
        }

        [Fact]
        public void Can_CRUD_Ticket()
        {
            var tick = new Ticket() { Preis = 15.55m };
            var newPreis = 12.22m;

            using (var con = new EfContext())
            {
                //INSERT
                con.Tickets.Add(tick);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check INSERT
                var loaded = con.Tickets.Find(tick.Id);
                Assert.NotNull(loaded);
                Assert.Equal(tick.Preis, loaded.Preis);

                //UPDATE
                loaded.Preis = newPreis;
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check UPDATE
                var loaded = con.Tickets.Find(tick.Id);
                Assert.Equal(newPreis, loaded.Preis);

                //DELETE
                con.Tickets.Remove(loaded);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //check DELETE
                var loaded = con.Tickets.Find(tick.Id);
                Assert.Null(loaded);
            }
        }

    }
}
