using System;
using System.Collections.Generic;

namespace UserAdvice.ViewModel
{
    public class PostTeaser
    {
        public PostTeaser()
        {
            Tags = new List<Tag>();
            StatusComments = new List<Comment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string VoteCount { get; set; }
        public string CommentCount { get; set; }

        public bool UserVoted { get; set; }

        public CategoryRef Category { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Comment> StatusComments { get; set; }
    }
}
