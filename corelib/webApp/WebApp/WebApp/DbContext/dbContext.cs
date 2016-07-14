using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class dbContext : DbContext
    {
        public dbContext() : base("dbcontext_fsxcv"){

            Database.SetInitializer<dbContext>(null);

        }
    }
}