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
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Data.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        private UserProfile LoggedUserProfile;
        long UserProfileId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        #region
        public DbSet<AuditEntry> AuditEntries { get; set; }
        #endregion
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


            modelBuilder.Entity<AuditEntry>().Property(ae => ae.Changes).HasConversion(
        value => JsonConvert.SerializeObject(value),
        serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddAuitInfo();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            //AddAuitInfo();

            string userProfileId = _httpContextAccessor.HttpContext.Request.Headers["UserProfileId"];
            if (userProfileId != null)
            {
                UserProfileId = long.Parse(userProfileId);
                //LoggedUserProfile = UserProfiles.SingleOrDefault(x => x.Id == long.Parse(userProfileId));
            }

            // Get audit entries
            var auditEntries = OnBeforeSaveChanges();
            // Save current entity
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            // Save audit entries
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }
        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        #region Audit Helpers
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

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var entries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !(entry.Entity is IEntity))
                    continue;

                var auditEntry = new AuditEntry
                {
                    ActionType = entry.State == EntityState.Added ? "INSERT" : entry.State == EntityState.Deleted ? "DELETE" : "UPDATE",
                    EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString(),
                    EntityName = entry.Metadata.ClrType.Name,
                    UserProfileId=UserProfileId,
                    Username = null,//LoggedUserProfile.FirstName,
                    TimeStamp = DateTime.UtcNow,
                    Changes = entry.Properties.Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue),

                    // TempProperties are properties that are only generated on save, e.g. ID's
                    // These properties will be set correctly after the audited entity has been saved
                    TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList(),
                };

                entries.Add(auditEntry);
            }

            return entries;
        }

        private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.EntityId = prop.CurrentValue.ToString();
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
            }

            AuditEntries.AddRange(auditEntries);
            return SaveChangesAsync();
        }
        #endregion

    }

}
