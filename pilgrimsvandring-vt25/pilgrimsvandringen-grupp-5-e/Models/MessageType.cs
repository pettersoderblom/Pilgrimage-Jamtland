using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace pilgrimsvandringen_grupp_5_e.Models
{
    [Index(nameof(Type), IsUnique = true)]
    public class MessageType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public MessageType(string type)
        {
            Type = type;
        }
    }
}
