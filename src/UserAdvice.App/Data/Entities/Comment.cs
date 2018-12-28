using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAdvice.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public IdentityUser CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public string Content { get; set; }
    }
}
