namespace CarShop.Data.ModelBuilderExtension
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public static class ModelBuilderExtension
    {
        public static void DisableCascadeDelete(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                                                .ToList();

            var foreignKeys = entityTypes.SelectMany(e => e.GetForeignKeys()
                                         .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public static void SetGlobalQueryFilterForDeletedEntities(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Vehicle>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<VehicleBrand>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<VehicleType>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Issue>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<IssuePriority>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<IssueStatus>().HasQueryFilter(x => !x.IsDeleted);
        }

        internal const string Invalid_Seeder_Injection = "On {0} seeder were invalid database injection!";
    }
}