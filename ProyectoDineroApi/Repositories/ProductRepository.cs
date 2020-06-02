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
    public class ProductRepository : IRepository<Product>
    {
        IMoneyContext context;

        public ProductRepository(IMoneyContext context)
        {
            this.context = context;
        }
        public Product Add(Product obj)
        {
            obj.Id = context.Products.Max(o => o.Id) + 1;
            Product prod = new Product
            {
                Code = obj.Code,
                Id = obj.Id,
                Name = obj.Name,
                Type = obj.Type
            };
            context.Products.Add(prod);
            context.SaveChanges();
            return prod;
        }

        public void Delete(Product obj)
        {
            Product prod = this.Get(obj.Id);
            context.Products.Remove(prod);
            context.SaveChanges();
        }

        public Product Edit(Product obj)
        {
            Product prod = this.Get(obj.Id);
            prod.Name = obj.Name;
            prod.Code = obj.Code;
            prod.Type = obj.Type;
            context.SaveChanges();
            return prod;
        }

        public Product Get(int id)
        {
            return context.Products.FirstOrDefault(prod => prod.Id == id);
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllWithoutParam()
        {
            return context.Products.ToList();
        }

        public List<Product> GetFiltered(Expression<Func<Product, bool>> expr)
        {
            return context.Products.Where(expr).ToList();
        }
    }
}