using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using UserAdvice.Configuration;
using UserAdvice.Queries;
using UserAdvice.Queries.ViewModel;

namespace UserAdvice.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IQueryHandler<PostTeaserQuery, List<ViewModel.PostTeaser>> _postTeaserHandler;

        public IndexModel(
            IOptions<IndexOptions> options,
            IQueryHandler<PostTeaserQuery, List<ViewModel.PostTeaser>> postTeaserHandler)
        {
            Options = options.Value;
            _postTeaserHandler = postTeaserHandler;
        }

        [BindProperty(Name = "page", SupportsGet = true)]
        public int? PageNumber { get; set; }

    
        public IndexOptions Options { get; }

        public List<ViewModel.PostTeaser> Posts { get; } = new List<ViewModel.PostTeaser>();
        public List<ViewModel.Category> Categories { get; } = new List<ViewModel.Category>();


        public async Task OnGet(int? categoryId)
        {
            if (!Options.HidePosts)
            {
                var query = new PostTeaserQuery(categoryId, User)
                {
                    IgnoreCategoryIfNull = Options.IncludeAllCategories,
                    PageNumber = PageNumber ?? 0
                };

                var result = await _postTeaserHandler.Execute(query);

                if (result == null)
                    NotFound();

                Posts.AddRange(result);
            }
        }
    }
}
