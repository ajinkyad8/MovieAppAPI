namespace MovieAppAPI.Helpers
{
    public class QueryParams
    {
        private int pageSize = 20;
        private const int MAXPAGESIZE = 50;
        public int pageNumber { get; set; } = 1;
        public int PageSize { get => pageSize; set => pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value; }
        public string search { get; set; }
        public string sortBy { get; set; }
        public bool descending { get; set; } = false;
    }
}