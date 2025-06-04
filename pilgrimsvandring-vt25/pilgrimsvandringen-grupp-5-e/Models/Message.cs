using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace pilgrimsvandringen_grupp_5_e.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime? DateTime { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MessageTypeId { get; set; }
        [Required]
        public int UserId { get; set; }
        public MessageType MessageType { get; set; }
        public User User { get; set; }

    }
}
