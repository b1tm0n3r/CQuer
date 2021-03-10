using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DataModels.StandardEntities
{
    public class FileReference
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string FileName { get; set; }
        [Required]
        [MaxLength(256)]
        public string Path { get; set; }
        public string SHA256Hash { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
    }
}
