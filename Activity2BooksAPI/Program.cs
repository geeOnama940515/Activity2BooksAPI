using Activity2BooksAPI;
using Activity2BooksAPI.Interfaces;
using Activity2BooksAPI.Models.Identity;
using Activity2BooksAPI.Services;
using Microsoft.Extensions.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDependencyInjection(configuration);


//lower-case endpoints

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("/api/accounts").MapIdentityApi<ApplicationUser>().WithTags("Accounts");
app.Run();
