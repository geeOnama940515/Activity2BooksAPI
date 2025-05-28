using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;

namespace Activity2BooksAPI.Services
{
    public class BookServiceImplementation : IBookService
    {
        private readonly List<Book> _books = new();
        private readonly IAuthorService _authorService;
        private int _bookId = 1;

        public BookServiceImplementation(IAuthorService authorService)
        {
            _authorService = authorService;

            // Get some authors from the AuthorService
            var orwell = _authorService.GetAll().FirstOrDefault(a => a.FirstName.Contains("George"));
            var rowling = _authorService.GetAll().FirstOrDefault(a => a.FirstName.Contains("J.K."));

            // Seed books
            if (orwell != null)
            {
                Add(new Book { Title = "1984",Description = "This a book from 1984", Authors = new List<Author> { orwell } });
            }

            if (rowling != null)
            {
                Add(new Book { Title = "Harry Potter",Description = "Harry Potter : Chamber of Secrets", Authors = new List<Author> { rowling } });
            }
        }

        public List<Book> GetAll() => _books;

        public Book? GetById(int id) => _books.FirstOrDefault(b => b.Id == id);

        public Book Add(Book book)
        {
            book.Id = _bookId++;
            _books.Add(book);
            return book;
        }

        public bool Update(int id, Book book)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Title = book.Title;
            existing.Authors = book.Authors;
            return true;
        }

        public bool Delete(int id)
        {
            var book = GetById(id);
            return book != null && _books.Remove(book);
        }
    }
}
