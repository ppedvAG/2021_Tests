using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.Zugfahrt.Model.Contracts
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        void Add<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        void SaveAll();
    }
}
