using System.ComponentModel.DataAnnotations;

namespace Common.DTOs
{
    public class UpdateAccountDto
    {
        [Required]
        public string Name { get; set; }
    }
}