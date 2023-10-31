using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;
using Data.Entities.Shared;
using Data.Entities.UserManagement;
using Data.Entities.Setup;
using Data.Entities.Purchases;
using Data.Entities.Sales;

namespace Data.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        #region Setup 
        public DbSet<Advertisment> Advertisments { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ClientVendor> ClientVendors { get; set; }
        #endregion

        #region User Management
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        #endregion

        #region Purchases
        public DbSet<PurchasesBillHeader> PurchasesBillHeaders { get; set; }
        public DbSet<PurchasesBillDetail> PurchasesBillDetails { get; set; }
        #endregion

        #region Sales
        public DbSet<SalesBillHeader> SalesBillHeaders { get; set; }
        public DbSet<SalesBillDetail> SalesBillDetails { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientVendor>()
           .HasIndex(p => new { p.FullName })
           .IsUnique(true);

            //modelBuilder.Entity<RolePrivilege>()
            //.HasNoKey();


            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }
        private void AddAuitInfo()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.UtcNow;
                }
            ((BaseEntity)entry.Entity).Modified = DateTime.UtcNow;
            }
        }

    }

}
