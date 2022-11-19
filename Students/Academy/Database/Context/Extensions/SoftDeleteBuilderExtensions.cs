using Data.Base.Contracts;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Database.Context.Extensions
{
    internal static class SoftDeleteBuilderExtensions
    {
        internal static void ConfigureIsDeletedIndex(this ModelBuilder modelBuilder)
        {
            var deletableEntityTypes = modelBuilder.Model
                .GetEntityTypes()
                .Where(et => et.ClrType != null && typeof(ISoftDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(ISoftDeletableEntity.IsDeleted));
            }
        }
    }
}
