using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserAdvice.Queries;
using UserAdvice.Queries.ViewModel;

namespace UserAdvice.Web.Pages
{
    public class PostModel : PageModel
    {
        private readonly IQueryHandler<PostQuery, ViewModel.Post> _postHandler;

        public PostModel(
            IQueryHandler<PostQuery, ViewModel.Post> postHandler)
        {
            _postHandler = postHandler;
        }

        public ViewModel.Post Post { get; set; }

        public async Task OnGet(int postId)
        {
            await LoadPost(postId);
            if (Post == null)
            {
                NotFound();
                return;
            }
        }

        private async Task LoadPost(int postId)
        {
            Post = await _postHandler.Execute(new PostQuery(postId, User));
        }
    }
}