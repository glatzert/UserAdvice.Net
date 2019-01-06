using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAdvice.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<Post> Posts { get; set; }

        public string UrlSegment { get; set; }

        public int PostCount { get; set; }
    }
}
