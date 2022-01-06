namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });


            builder.HasOne(userRole => userRole.User)
                   .WithMany(role => role.Roles)
                   .HasForeignKey(userRole => userRole.UserId);

            builder.HasOne(userRole => userRole.Role)
                   .WithMany(role => role.Users)
                   .HasForeignKey(userRole => userRole.RoleId);             
        }
    }
}