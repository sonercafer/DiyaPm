using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DiyaPM.UI.Models
{
    public class DiyaPMContext : DbContext
    {
        public DiyaPMContext() : base("DiyaPMContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DiyaPMContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}