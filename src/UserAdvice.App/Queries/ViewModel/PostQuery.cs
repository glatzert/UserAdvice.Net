using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserAdvice.Configuration;
using UserAdvice.Data;
using Entities = UserAdvice.Data.Entities;
using UserAdvice.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserAdvice.Data.Mapping;
using UserAdvice.Extensions;

namespace UserAdvice.Queries.ViewModel
{
    public class PostQuery : IQuery<Post>
    {
        public PostQuery(int postId, ClaimsPrincipal currentUser)
        {
            if(postId <= 0)
                throw new ArgumentOutOfRangeException(nameof(postId));

            PostId = postId;
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public int PostId { get; }
        public ClaimsPrincipal CurrentUser { get; }
    }

    internal class PostQueryHandler : IQueryHandler<PostQuery, Post>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IOptions<ApplicationOptions> _options;
        private readonly UserManager<Entities.AppUser> _userManager;

        public PostQueryHandler(ApplicationDbContext dbContext,
            IOptions<ApplicationOptions> options, UserManager<Entities.AppUser> userManager)
        {
            _dbContext = dbContext;
            _options = options;
            _userManager = userManager;
        }

        public async Task<Post> Execute(PostQuery query)
        {
            var post = await _dbContext.Posts
                .AsNoTracking()
                .Include(x => x.CreatedBy)
                .Include(x => x.PostTags)
                    .ThenInclude(x => x.Tag)
                .Include(x => x.Category)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x => x.Id == query.PostId);
            
            if (post == null)
                return null;

            post.Comments = await _dbContext.Comments
                .AsNoTracking()
                .Include(x => x.CreatedBy)
                .Include(x => x.Status)
                .Where(x => x.PostId == query.PostId)
                .ToListAsync();

            var result = post.ToViewModel();

            if (query.CurrentUser.IsAuthenticated())
            {
                var user = await _userManager.GetUserAsync(query.CurrentUser);

                result.UserVoted = await _dbContext.UserVotes
                    .AsNoTracking()
                    .AnyAsync(x => x.VoterId == user.Id && x.PostId == query.PostId);
            }

            if (!query.CurrentUser.IsAuthenticated())
            {
                //TODO: Remove non public Tags
            }

            return result;
        }
    }
}
