using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserAdvice.Configuration;
using UserAdvice.Data;
using UserAdvice.Data.Mapping;
using UserAdvice.Extensions;
using UserAdvice.ViewModel;

namespace UserAdvice.Queries.ViewModel
{
    public class PostTeaserQuery : PagedQuery, IQuery<List<PostTeaser>>
    {
        public PostTeaserQuery(int? categoryId, ClaimsPrincipal currentUser)
        {
            if (categoryId.HasValue && categoryId <= 0)
                throw new ArgumentOutOfRangeException();

            CategoryId = categoryId;
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public int? CategoryId { get; }
        public ClaimsPrincipal CurrentUser { get; }

        public bool IgnoreCategoryIfNull { get; set; }
    }

    internal class PostTeaserQueryHandler : IQueryHandler<PostTeaserQuery, List<PostTeaser>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationOptions _options;

        public PostTeaserQueryHandler(ApplicationDbContext dbContext,
            IOptions<ApplicationOptions> options)
        {
            _dbContext = dbContext;
            _options = options.Value;
        }

        public async Task<List<PostTeaser>> Execute(PostTeaserQuery query)
        {
            var ignoreCategory = !query.CategoryId.HasValue && query.IgnoreCategoryIfNull;
            var username = query.CurrentUser.FindFirstValue("name");

            var posts = (await _dbContext.Posts
                .AsNoTracking()
                .Include(x => x.PostTags)
                    .ThenInclude(x => x.Tag)
                .Where(x => ignoreCategory || x.CategoryId == query.CategoryId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(query.PageSize * query.PageNumber)
                .Take(query.PageSize)
                .ToListAsync())
                .ToDictionary(x => x.Id);

            var statusComments = (await _dbContext.Comments
                .AsNoTracking()
                .Include(x => x.CreatedBy)
                .Where(x => x.StatusId.HasValue && posts.Keys.Contains(x.PostId))
                .ToListAsync()).ToLookup(x => x.PostId);

            foreach(var comments in statusComments)
                posts[comments.Key].Comments = comments.ToList();

            var result = posts.Select(x => x.Value.ToViewModel());
            
            if (query.CurrentUser.IsAuthenticated())
            {
                var uservotes = await _dbContext.UserVotes
                    .AsNoTracking()
                    .Where(x => x.VoterId == username)
                    .Where(x => posts.Keys.Contains(x.PostId))
                    .Select(x => x.PostId)
                    .ToListAsync();

                foreach (var r in result)
                    r.UserVoted = uservotes.Contains(r.Id);
            }

            return result.ToList();
        }
    }
}
