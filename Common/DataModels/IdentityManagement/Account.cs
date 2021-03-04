using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DataModels.IdentityManagement
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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
