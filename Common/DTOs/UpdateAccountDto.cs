using System.ComponentModel.DataAnnotations;
using Common.DataModels.IdentityManagement;

namespace Common.DTOs
{
    public class UpdateAccountDto
    {
        [Required]
        public string Name { get; set; }
    }
}