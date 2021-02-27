using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.DataModels.StandardEntities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int CreatorId { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
    }
}
