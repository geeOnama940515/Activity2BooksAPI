namespace Activity2BooksAPI.Models.DTO
{
    public class AuthorDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
       // public string FullName => $"{FirstName} {LastName}";
    }
}
