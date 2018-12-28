using System.Collections.Generic;

namespace UserAdvice.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Post> Posts { get; set; }

        public string UrlSegment { get; set; }
    }
}
