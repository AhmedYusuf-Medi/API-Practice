namespace CarShop.Data
{
    using CarShop.Data.ModelBuilderExtension;
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    public class CarShopDbContext : DbContext
    {
        public CarShopDbContext(DbContextOptions<CarShopDbContext> options)
        : base(options)
        { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<IssueStatus> IssueStatuses { get; set; }
        public DbSet<IssuePriority> IssuePriorities { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.SetGlobalQueryFilterForDeletedEntities();

            modelBuilder.DisableCascadeDelete();
        }

        public override int SaveChanges()
        {
            ApplyEntityRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                                  CancellationToken cancellationToken = default(CancellationToken))
        {
            ApplyEntityRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyEntityRules()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues["CreatedOn"] = DateTime.Now;
                    entry.CurrentValues["IsDeleted"] = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues["ModifiedOn"] = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                    entry.CurrentValues["DeletedOn"] = DateTime.Now;
                }
            }
        }

        public void Undelete(IDeletable entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }
    }
}