namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(ValidationConstants.User_Role)]
    public class UserRole : DeletableJoinEntity
    {
        [Key]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Key]
        public long RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}