using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;
using System.Xml.Linq;

namespace Activity2BooksAPI.Services
{
    public class AuthorServiceImplementation : IAuthorService
    {
        private readonly AppDBContext _context;

        public AuthorServiceImplementation(AppDBContext context)
        {
            _context = context;
        }

        public List<Author> GetAll() => _context.Authors.ToList();

        public Author? GetById(int id) => _context.Authors.FirstOrDefault(a => a.Id == id);

        public Author Add(Author author)
        {
            var add = _context.Authors.Add(author);
            _context.SaveChanges();
            return add.Entity;
        }

        public bool Update(int id, Author author)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.FirstName = author.FirstName;
            existing.LastName = author.LastName;
            existing.Books = author.Books;

            _context.Authors.Update(existing);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var author = GetById(id);

            if(author == null) return false;

            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }

        public List<Author> SearchByName(string name)
        {
            return _context.Authors
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
