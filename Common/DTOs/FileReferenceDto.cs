using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class FileReferenceDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TicketId { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Sha256Checksum { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        [Required]
        public bool ChecksumMatchWithDeclared { get; set; }
        [Required]
        public bool ChecksumMatchWithRemote { get; set; }
    }
}