using AutoFixture;
using FluentAssertions;
using ppedv.Zugfahrt.Model;
using System;
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

        [Fact]
        public void Can_Kunde_AutoFix_FluentAssertions()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            var kunde = fix.Create<Kunde>();

            using (var con = new EfContext())
            {
                //INSERT
                con.Kunden.Add(kunde);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Kunden.Find(kunde.Id);

                loaded.Name.Should().Be(kunde.Name); //Assert.Equal(kunde.Name, loaded.Name);

                loaded.Should().BeEquivalentTo(kunde, op =>
                {
                    op.Using<DateTime>(cfg => cfg.Subject.Should().BeCloseTo(cfg.Expectation)).WhenTypeIs<DateTime>();
                    op.IgnoringCyclicReferences();
                    return op;
                });
            }
        }


        [Fact]
        public void Can_delete_Kunde_does_delete_Tickets()
        {
            var tick1 = new Ticket();
            var tick2 = new Ticket();
            var tick3 = new Ticket();

            var kunde = new Kunde();
            kunde.Tickets.Add(tick1);
            kunde.Tickets.Add(tick2);
            kunde.Tickets.Add(tick3);

            //ticket mit Kunde speichern
            using (var con = new EfContext())
            {
                con.Kunden.Add(kunde);
                con.SaveChanges();
            }

            //delete Kunde 
            using (var con = new EfContext())
            {
                var loaded = con.Kunden.Find(kunde.Id);
                loaded.Tickets.Count.Should().Be(3); //verify tickets added
                con.Kunden.Remove(loaded);
                con.SaveChanges();
            }

            //test 
            using (var con = new EfContext())
            {
                var loadedKunde = con.Kunden.Find(kunde.Id);
                loadedKunde.Should().BeNull();

                con.Tickets.Find(tick1.Id).Should().BeNull();
                con.Tickets.Find(tick2.Id).Should().BeNull();
                con.Tickets.Find(tick3.Id).Should().BeNull();
            }

        }

        [Fact]
        public void Can_delete_Ticket_does_NOT_delete_Kunde()
        {
            var tick1 = new Ticket();
            var tick2 = new Ticket();
            var tick3 = new Ticket();

            var kunde = new Kunde();
            kunde.Tickets.Add(tick1);
            kunde.Tickets.Add(tick2);
            kunde.Tickets.Add(tick3);

            //ticket mit Kunde speichern
            using (var con = new EfContext())
            {
                con.Kunden.Add(kunde);
                con.SaveChanges();
            }

            //delete Ticket 
            using (var con = new EfContext())
            {
                var loaded = con.Tickets.Find(tick2.Id);
                con.Tickets.Remove(loaded);
                con.SaveChanges();
            }

            //test 
            using (var con = new EfContext())
            {
                var loadedKunde = con.Kunden.Find(kunde.Id);
                loadedKunde.Should().NotBeNull();

                con.Tickets.Find(tick1.Id).Should().NotBeNull();
                con.Tickets.Find(tick2.Id).Should().BeNull();
                con.Tickets.Find(tick3.Id).Should().NotBeNull();
            }
        }

    }
}
