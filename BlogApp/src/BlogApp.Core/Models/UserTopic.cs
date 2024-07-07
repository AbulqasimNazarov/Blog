using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public class UserTopic
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TopicId"), Required]
        public int TopicId { get; set; }
        public Topic? Topic { get; set; }
        [ForeignKey("UserId"), Required]
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}