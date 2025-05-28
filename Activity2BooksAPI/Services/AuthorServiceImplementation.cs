using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;
using System.Xml.Linq;

namespace Activity2BooksAPI.Services
{
    public class AuthorServiceImplementation : IAuthorService
    {
        private readonly List<Author> _authors = new();
        private int _authorId = 1;

        public AuthorServiceImplementation()
        {
            // Seed authors
            Add(new Author { FirstName = "George",LastName = "Orwell" });
            Add(new Author { FirstName = "J.K.", LastName = "Rowling"});
            Add(new Author { FirstName = "J.R.R." , LastName = "Tolkien" });
        }

        public List<Author> GetAll() => _authors;

        public Author? GetById(int id) => _authors.FirstOrDefault(a => a.Id == id);

        public Author Add(Author author)
        {
            author.Id = _authorId++;
            _authors.Add(author);
            return author;
        }

        public bool Update(int id, Author author)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.FirstName = author.FirstName;
            existing.LastName = author.LastName;
            existing.Books = author.Books;
            return true;
        }

        public bool Delete(int id)
        {
            var author = GetById(id);
            return author != null && _authors.Remove(author);
        }
    }
}
