using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;

namespace Activity2BooksAPI.Services
{
    public class BookServiceImplementation : IBookService
    {
        private readonly AppDBContext _context;
        private readonly IAuthorService _authorService;

        public BookServiceImplementation(AppDBContext context,IAuthorService authorService)
        {
            _context = context;
            _authorService = authorService;

        }

        public List<Book> GetAll() => _context.Books.ToList();

        public Book? GetById(int id) => _context.Books.FirstOrDefault(b => b.Id == id);

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
            var existingBook = _context.Books.FirstOrDefault(b => b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase));
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
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Title = book.Title;
            existing.Authors = book.Authors;

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
            .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
    }
}
