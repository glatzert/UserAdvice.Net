using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserAdvice.Data.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }
        public string BackgroundColor { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
