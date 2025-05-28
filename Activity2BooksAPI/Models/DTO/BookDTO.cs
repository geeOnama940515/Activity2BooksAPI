namespace Activity2BooksAPI.Models.DTO
{


    public class BookDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<int> AuthorIds { get; set; } = new();
    }

    public class BookDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> AuthorNames { get; set; } = new(); // <-- instead of AuthorIds
    }
}
