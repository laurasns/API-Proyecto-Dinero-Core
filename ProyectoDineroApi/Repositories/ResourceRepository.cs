using PagedList;
using ProyectoDineroApi.Data;
using ProyectoDineroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ProyectoDineroApi.Repositories
{
    public class ResourceRepository : IRepository<Resource>
    {
        IMoneyContext context;

        public ResourceRepository(IMoneyContext context)
        {
            this.context = context;
        }
        public Resource Add(Resource obj)
        {
            obj.Id = this.context.Resources.Max(o => o.Id) + 1;
            Resource res = new Resource
            {
                Author = obj.Author,
                Description = obj.Description,
                Id = obj.Id,
                Image = obj.Image,
                Name = obj.Name,
                Type = obj.Type,
                Url = obj.Url
            };
            context.Resources.Add(res);
            context.SaveChanges();
            return res;
        }

        public void Delete(Resource obj)
        {
            Resource resource = this.Get(obj.Id);
            context.Resources.Remove(resource);
            context.SaveChanges();
        }

        public Resource Edit(Resource obj)
        {
            Resource resource = this.Get(obj.Id);
            resource.Image = obj.Image;
            resource.Name = obj.Name;
            resource.Type = obj.Type;
            resource.Url = obj.Url;
            context.SaveChanges();
            return resource;
        }

        public Resource Get(int id)
        {
            return this.context.Resources.FirstOrDefault(res => res.Id == id);
        }

        public List<Resource> GetAll()
        {
            return this.context.Resources.ToList();
        }

        public List<Resource> GetAllWithoutParam()
        {
            throw new NotImplementedException();
        }

        public List<Resource> GetFiltered(Expression<Func<Resource, bool>> expr)
        {
            return this.context.Resources.Where(expr).ToList();
        }
    }
}