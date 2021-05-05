using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DTOs
{
    public class TicketDto
    {       
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 5)]
        public int Severity { get; set; }
        [Required]
        public bool Solved { get; set; }
        [Required]
        public string DownloadUrl { get; set; }
        public string Sha256Checksum { get; set; }
    }
}
