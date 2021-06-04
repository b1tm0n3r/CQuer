using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class DownloadReferenceDto
    {
        [Required]
        public int TicketId { get; set; }
        [Required]
        public string DownloadUrl { get; set; }
        public string Sha256Checksum { get; set; }
    }
}
