using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Activity2BooksAPI.Services
{
    public class BookServiceImplementation : IBookService
    {
        private readonly AppDBContext _context;

        public BookServiceImplementation(AppDBContext context)
        {
            _context = context;

        }

        public List<Book> GetAll() => _context.Books.Include(a => a.Authors).ToList();

        public Book? GetById(int id) => _context.Books.AsEnumerable().FirstOrDefault(b => b.Id == id);

        public Book Add(Book book)
        {
            // Validate authors
            if (book.Authors == null || !book.Authors.Any())
            {
                throw new ArgumentException("Book must have at least one author.");
            }
            if(book.Title == null || book.Title.Trim().Length == 0)
            {
                throw new ArgumentException("Book title cannot be empty.");
            }
            //check if book is existing
            var existingBook = _context.Books.FirstOrDefault(b => b.Title.ToLower().Equals(book.Title.ToLower()));
            if (existingBook != null)
            {
                throw new InvalidOperationException("A book with the same title already exists.");
            }

            var addBook = _context.Books.Add(book);
            _context.SaveChanges();
            return addBook.Entity;
        }

        public bool Update(int id, Book book)
        {
            var existing = _context.Books
                .Include(b => b.Authors) // Include navigation property
                .FirstOrDefault(b => b.Id == id);

            if (existing == null) return false;

            existing.Title = book.Title;

            existing.Authors.Clear();

            foreach (var author in book.Authors)
            {
                var existingAuthor = _context.Authors.Find(author.Id);
                if (existingAuthor != null)
                {
                    existing.Authors.Add(existingAuthor);
                }
            }

            _context.Books.Update(existing);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);
            if (book == null) return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public List<Book> SearchByTitle(string title)
        {
            return _context.Books
            .Where(b => b.Title.ToLower().Contains(title.ToLower()))
            .ToList();
        }
    }
}
