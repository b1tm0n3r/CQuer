using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistence.DataModels
{
    public enum AccountType
    {
        Administrator,
        StandardUser
    }
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        public AccountType AccountType { get; set; }
        [Required]
        public string Password { get; set; } //TODO: store hash of pass
    }
}
