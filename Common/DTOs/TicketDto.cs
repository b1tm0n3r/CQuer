using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs
{
    class TicketDto
    {
        public int CreatorId { get; set; }
        public string Description { get; set; }
        public int Severity { get; set; }
        public bool Solved { get; set; }
        public string DownloadUrl { get; set; }
        public string Sha256Checksum { get; set; }
    }
}
