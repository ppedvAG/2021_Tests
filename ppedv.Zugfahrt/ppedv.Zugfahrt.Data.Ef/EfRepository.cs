using ppedv.Zugfahrt.Model;
using ppedv.Zugfahrt.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.Zugfahrt.Data.Ef
{
    public class EfRepository : IRepository
    {
        EfContext con = new EfContext();

        public void Add<T>(T entity) where T : Entity
        {
            //if (entity is Kunde)
            //    con.Kunden.Add(entity as Kunde);
            con.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            con.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return con.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return con.Set<T>().Find(id);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return con.Set<T>();
        }

        public void SaveAll()
        {
            con.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            var loaded = GetById<T>(entity.Id);
            if (loaded != null)
                con.Entry(loaded).CurrentValues.SetValues(entity);
        }
    }
}
