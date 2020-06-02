using ProyectoDineroApi.Data;
using ProyectoDineroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ProyectoDineroApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        IMoneyContext context;

        public UserRepository(IMoneyContext context)
        {
            this.context = context;
        }

        public User Add(User obj)
        {
            obj.Id = context.Users.Any() ? context.Users.Max(o => o.Id) + 1 : 0;
            obj.RoleId = 2;
            obj.Activated = true;
            User user = new User
            {
                Activated = obj.Activated,
                Email = obj.Email,
                Id = obj.Id,
                Name = obj.Name,
                Password = obj.Password,
                PasswordSalt = obj.PasswordSalt,
                RoleId = obj.RoleId,
                Surname = obj.Surname,
                Username = obj.Username
            };
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public void Delete(User obj)
        {
            User userToDelete = this.Get(obj.Id);
            userToDelete.Activated = false;
            context.Users.Remove(userToDelete);
            context.SaveChanges();
        }

        public User Edit(User obj)
        {
            User user = this.GetUserByUsername(obj.Username);
            user.Name = obj.Name;
            user.Surname = obj.Surname;
            user.Username = obj.Username;
            user.Email = obj.Email;
            context.SaveChanges();
            return user;
        }

        public User GetUserByUsername(String userName)
        {
            return context.Users.FirstOrDefault(o => o.Username == userName && o.Activated);
        }

        public User GetUserByEmail(String email)
        {
            return context.Users.FirstOrDefault(o => o.Email == email && o.Activated);
        }

        public User Get(int id)
        {
            return context.Users.FirstOrDefault(o => o.Id == id);
        }

        public IQueryable<User> GetFiltered(Expression<Func<User, bool>> lambda)
        {
            return context.Users.Where(lambda);
        }

        public int GetUserRole(int userId)
        {
            int role = context.Users.FirstOrDefault(i => i.Id == userId).RoleId;
            return role;
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}