using System;
using System.Collections.Generic;
using System.Text;
using UserAdvice.ViewModel;

namespace UserAdvice.Configuration
{
    public class ApplicationOptions
    {
        public IndexOptions IndexOptions { get; set; }
    }

    public class IndexOptions : ICategory
    {
        public bool HidePosts { get; set; }
        public bool IncludeAllCategories { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string LogoUrl { get; set; }
    }
}
