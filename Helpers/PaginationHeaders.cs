namespace MovieAppAPI.Helpers
{
    public class PaginationHeaders
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public PaginationHeaders(int pageNumber, int pageSize, int totalPages, int totalItems)
        {
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}