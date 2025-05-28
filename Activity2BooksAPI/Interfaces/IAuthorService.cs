using Activity2BooksAPI.Models;

namespace Activity2BooksAPI.Interfaces
{
    /// <summary>
    /// Interface for author service operations.
    /// This is a blueprint for managing authors in the book API.
    /// and provides methods to retrieve, add, update, and delete authors.
    /// This must be implemented by any class that provides author management functionality.
    /// </summary>
    public interface IAuthorService
    {
        List<Author> GetAll();
        Author? GetById(int id);
        Author Add(Author author);
        bool Update(int id, Author author);
        bool Delete(int id);
        List<Author> SearchByName(string name);  // For searching by name
        List<Book> GetBooksByAuthorId(int authorId); // For listing books written by an author
    }
}
