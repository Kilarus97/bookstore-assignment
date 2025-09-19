using Microsoft.EntityFrameworkCore;
using BookstoreApplication.Models;

namespace BookstoreApplication.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AuthorAward> AuthorAwards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔗 Relacije
            modelBuilder.Entity<AuthorAward>()
                .HasKey(aa => new { aa.AuthorId, aa.AwardId });

            modelBuilder.Entity<AuthorAward>()
                .ToTable("AuthorAwardBridge");

            modelBuilder.Entity<AuthorAward>()
                .HasOne(aa => aa.Author)
                .WithMany(a => a.AuthorAwards)
                .HasForeignKey(aa => aa.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorAward>()
                .HasOne(aa => aa.Award)
                .WithMany(a => a.AuthorAwards)
                .HasForeignKey(aa => aa.AwardId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany()
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Author>()
                .Property(a => a.DateOfBirth)
                .HasColumnName("Birthday");


            // 📚 Autori
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Ivo Andrić", Biography = "Nobelovac", DateOfBirth = DateTime.SpecifyKind(new DateTime(1892, 10, 9), DateTimeKind.Utc) },
                new Author { Id = 2, FullName = "Danilo Kiš", Biography = "Enciklopedista", DateOfBirth = DateTime.SpecifyKind(new DateTime(1935, 2, 22), DateTimeKind.Utc) },
                new Author { Id = 3, FullName = "Isidora Sekulić", Biography = "Prva akademkinja", DateOfBirth = DateTime.SpecifyKind(new DateTime(1877, 2, 16), DateTimeKind.Utc) },
                new Author { Id = 4, FullName = "Miloš Crnjanski", Biography = "Modernista", DateOfBirth = DateTime.SpecifyKind(new DateTime(1893, 10, 26), DateTimeKind.Utc) },
                new Author { Id = 5, FullName = "Branislav Nušić", Biography = "Satiričar", DateOfBirth = DateTime.SpecifyKind(new DateTime(1864, 10, 20), DateTimeKind.Utc) }
            );

            // 🏢 Izdavači
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Laguna", Address = "Beograd", Website = "https://laguna.rs" },
                new Publisher { Id = 2, Name = "Zavod za udžbenike", Address = "Novi Sad", Website = "https://zavod.co.rs" },
                new Publisher { Id = 3, Name = "Geopoetika", Address = "Beograd", Website = "https://geopoetika.com" }
            );

            // 📖 Knjige
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Na Drini ćuprija", PageCount = 320, PublishedDate = DateTime.SpecifyKind(new DateTime(1945, 1, 1), DateTimeKind.Utc), ISBN = "9780001", AuthorId = 1, PublisherId = 1 },
                new Book { Id = 2, Title = "Prokleta avlija", PageCount = 210, PublishedDate = DateTime.SpecifyKind(new DateTime(1954, 1, 1), DateTimeKind.Utc), ISBN = "9780002", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 3, Title = "Enciklopedija mrtvih", PageCount = 180, PublishedDate = DateTime.SpecifyKind(new DateTime(1983, 1, 1), DateTimeKind.Utc), ISBN = "9780003", AuthorId = 2, PublisherId = 1 },
                new Book { Id = 4, Title = "Bašta, pepeo", PageCount = 200, PublishedDate = DateTime.SpecifyKind(new DateTime(1968, 1, 1), DateTimeKind.Utc), ISBN = "9780004", AuthorId = 2, PublisherId = 3 },
                new Book { Id = 5, Title = "Analitički trenuci", PageCount = 150, PublishedDate = DateTime.SpecifyKind(new DateTime(1930, 1, 1), DateTimeKind.Utc), ISBN = "9780005", AuthorId = 3, PublisherId = 2 },
                new Book { Id = 6, Title = "Pisma iz Norveške", PageCount = 120, PublishedDate = DateTime.SpecifyKind(new DateTime(1914, 1, 1), DateTimeKind.Utc), ISBN = "9780006", AuthorId = 3, PublisherId = 1 },
                new Book { Id = 7, Title = "Seobe", PageCount = 400, PublishedDate = DateTime.SpecifyKind(new DateTime(1929, 1, 1), DateTimeKind.Utc), ISBN = "9780007", AuthorId = 4, PublisherId = 3 },
                new Book { Id = 8, Title = "Lirika Itake", PageCount = 100, PublishedDate = DateTime.SpecifyKind(new DateTime(1919, 1, 1), DateTimeKind.Utc), ISBN = "9780008", AuthorId = 4, PublisherId = 2 },
                new Book { Id = 9, Title = "Gospođa ministarka", PageCount = 130, PublishedDate = DateTime.SpecifyKind(new DateTime(1929, 1, 1), DateTimeKind.Utc), ISBN = "9780009", AuthorId = 5, PublisherId = 1 },
                new Book { Id = 10, Title = "Narodni poslanik", PageCount = 140, PublishedDate = DateTime.SpecifyKind(new DateTime(1906, 1, 1), DateTimeKind.Utc), ISBN = "9780010", AuthorId = 5, PublisherId = 3 },
                new Book { Id = 11, Title = "Autobiografija", PageCount = 160, PublishedDate = DateTime.SpecifyKind(new DateTime(1924, 1, 1), DateTimeKind.Utc), ISBN = "9780011", AuthorId = 5, PublisherId = 2 },
                new Book { Id = 12, Title = "Dr", PageCount = 110, PublishedDate = DateTime.SpecifyKind(new DateTime(1936, 1, 1), DateTimeKind.Utc), ISBN = "9780012", AuthorId = 5, PublisherId = 1 }
            );

            // 🏆 Nagrade
            modelBuilder.Entity<Award>().HasData(
                new Award { Id = 1, Name = "NIN-ova nagrada", Description = "Najbolji roman godine", YearEstablished = 1954 },
                new Award { Id = 2, Name = "Sterijino pozorje", Description = "Najbolja drama", YearEstablished = 1956 },
                new Award { Id = 3, Name = "Nobelova nagrada", Description = "Za književnost", YearEstablished = 1901 },
                new Award { Id = 4, Name = "Andrićeva nagrada", Description = "Za pripovetku", YearEstablished = 1975 }
            );

            // 🔗 Poveznica AuthorAwardBridge
                    modelBuilder.Entity<AuthorAward>().HasData(
                new AuthorAward { AuthorId = 1, AwardId = 3, YearReceived = 1961 },
                new AuthorAward { AuthorId = 1, AwardId = 4, YearReceived = 1976 },
                new AuthorAward { AuthorId = 2, AwardId = 1, YearReceived = 1983 },
                new AuthorAward { AuthorId = 2, AwardId = 4, YearReceived = 1985 },
                new AuthorAward { AuthorId = 3, AwardId = 4, YearReceived = 1930 },
                new AuthorAward { AuthorId = 3, AwardId = 2, YearReceived = 1935 },
                new AuthorAward { AuthorId = 4, AwardId = 1, YearReceived = 1929 },
                new AuthorAward { AuthorId = 4, AwardId = 2, YearReceived = 1931 },
                new AuthorAward { AuthorId = 5, AwardId = 2, YearReceived = 1929 },
                new AuthorAward { AuthorId = 5, AwardId = 1, YearReceived = 1930 },
                new AuthorAward { AuthorId = 5, AwardId = 4, YearReceived = 1932 },
                new AuthorAward { AuthorId = 2, AwardId = 3, YearReceived = 1986 },
                new AuthorAward { AuthorId = 4, AwardId = 3, YearReceived = 1933 },
                new AuthorAward { AuthorId = 3, AwardId = 1, YearReceived = 1936 },
                new AuthorAward { AuthorId = 1, AwardId = 1, YearReceived = 1955 }
            );

        }
    }
}
