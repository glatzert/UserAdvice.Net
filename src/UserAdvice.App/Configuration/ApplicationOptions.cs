using UserAdvice.ViewModel;

namespace UserAdvice.Configuration
{
    public class ApplicationOptions : ICategory
    {
        public ApplicationOptions()
        {
            IndexOptions = new IndexOptions();
        }

        public bool IPurchasedALicenseAndWantToRemoveTheFooter { get; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }

        public IndexOptions IndexOptions { get; set; }
    }

    public class IndexOptions
    {
        public bool HidePosts { get; set; }
        public bool IncludeAllCategories { get; set; }
    }
}
