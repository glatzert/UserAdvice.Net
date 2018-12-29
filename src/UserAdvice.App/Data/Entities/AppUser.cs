using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace UserAdvice.Data.Entities
{
    public class AppUser : IdentityUser
    {
        public List<UserVote> UserVotes { get; set; }

        public int VoteCount { get; set; }
    }
}
