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

        public DbSet<tbl_tokens> tokens { get; set; }

        public DbSet<tbl_emails> email_logs { get; set; }

        public DbSet<Dealers> Dealers { get; set; }

        public DbSet<Dealer_attachment> dealer_attachment { get; set; }

        public DbSet<Fleet> Fleet { get; set; }

        public DbSet<fleet_provinces> province { get; set; }

        public DbSet<fleet_vehicle_models> fleetVehicleModel { get; set; }

        public DbSet<fleet_vehicle_type> fleet_vehicle_types { get; set; }

        public DbSet<fleet_vehicle_make> fleetVehicleMake { get; set; }

        public DbSet<fleet_vehicle_series> fleetVehicleSeries { get; set; }

        public DbSet<OwnershipTypes> OwnershipTypes { get; set; }

        public DbSet<fleet_vehicle_type_mapping> fleetVehicleMappings { get; set; }

        public DbSet<fleet_colors> fleet_colors { get; set; }

        public DbSet<parts_category> parts_category { get; set; }

        public DbSet<parts_stype> parts_stype { get; set; }

        public DbSet<unit_measures> UnitMeasures { get; set; }

        public DbSet<employees_managers> employee_manager { get; set; }

        public DbSet<employees_local> employee_local { get; set; }

    }
}