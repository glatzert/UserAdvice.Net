using System.Security.Claims;

namespace UserAdvice.Commands.Posts
{
    public class CreatePostCommand
    {
        public CreatePostCommand(ClaimsPrincipal currentUser)
        {
            CurrentUser = currentUser ?? throw new System.ArgumentNullException(nameof(currentUser));
        }

        public string Title { get; set; }
        public string Content { get; set; }

        public int? CategoryId { get; set; }

        public ClaimsPrincipal CurrentUser { get; set; }
    }
}
