using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserAdvice.Data;
using UserAdvice.Data.Mapping;
using UserAdvice.ViewModel;

namespace UserAdvice.Queries.ViewModel
{
    public class CategoryQuery : IQuery<List<Category>>
    {
        public CategoryQuery(ClaimsPrincipal currentUser)
        {
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public ClaimsPrincipal CurrentUser { get; }
    }

    internal class CategoryQueryHandler : IQueryHandler<CategoryQuery, List<Category>>
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> Execute(CategoryQuery query)
        {
            var categories = await _dbContext.Categories
                .ToListAsync();

            return categories.Select(x => x.ToViewModel()).ToList();
        }
    }
}
