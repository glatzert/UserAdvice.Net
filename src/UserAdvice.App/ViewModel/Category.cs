namespace UserAdvice.ViewModel
{
    public class Category
    {
        public int Id { get; set; }

        public string UrlSegment { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string LogoUrl { get; set; }

        public int SortKey { get; set; }
    }

    public class CategoryRef
    {
        public int Id { get; set; }

        public string UrlSegment { get; set; }

        public string Name { get; set; }
    }
}
