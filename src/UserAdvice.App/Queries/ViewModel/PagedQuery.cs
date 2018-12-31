namespace UserAdvice.Queries.ViewModel
{
    public abstract class PagedQuery
    {
        private int _pageNumber;
        private int _pageSize = Constants.DefaultPageSize;

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (_pageSize <= 0 || _pageSize > Constants.MaxPageSize)
                    _pageSize = Constants.DefaultPageSize;
                else
                    _pageSize = value;
            }
        }
        public int PageNumber
        {
            get => _pageNumber;

            set
            {
                if (value >= 0)
                    _pageNumber = value;
            }
        }
    }
}