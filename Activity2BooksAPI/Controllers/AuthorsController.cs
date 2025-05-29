using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models.DTO;
using Activity2BooksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Activity2BooksAPI.Services.Helpers;

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
        public  IActionResult GetAll()
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

                return this.CreateResponse(200,"Fetched Authors",authors);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error retrieving authors: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
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

                return this.CreateResponse(200, "Fetched Author", dto);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error retrieving author: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AuthorDTO authorDto)
        {
            try
            {
                var isExisting = _authorService.SearchByName($"{authorDto.FirstName} {authorDto.LastName}");
                if (isExisting.Any())
                {
                    return this.CreateResponse(409,"Author with the same name already exists.");
                }

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                var added = _authorService.Add(author);
                return this.CreateResponse(201,"Author Created.", authorDto);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error creating author: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AuthorDTO authorDto)
        {
            try
            {
                var isExisting = _authorService.GetById(id);
                if (isExisting == null) return this.CreateResponse(404,"Author does not exist!");

                var author = new Author
                {
                    FirstName = authorDto.FirstName,
                    LastName = authorDto.LastName
                };

                return _authorService.Update(id, author)
                    ? this.CreateResponse(200,"Author Updated")
                    : this.CreateResponse(404,"Author does not exist!");
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error updating author: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return _authorService.Delete(id)
                    ? this.CreateResponse(200,"Author Deleted.")
                    : this.CreateResponse(404, "Author does not exist!");
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error deleting author: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public IActionResult SearchByName([FromQuery] string name)
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

                return this.CreateResponse(200,"Author fethced",authors);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error searching authors: {ex.Message}");
            }
        }

        [HttpGet("{id}/books")]
        public IActionResult GetBooksByAuthorId(int id)
        {
            try
            {
                var books = _authorService.GetBooksByAuthorId(id);

                if (books == null || books.Count == 0)
                    return this.CreateResponse(404,"No books found for this author.");

                var bookDtos = books.Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description
                });

                return this.CreateResponse(200,"Fetched books by author",bookDtos);
            }
            catch (Exception ex)
            {
                return this.CreateResponse(500, $"Error retrieving books: {ex.Message}");
            }
        }
    }
}
