using ProyectoDineroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDineroApi.Repositories
{
    public interface IUserRepository
    {
        User Add(User obj);
        void Delete(User obj);
        User Edit(User obj);
        User GetUserByUsername(String userName);
        User GetUserByEmail(String email);
        User Get(int id);
        List<User> GetAll();
        IQueryable<User> GetFiltered(Expression<Func<User, bool>> lambda);
        int GetUserRole(int userId);
    }
}
