using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CW1_Cloud_MVC_7624.Models;

namespace CW1_Cloud_MVC_7624.Data
{
    public class CW1_Cloud_MVC_7624Context : DbContext
    {
        public CW1_Cloud_MVC_7624Context (DbContextOptions<CW1_Cloud_MVC_7624Context> options)
            : base(options)
        {
        }

        public DbSet<CW1_Cloud_MVC_7624.Models.Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
