using PagedList;
using ProyectoDineroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDineroApi.Repositories
{
    public interface IRepository<T>
    {
        T Get(int id);

        List<T> GetAll();
        List<T> GetAllWithoutParam();

        List<T> GetFiltered(Expression<Func<T, bool>> expr);

        T Add(T obj);

        T Edit(T obj);

        void Delete(T obj);

    }
}
