using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models.DTO;
using Activity2BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Activity2BooksAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AuthorDTO>> GetAll()
        {
            try
            {
                var authors = _authorService.GetAll()
                    .Select(a => new AuthorDTO
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    });

                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving authors: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AuthorDTO> Get(int id)
        {
            try
            {
                var author = _authorService.GetById(id);
                if (author == null) return NotFound("Author not found");

                var dto = new AuthorDTO
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving author: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorDTO authorDto)
        {
            try
            {
                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                var added = _authorService.Add(author);
                return CreatedAtAction(nameof(Get), new { id = added.Id }, authorDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating author: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AuthorDTO authorDto)
        {
            try
            {
                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                return _authorService.Update(id, author)
                    ? Ok("Author Updated")
                    : NotFound("Author does not exist!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating author: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return _authorService.Delete(id)
                    ? Ok("Deleted")
                    : NotFound("Author does not exist!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting author: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<AuthorDTO>> SearchByName([FromQuery] string name)
        {
            try
            {
                var authors = _authorService.SearchByName(name)
                    .Select(a => new AuthorDTO
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    });

                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching authors: {ex.Message}");
            }
        }

        [HttpGet("{id}/books")]
        public ActionResult<IEnumerable<BookDTO>> GetBooksByAuthorId(int id)
        {
            try
            {
                var books = _authorService.GetBooksByAuthorId(id);

                if (books == null || books.Count == 0)
                    return NotFound("No books found for this author.");

                var bookDtos = books.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description
                });

                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving books: {ex.Message}");
            }
        }
    }
}
