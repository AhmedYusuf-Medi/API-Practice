namespace CarShop.Data.ModelBuilderExtension.EntityConfigurations
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(user => user.Username)
                   .IsUnique();

            builder.HasIndex(user => user.Email)
                   .IsUnique();
        }
    }
}