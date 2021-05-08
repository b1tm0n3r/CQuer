using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.DataModels.StandardEntities
{
    public class FileReference
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        [Required]
        [MaxLength(256)]
        public string FileName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Path { get; set; }
        public string Sha256Checksum { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        public bool ChecksumMatchWithDeclared { get; set; }
        public bool ChecksumMatchWithRemote { get; set; }
    }
}
