using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Core.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Text { get; set; }
        [ForeignKey("TopicId"), Required]
        public int? TopicId { get; set; }
        [ForeignKey("UserId"), Required]
        public int? UserId { get; set; }
        public string? PictureUrl { get; set; } 
    }
}