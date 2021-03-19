using Moq;
using ppedv.Zugfahrt.Model;
using ppedv.Zugfahrt.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ppedv.Zugfahrt.Logic.Tests
{
    public class CoreTests
    {
        [Fact]
        public void Can_CalcFahrtEinnahmen()
        {
            var zf = new Model.Zugfahrt();
            zf.Tickets.Add(new Model.Ticket() { Preis = 8.80m });
            zf.Tickets.Add(new Model.Ticket() { Preis = 2.40m });

            var core = new Core(null);

            var result = core.CalcFahrtEinnahmen(zf);

            Assert.Equal(11.20m, result);
        }

        [Fact]
        public void Can_GetMostValueZugfahrt()
        {
            var core = new Core(new TestRepo());

            var result = core.GetMostValueZugfahrt();

            Assert.Equal("ZF2", result.Start);
        }

        [Fact]
        public void Can_GetMostValueZugfahrt_Moq()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.Query<Model.Zugfahrt>()).Returns(() =>
            {
                var zf1 = new Model.Zugfahrt() { Start = "ZF1" };
                zf1.Tickets.Add(new Ticket() { Preis = 12.80m });

                var zf2 = new Model.Zugfahrt() { Start = "ZF2" };
                zf2.Tickets.Add(new Ticket() { Preis = 8.80m });
                zf2.Tickets.Add(new Ticket() { Preis = 5.80m });

                return new[] { zf1, zf2 }.AsQueryable();
            });
            var core = new Core(mock.Object);

            var result = core.GetMostValueZugfahrt();

            //mock.Verify(x => x.Query<Model.Zugfahrt>(),Times.Exactly(12));

            Assert.Equal("ZF2", result.Start);
        }
    }

    class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Model.Entity
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Model.Entity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Model.Entity
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id) where T : Model.Entity
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Model.Entity
        {
            if (typeof(T) == typeof(Model.Zugfahrt))
            {
                var zf1 = new Model.Zugfahrt() { Start = "ZF1" };
                zf1.Tickets.Add(new Ticket() { Preis = 12.80m });

                var zf2 = new Model.Zugfahrt() { Start = "ZF2" };
                zf2.Tickets.Add(new Ticket() { Preis = 8.80m });
                zf2.Tickets.Add(new Ticket() { Preis = 5.80m });

                return new[] { zf1, zf2 }.Cast<T>().AsQueryable();
            }
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Model.Entity
        {
            throw new NotImplementedException();
        }
    }
}
