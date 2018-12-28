using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UserAdvice.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public IdentityUser CreatedBy { get; set; }
        public string CreatedById { get; set; }

        public string Content { get; set; }

        public List<PostTag> PostTags { get; set; }

        [ForeignKey(nameof(Category))]
        public Category Category { get; set; }
        public int? CategoryId { get; set; }
    }
}
