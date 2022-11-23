using Data.Base.Contracts;
using Data.Models;
using Database.Context.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

#nullable disable

namespace Database.Context
{
    public class AcademyContext : DbContext
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod = typeof(AcademyContext)
            .GetMethod(nameof(SetIsDeletedQueryFilter),
            BindingFlags.NonPublic | BindingFlags.Static);

        public AcademyContext()
        {
        }

        public AcademyContext(DbContextOptions<AcademyContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<StudentsSubjects> StudentsSubjects { get; set; }

        public DbSet<LecturersSubjects> LecturersSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /* Build Index for IsDeleted field and add global filter */
            builder.ConfigureIsDeletedIndex();

            var entityTypes = builder.Model.GetEntityTypes().ToList();
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(ISoftDeletableEntity).IsAssignableFrom(et.ClrType));

            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete for soft deletable entities, if you want to disable it for all just change deletableEntityTypes with entityTypes
            var foreignKeys = deletableEntityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var now = DateTime.Now;
            this.ApplyAuditInfoRules(now);
            this.ApplySoftDeleteInfoRules(now);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            this.ApplyAuditInfoRules(now);
            this.ApplySoftDeleteInfoRules(now);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyAuditInfoRules(DateTime now)
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditableEntity &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = now;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entity.ModifiedOn = now;
                }
            }
        }

        private void ApplySoftDeleteInfoRules(DateTime now)
        {
            var deletedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is ISoftDeletableEntity &&
                    (e.State == EntityState.Deleted));

            foreach (var entry in deletedEntries)
            {
                var entity = (ISoftDeletableEntity)entry.Entity;

                entity.DeletedOn = now;
                entity.IsDeleted = true;
            }
        }

        internal static void SetIsDeletedQueryFilter<TEntity>(ModelBuilder builder)
            where TEntity : class, ISoftDeletableEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
