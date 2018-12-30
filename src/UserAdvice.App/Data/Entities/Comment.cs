using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAdvice.Data.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }
        public int? StatusId { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public string Content { get; set; }
    }
}
