using Activity2BooksAPI.Models.Identity;
using Activity2BooksAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class AppDBContext : IdentityDbContext<ApplicationUser>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>().ToTable("UserAccounts");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");


        // Configure many-to-many relationship
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors)
            .UsingEntity(j => j.ToTable("AuthorBooks"));

        // Seed data
        var authors = new List<Author>
        {
            new() { Id = 1, FirstName = "George", LastName = "Orwell" },
            new() { Id = 2, FirstName = "Jane", LastName = "Austen" },
            new() { Id = 3, FirstName = "Mark", LastName = "Twain" },
            new() { Id = 4, FirstName = "J.K.", LastName = "Rowling" },
            new() { Id = 5, FirstName = "F. Scott", LastName = "Fitzgerald" }
        };

        var books = new List<Book>
        {
            new() { Id = 1, Title = "1984", Description = "Dystopian novel" },
            new() { Id = 2, Title = "Pride and Prejudice", Description = "Romantic fiction" },
            new() { Id = 3, Title = "Adventures of Huckleberry Finn", Description = "Classic adventure" },
            new() { Id = 4, Title = "Harry Potter and the Philosopher's Stone", Description = "Fantasy novel" },
            new() { Id = 5, Title = "The Great Gatsby", Description = "American classic" }
        };

        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Book>().HasData(books);

        // Seeding many-to-many relationships manually
        modelBuilder.Entity("AuthorBook").HasData(
            new { AuthorsId = 1, BooksId = 1 }, // Orwell -> 1984
            new { AuthorsId = 2, BooksId = 2 }, // Austen -> Pride
            new { AuthorsId = 3, BooksId = 3 }, // Twain -> Huck Finn
            new { AuthorsId = 4, BooksId = 4 }, // Rowling -> HP
            new { AuthorsId = 5, BooksId = 5 }  // Fitzgerald -> Gatsby
        );

        // Seed ApplicationUser
        // Seed ApplicationUser with static password hashes
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "25ab6d7e-585f-469e-902b-f60008bdfb03",
                Name = "Developer",
                NormalizedName = "DEVELOPER"
            }
        );

        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "25ab6d7e-e25d-446f-86af-df82d884e7b7",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com", // Add email if required
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                // Pre-computed hash for "p@$$W0rd@123"
                PasswordHash = "AQAAAAIAAYagAAAAELJ7ZPea3kZcbr8uKykT7uqICP3KVwUsn8AErvMGc5nr75Dw7p2IhPLXMl6sjgslOQ==",
                SecurityStamp = "372f5d7e-8eec-46ec-9670-32bc12f61de6",
                ConcurrencyStamp = "8d8808e-51be-4060-a50a-7c79c4e02586"
            },
            new ApplicationUser
            {
                Id = "9911b550-3b0e-4889-902b-483d56fd7210",
                UserName = "developer",
                NormalizedUserName = "DEVELOPER",
                Email = "developer@example.com", // Add email if required
                NormalizedEmail = "DEVELOPER@EXAMPLE.COM",
                EmailConfirmed = true,
                // Pre-computed hash for "devp@$$W0rd@123"
                PasswordHash = "AQAAAAIAAYagAAAAECRSL36zEJUqgeYOMa2wiN1NIePclV18upLXF4sbSo+lqFFKhd+Wfb/bnwJLqbSUZg==",
                SecurityStamp = "372f5d7e-8eec-46ec-9670-32bc12f61de6",
                ConcurrencyStamp = "8d8808e-51be-4060-a50a-7c79c4e02586"
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "25ab6d7e-e25d-446f-86af-df82d884e7b7"
            },
            new IdentityUserRole<string>()
            {
                RoleId = "25ab6d7e-585f-469e-902b-f60008bdfb03", // Fixed: Developer role ID
                UserId = "9911b550-3b0e-4889-902b-483d56fd7210"
            }
        );
    }
}
