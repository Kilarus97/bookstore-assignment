using System;
using BookstoreApplication.Data;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Dodaj CORS servis pre nego što pozoveš builder.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Ostale servise
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddScoped<BookServices>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<PublisherService>();

builder.Services.AddScoped<BooksRepo>();
builder.Services.AddScoped<AuthorsRepo>();
builder.Services.AddScoped<PublishersRepo>();


var app = builder.Build();

// 2. Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // obavezno pre CORS-a
app.UseCors("AllowFrontend"); // sad je validan jer je servis već registrovan
app.UseAuthorization();

app.MapControllers();

app.Run();



