using System;
using System.Collections.Generic;

namespace UserAdvice.ViewModel
{
    public class PostTeaser
    {
        public PostTeaser()
        {
            Tags = new List<TagRef>();
            StatusComments = new List<StatusComment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string VoteCount { get; set; }
        public string CommentCount { get; set; }

        public bool UserVoted { get; set; }

        public CategoryRef Category { get; set; }

        public List<TagRef> Tags { get; set; }

        public List<StatusComment> StatusComments { get; set; }
        
        public class StatusComment
        {
            public string Content { get; set; }

            public string StatusName { get; set; }
            public string ForegroundColor { get; set; }

            public string CreatedBy { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
        }
    }
}
