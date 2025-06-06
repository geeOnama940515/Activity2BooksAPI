﻿namespace Activity2BooksAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Author>? Authors { get; set; } = new();
    }
}
