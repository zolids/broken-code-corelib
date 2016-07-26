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

        public DbSet<wp_pages> WPPages { get; set; }

        public DbSet<gsa_user_pages> gsa_user_pages { get; set; }

        public DbSet<tbl_projects> projects { get; set; }

        public DbSet<tbl_config> config { get; set; }

        public DbSet<tbl_requests> requests { get; set; }

        public DbSet<tbl_emails> email_logs { get; set; }

        public DbSet<Dealers> Dealers { get; set; }

        public DbSet<Dealer_attachment> dealer_attachment { get; set; }

        public DbSet<Fleet> Fleet { get; set; }

    }
}