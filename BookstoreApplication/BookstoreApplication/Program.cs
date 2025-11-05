using BookstoreApplication.Data;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Mapping;
using BookstoreApplication.Middleware;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ➡️ DbContext mora ići pre Identity
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ➡️ Identity sa string ključem
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BookstoreDbContext>()
    .AddDefaultTokenProviders();

// ➡️ Identity opcije
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
});

builder.Services.AddAuthentication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IBooksRepo, BooksRepo>();
builder.Services.AddScoped<IAuthorsRepo, AuthorsRepo>();
builder.Services.AddScoped<IPublishersRepo, PublishersRepo>();

// ➡️ Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// ➡️ Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ⚠️ Redosled je bitan: prvo Authentication, pa Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
