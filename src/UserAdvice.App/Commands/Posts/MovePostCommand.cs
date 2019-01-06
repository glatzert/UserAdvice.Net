using System;
using System.Security.Claims;

namespace UserAdvice.Commands.Posts
{
    public class MovePostCommand
    {
        public MovePostCommand(int postId, int? categoryId,
            ClaimsPrincipal currentUser)
        {
            if (postId <= 0)
                throw new ArgumentOutOfRangeException(nameof(postId));
            if (categoryId.HasValue && categoryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(categoryId));

            PostId = postId;
            CategoryId = categoryId;
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public int PostId { get; }
        public int? CategoryId { get; }

        public ClaimsPrincipal CurrentUser { get; }
    }
}
