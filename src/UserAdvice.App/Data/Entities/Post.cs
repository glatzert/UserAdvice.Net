using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAdvice.Data.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public AppUser CreatedBy { get; set; }
        public string CreatedById { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTimeOffset? ClosedAt { get; set; }
        
        public List<PostTag> PostTags { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int? CategoryId { get; set; }

        
        public int VoteCount { get; set; }
        public List<UserVote> Votes { get; set; }

        public int CommentCount { get; set; }
        public List<Comment> Comments { get; set; }


        [ForeignKey(nameof(DuplicateOfId))]
        public int? DuplicateOfId { get; set; }
        public Post DuplicateOf { get; set; }
    }
}
