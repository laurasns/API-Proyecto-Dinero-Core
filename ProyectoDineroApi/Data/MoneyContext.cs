using Microsoft.EntityFrameworkCore;
using ProyectoDineroApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDineroApi.Data
{
    public class MoneyContext: DbContext, IMoneyContext
    {
        public MoneyContext(DbContextOptions<MoneyContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Calculator> Calculations { get; set; }

    }
}
