using System.Security.Claims;
using System.Text;
using BookstoreApplication.Data;
using BookstoreApplication.Interfaces;
using BookstoreApplication.Mapping;
using BookstoreApplication.Middleware;
using BookstoreApplication.Models;
using BookstoreApplication.Repository;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

builder.Services.AddAuthentication(options =>
{ // Naglašavamo da koristimo JWT
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true, // Validacija da li je token istekao

            ValidateIssuer = true,   // Validacija URL-a aplikacije koja izdaje token
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // URL aplikacije koja izdaje token (čita se iz appsettings.json)

            ValidateAudience = true, // Validacija URL-a aplikacije koja koristi token
            ValidAudience = builder.Configuration["Jwt:Audience"], // URL aplikacije koja koristi token (čita se iz appsettings.json)

            ValidateIssuerSigningKey = true, // Validacija ključa za potpisivanje tokena (koji se koristi i pri proveri potpisa)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])), //Ključ za proveru tokena (čita se iz appsettings.json)

            RoleClaimType = ClaimTypes.Role // Potrebno za kontrolu pristupa, što ćemo videti kasnije
        };
    });

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Building Example API", Version = "v1" });

    // Definisanje JWT Bearer autentifikacije
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });

    // Primena Bearer autentifikacije
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      Array.Empty<string>()
    }
  });
});


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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

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
