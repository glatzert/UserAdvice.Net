using System;

namespace UserAdvice.ViewModel
{
    public class Comment
    {
        public string Content { get; set; }
        
        public Status Status { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
