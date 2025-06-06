﻿using Activity2BooksAPI.Models;

namespace Activity2BooksAPI.Interfaces
{
    /// <summary>
    /// Interface for book service operations.
    /// Blueprint for managing books in the book API.
    /// must be implemented by any class that provides book management functionality.
    /// </summary>
    public interface IBookService
    {
        List<Book> GetAll();
        Book? GetById(int id);
        Book Add(Book book);
        bool Update(int id, Book book);
        bool Delete(int id);
        List<Book> SearchByTitle(string title);
    }
}

// This interface defines the contract for book-related operations, including methods to retrieve, add, update, and delete books.
// Could've made it Generic, but for demo purposes I made it this way.