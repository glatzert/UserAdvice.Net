using System;
using System.Collections.Generic;

namespace UserAdvice.ViewModel
{
    public class Post : IVotedPost
    {
        public Post()
        {
            Tags = new List<Tag>();
            Comments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public int VoteCount { get; set; }
        public bool UserVoted { get; set; }

        public Category Category { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
