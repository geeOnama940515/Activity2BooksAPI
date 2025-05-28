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
            var orwell = Add(new Author { FirstName = "George", LastName = "Orwell" });
            orwell.Books = new List<Book>
            {
                new Book { Id = 1, Title = "1984", Description = "Dystopian novel" },
                new Book { Id = 2, Title = "Animal Farm", Description = "Political allegory" }
            };

            var rowling = Add(new Author { FirstName = "J.K.", LastName = "Rowling" });
            rowling.Books = new List<Book>
            {
                new Book { Id = 3, Title = "Harry Potter and the Philosopher's Stone", Description = "First Harry Potter book" }
            };

            var tolkien = Add(new Author { FirstName = "J.R.R.", LastName = "Tolkien" });
            tolkien.Books = new List<Book>
            {
                new Book { Id = 4, Title = "The Hobbit", Description = "Adventure in Middle-earth" },
                new Book { Id = 5, Title = "The Lord of the Rings", Description = "Epic fantasy" }
            };
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

        public List<Author> SearchByName(string name)
        {
            return _authors
              .Where(a => $"{a.FirstName} {a.LastName}".Contains(name, StringComparison.OrdinalIgnoreCase))
              .ToList();
        }

        public List<Book> GetBooksByAuthorId(int authorId)
        {
            var author = GetById(authorId);

            return author?.Books ?? new List<Book>();
        }
    }
}
