using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DTOs
{
    public class FileReferenceDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string SHA256Hash { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
    }
}