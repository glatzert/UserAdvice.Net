using System;
using System.Security.Claims;

namespace UserAdvice.Commands.Posts
{
    public class RemoveTagCommand
    {
        public RemoveTagCommand(int postId, int tagId,
            ClaimsPrincipal currentUser)
        {
            if (postId <= 0)
                throw new ArgumentOutOfRangeException(nameof(postId));
            if (tagId <= 0)
                throw new ArgumentOutOfRangeException(nameof(tagId));

            PostId = postId;
            TagId = tagId;

            CurrentUser = currentUser ?? throw new System.ArgumentNullException(nameof(currentUser));
        }

        public int PostId { get; }
        public int TagId { get; }
        public ClaimsPrincipal CurrentUser { get; }
    }
}
