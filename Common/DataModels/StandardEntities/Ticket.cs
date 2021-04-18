using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DataModels.StandardEntities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int CreatorId { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        [Required]
        [Range(1,5)]
        public int Severity { get; set; }
        [Required]
        public bool Solved { get; set; }
        [Required]
        public string DownloadUrl { get; set; }
        public string Sha256Checksum { get; set; }
    }
}
