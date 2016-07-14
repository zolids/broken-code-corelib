using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Models
{
    public class dbContext : DbContext
    {
        public dbContext() : base("dbcontext_fsxcv"){

            Database.SetInitializer<dbContext>(null);

        }

        public DbSet<Users> Users { get; set; }
    }
}