# Activity 2: API Development in .NET Core and Framework

## How I Developed the API for a Book Management System

### 1. Created Models

#### Core Models

**Book Model**
| Property | Type | Description |
|----------|------|-------------|
| Id | int | Book identifier |
| Title | string | Book title |
| Description | string | Book description |
| Authors | List | Many-to-many relationship: multiple authors per book |

**Author Model**
| Property | Type | Description |
|----------|------|-------------|
| Id | int | Author identifier |
| FirstName | string | Author's first name |
| LastName | string | Author's last name |
| Books | List | Many-to-many relationship: authors publish multiple books |

#### DTO (Data Transfer Objects)

**AuthorDTO**
| Property | Type | Description |
|----------|------|-------------|
| Id | int? | Nullable author identifier |
| FirstName | string | Author's first name |
| LastName | string | Author's last name |

**BookDTO**
| Property | Type | Description |
|----------|------|-------------|
| Id | int? | Nullable book identifier |
| Title | string | Book title |
| Description | string | Book description |
| AuthorIds | List<int> | List of author IDs linked to this book (many-to-many relation) |

**BookDetailsDTO**
| Property | Type | Description |
|----------|------|-------------|
| Id | int | Book identifier |
| Title | string | Book title |
| Description | string | Book description |
| AuthorNames | List<string> | List of author full names instead of IDs (for display purposes) |

### 2. Created Interface (Blueprint for the methods)

#### IBookService Interface

| Method | Description |
|--------|-------------|
| `List<Book> GetAll()` | Lists all books in the collection |
| `Book? GetById(int id)` | Returns the book with the given ID, or null if not found |
| `Book Add(Book book)` | Adds a new book; returns the created book |
| `bool Update(int id, Book book)` | Updates the book with given ID; returns true if successful |
| `bool Delete(int id)` | Deletes the book with given ID; returns true if successful |
| `List<Book> SearchByTitle(string title)` | Searches and returns books matching the given title |

#### IAuthorService Interface

| Method | Description |
|--------|-------------|
| `List<Author> GetAll()` | Lists all authors in the collection |
| `Author? GetById(int id)` | Returns the author with the given ID, or null if not found |
| `Author Add(Author author)` | Adds a new author; returns the created author |
| `bool Update(int id, Author author)` | Updates the author with given ID; returns true if successful |
| `bool Delete(int id)` | Deletes the author with given ID; returns true if successful |
| `List<Author> SearchByName(string name)` | Searches and returns authors matching the given name |
| `List<Book> GetBooksByAuthorId(int authorId)` | Returns all books written by the author with given ID |

### 3. Implementation

Implemented the interfaces in:
- `AuthorServiceImplementation` class
- `BookServiceImplementation` class

### 4. Added Dependency Injection in Program.cs

```csharp
builder.Services.AddSingleton<IAuthorService, AuthorServiceImplementation>();
builder.Services.AddSingleton<IBookService, BookServiceImplementation>();
```

### 5. Created Controllers with RESTful Endpoints

#### Author Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/authors` | Get all authors |
| GET | `/api/authors/{id}` | Get author by ID |
| GET | `/api/authors/search?name=` | Search authors by name |
| GET | `/api/authors/{id}/books` | Get all books written by an author |
| POST | `/api/authors` | Create a new author |
| PUT | `/api/authors/{id}` | Update an author |
| DELETE | `/api/authors/{id}` | Delete an author |

#### Book Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/books` | Get all books (with author names) |
| GET | `/api/books/{id}` | Get a book by ID |
| GET | `/api/books/search?title=` | Search books by title (optional feature) |
| POST | `/api/books` | Create a new book |
| PUT | `/api/books/{id}` | Update a book |
| DELETE | `/api/books/{id}` | Delete a book |

---

*This documentation outlines the complete API development process for a Book Management System using .NET Core, including model creation, service interfaces, dependency injection, and RESTful endpoint implementation.*
