using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models;
using Activity2BooksAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Activity2BooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetAll()
        {
            try
            {
                var books = _bookService.GetAll()
                    .Select(book => new BookDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description
                    });

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving books: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<BookDTO> Get(int id)
        {
            try
            {
                var book = _bookService.GetById(id);
                if (book == null) return NotFound("Book not found");

                var dto = new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving book: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create(BookDTO bookDTO)
        {
            try
            {
                var book = new Book
                {
                    Title = bookDTO.Title,
                    Description = bookDTO.Description
                };

                var created = _bookService.Add(book);

                var createdDTO = new BookDTO
                {
                    Id = created.Id,
                    Title = created.Title,
                    Description = created.Description
                };

                return CreatedAtAction(nameof(Get), new { id = created.Id }, createdDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating book: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDTO bookDTO)
        {
            try
            {
                var book = new Book
                {
                    Title = bookDTO.Title,
                    Description = bookDTO.Description
                };

                return _bookService.Update(id, book)
                    ? Ok("Book Updated!")
                    : NotFound("Book Not Found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating book: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return _bookService.Delete(id)
                    ? Ok("Book Deleted")
                    : NotFound("Book Not Found!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting book: {ex.Message}");
            }
        }
    }
}
