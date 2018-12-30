using System.ComponentModel.DataAnnotations;

namespace UserAdvice.Data.Entities
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string ForegroundColor { get; set; }

        public bool IsClosing { get; set; }
        public bool IsPublic { get; set; }
    }
}
