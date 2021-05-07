using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DTOs
{
    public class DownloadReferenceDto
    {
        [Required]
        public string DownloadUrl { get; set; }
        [Required]
        public string SHA256Hash { get; set; }
        public DownloadReferenceDto(string downloadUrl, string checksum)
        {
            DownloadUrl = downloadUrl;
            SHA256Hash = checksum;
        }
        public DownloadReferenceDto()
        {

        }
    }
}
