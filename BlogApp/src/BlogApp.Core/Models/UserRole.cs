using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RoleId"), Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        [ForeignKey("UserId"), Required]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}