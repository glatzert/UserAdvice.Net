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
        private readonly IQueryHandler<CategoryQuery, List<ViewModel.Category>> _categoryHandler;
        private readonly IQueryHandler<PostTeaserQuery, List<ViewModel.PostTeaser>> _postTeaserHandler;

        public IndexModel(
            IOptions<ApplicationOptions> options,
            IQueryHandler<CategoryQuery, List<ViewModel.Category>> categoryHandler,
            IQueryHandler<PostTeaserQuery, List<ViewModel.PostTeaser>> postTeaserHandler)
        {
            Options = options.Value.IndexOptions;
            _categoryHandler = categoryHandler;
            _postTeaserHandler = postTeaserHandler;
        }

        [BindProperty(Name = "page", SupportsGet = true)]
        public int? PageNumber { get; set; }

    
        public IndexOptions Options { get; }

        public List<ViewModel.PostTeaser> Posts { get; } = new List<ViewModel.PostTeaser>();
        public ViewModel.Category CurrentCategory { get; private set; }

        public List<ViewModel.Category> Categories { get; } = new List<ViewModel.Category>();

        public async Task OnGet(string categoryUrl)
        {
            await LoadCategories(categoryUrl);
            if(!string.IsNullOrEmpty(categoryUrl) && CurrentCategory == null)
            {
                NotFound();
                return;
            }

            if (CurrentCategory != null || !Options.HidePosts)
            {
                await LoadPosts();
            }
        }

        public async Task LoadCategories(string categoryUrl)
        {
            var query = new CategoryQuery(User);

            var result = await _categoryHandler.Execute(query);
            
            if(!string.IsNullOrEmpty(categoryUrl))
            {
                CurrentCategory = result
                    .FirstOrDefault(x => string.Equals(x.UrlSegment, categoryUrl));
            }

            Categories.AddRange(result);
        }

        public async Task LoadPosts()
        {
            var query = new PostTeaserQuery(CurrentCategory?.Id, User)
            {
                IgnoreCategoryIfNull = !Options.IncludeAllCategories,
                PageNumber = PageNumber ?? 0
            };

            var result = await _postTeaserHandler.Execute(query);
            Posts.AddRange(result);
        }
    }
}
