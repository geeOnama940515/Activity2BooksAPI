using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;
using Activity2BooksAPI.Models.DTO;
using Activity2BooksAPI.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace Activity2BooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Developer")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var books = _bookService.GetAll()
                    .Select(book => new BookDetailsDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        AuthorNames = book.Authors?
                            .Select(a => $"{a.FirstName} {a.LastName}")
                            .ToList() ?? new List<string>()
                    });

                return this.CreateResponse(200, "Fetched Books", books);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error retrieving books: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var book = _bookService.GetById(id);
                if (book == null) return NotFound("Book not found");

                var dto = new BookDetailsDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    AuthorNames = book.Authors?
                            .Select(a => $"{a.FirstName} {a.LastName}") //Projection
                            .ToList() ?? new List<string>()
                };

                return this.CreateResponse(200, "Fetched book", dto);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error retrieving book: {ex.Message}");
            }
        }
        [HttpGet("search")]
        public IActionResult SearchByTitle([FromQuery] string title)
        {
            try
            {
                var books = _bookService.SearchByTitle(title)
                    .Select(book => new BookDetailsDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        AuthorNames = book.Authors?
                                    .Select(a => $"{a.FirstName} {a.LastName}")
                                    .ToList() ?? new List<string>()
                    });

                return this.CreateResponse(200, "Fetched Books", books);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error searching books: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult Create(BookDTO bookDTO)
        {
            try
            {
                var authors = _authorService.GetAll()
                    .Where(a => bookDTO.AuthorIds.Contains(a.Id))
                    .ToList();

                var book = new Book
                {
                    Title = bookDTO.Title,
                    Description = bookDTO.Description,
                    Authors = authors
                };

                var created = _bookService.Add(book);

                var createdDTO = new BookDTO
                {
                    Id = created.Id,
                    Title = created.Title,
                    Description = created.Description,
                    AuthorIds = created.Authors?.Select(a => a.Id).ToList() ?? new List<int>()
                };

                return this.CreateResponse(201, "Book Created.", createdDTO);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error creating book: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDTO bookDTO)
        {
            try
            {
                var authors = _authorService.GetAll()
                    .Where(a => bookDTO.AuthorIds.Contains(a.Id))
                    .ToList();

                var book = new Book
                {
                    Id = id,
                    Title = bookDTO.Title,
                    Description = bookDTO.Description,
                    Authors = authors
                };

                return _bookService.Update(id, book)
                    ? this.CreateResponse(200, "Book Updated!")
                    : this.CreateResponse(404, "Book Not Found!");
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error updating book: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return _bookService.Delete(id)
                    ? this.CreateResponse(200, "Book Deleted")
                    : this.CreateResponse(404, "Book Not Found!");
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error deleting book: {ex.Message}");
            }
        }

        [HttpGet("with-pagination")]
        public async Task<IActionResult> GetPaginated([FromQuery] string? query, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var books = _bookService.GetAll();

            var bookQueryable = books.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                bookQueryable = bookQueryable.Where(p =>
                    p.Title.ToLower().Contains(query.ToLower())
                );
            }

            // Project to DTO before pagination (better performance and avoid cycles)
            var bookDtoQueryable = bookQueryable.Select(book => new BookDetailsDTO
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorNames = (book.Authors ?? Enumerable.Empty<Author>())
                        .Select(a => a.FirstName + " " + a.LastName)
                        .ToList()
            });

            var result = await bookDtoQueryable.GetPaginatedResults(page: page, pageSize: pageSize);

            return this.CreateResponse(200, "Retrieved Books", result);
        }
    }
}
