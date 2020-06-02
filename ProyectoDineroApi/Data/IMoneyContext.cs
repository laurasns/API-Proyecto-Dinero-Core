
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoDineroApi.Models;

namespace ProyectoDineroApi.Data
{
    public interface IMoneyContext
    {
         DbSet<User> Users { get; set; }
         DbSet<Product> Products { get; set; }
         DbSet<Resource> Resources { get; set; }
         DbSet<Role> Roles { get; set; }
         DbSet<Calculator> Calculations { get; set; }
        int SaveChanges();
    }
}
