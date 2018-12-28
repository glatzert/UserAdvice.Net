using System.Collections.Generic;

namespace UserAdvice.Data.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }
        public string Color { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
