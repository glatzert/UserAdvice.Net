using System;
using System.Collections.Generic;
using System.Text;

namespace UserAdvice.Configuration
{
    public class ApplicationOptions
    {
        public IndexOptions IndexOptions { get; set; }
    }

    public class IndexOptions
    {
        public bool HidePosts { get; set; }
        public bool IncludeAllCategories { get; set; }
    }
}
