namespace Activity2BooksAPI.Models.DTO
{
    public class PaginatedResult<T>
    {
        public int TotalRecords { get; set; }
        public int RecordsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public List<T>? Data { get; set; }
    }
}
