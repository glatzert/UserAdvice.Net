using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAdvice.Data.Entities
{
    public class UserVote
    {
        [ForeignKey(nameof(VoterId))]
        public AppUser Voter { get; set; }
        public string VoterId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int PostId { get; set; }

        public DateTimeOffset VotedAt { get; set; }
    }
}
