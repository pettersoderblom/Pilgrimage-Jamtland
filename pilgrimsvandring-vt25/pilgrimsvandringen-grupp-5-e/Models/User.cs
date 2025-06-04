using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace pilgrimsvandringen_grupp_5_e.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public virtual ApplicationUser IdentityUser{ get; set; }
    }
}
