namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });


            builder.HasOne(u => u.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(u => u.UserId);

            builder.HasOne(ur => ur.Role)
                   .WithMany(ur => ur.Users)
                   .HasForeignKey(ur => ur.RoleId);             
        }
    }
}