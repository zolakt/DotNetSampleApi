namespace SampleApp.Common.DAL.Repositories
{
    public class PaginationOptions
    {
        public int? Limit { get; set; }

        public int? Offset { get; set; }

        private int? _currentPage = 1;

        public int? CurrentPage
        {
            get
            {
                if (!_currentPage.HasValue && Offset.HasValue && Limit > 0)
                {
                    _currentPage = Offset / Limit;
                }

                return _currentPage;
            }
            set
            {
                _currentPage = value;

                if (!Offset.HasValue)
                {
                    Offset = _currentPage * Limit;
                }
            }
        }
    }
}
