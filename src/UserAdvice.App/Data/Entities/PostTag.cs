using System.ComponentModel.DataAnnotations.Schema;

namespace UserAdvice.Data.Entities
{
    public class PostTag
    {
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
}
