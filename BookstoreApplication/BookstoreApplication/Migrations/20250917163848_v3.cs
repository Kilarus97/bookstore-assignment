using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Biography = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    YearEstablished = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorAwardBridge",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    AwardId = table.Column<int>(type: "integer", nullable: false),
                    YearReceived = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorAwardBridge", x => new { x.AuthorId, x.AwardId });
                    table.ForeignKey(
                        name: "FK_AuthorAwardBridge_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorAwardBridge_Awards_AwardId",
                        column: x => x.AwardId,
                        principalTable: "Awards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    PageCount = table.Column<int>(type: "integer", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false),
                    PublisherId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Birthday", "FullName" },
                values: new object[,]
                {
                    { 1, "Nobelovac", new DateTime(1892, 10, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Ivo Andrić" },
                    { 2, "Enciklopedista", new DateTime(1935, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Danilo Kiš" },
                    { 3, "Prva akademkinja", new DateTime(1877, 2, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Isidora Sekulić" },
                    { 4, "Modernista", new DateTime(1893, 10, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Miloš Crnjanski" },
                    { 5, "Satiričar", new DateTime(1864, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Branislav Nušić" }
                });

            migrationBuilder.InsertData(
                table: "Awards",
                columns: new[] { "Id", "Description", "Name", "YearEstablished" },
                values: new object[,]
                {
                    { 1, "Najbolji roman godine", "NIN-ova nagrada", 1954 },
                    { 2, "Najbolja drama", "Sterijino pozorje", 1956 },
                    { 3, "Za književnost", "Nobelova nagrada", 1901 },
                    { 4, "Za pripovetku", "Andrićeva nagrada", 1975 }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "Name", "Website" },
                values: new object[,]
                {
                    { 1, "Beograd", "Laguna", "https://laguna.rs" },
                    { 2, "Novi Sad", "Zavod za udžbenike", "https://zavod.co.rs" },
                    { 3, "Beograd", "Geopoetika", "https://geopoetika.com" }
                });

            migrationBuilder.InsertData(
                table: "AuthorAwardBridge",
                columns: new[] { "AuthorId", "AwardId", "YearReceived" },
                values: new object[,]
                {
                    { 1, 1, 1955 },
                    { 1, 3, 1961 },
                    { 1, 4, 1976 },
                    { 2, 1, 1983 },
                    { 2, 3, 1986 },
                    { 2, 4, 1985 },
                    { 3, 1, 1936 },
                    { 3, 2, 1935 },
                    { 3, 4, 1930 },
                    { 4, 1, 1929 },
                    { 4, 2, 1931 },
                    { 4, 3, 1933 },
                    { 5, 1, 1930 },
                    { 5, 2, 1929 },
                    { 5, 4, 1932 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "9780001", 320, new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Na Drini ćuprija" },
                    { 2, 1, "9780002", 210, new DateTime(1954, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Prokleta avlija" },
                    { 3, 2, "9780003", 180, new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Enciklopedija mrtvih" },
                    { 4, 2, "9780004", 200, new DateTime(1968, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Bašta, pepeo" },
                    { 5, 3, "9780005", 150, new DateTime(1930, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Analitički trenuci" },
                    { 6, 3, "9780006", 120, new DateTime(1914, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Pisma iz Norveške" },
                    { 7, 4, "9780007", 400, new DateTime(1929, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Seobe" },
                    { 8, 4, "9780008", 100, new DateTime(1919, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Lirika Itake" },
                    { 9, 5, "9780009", 130, new DateTime(1929, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Gospođa ministarka" },
                    { 10, 5, "9780010", 140, new DateTime(1906, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Narodni poslanik" },
                    { 11, 5, "9780011", 160, new DateTime(1924, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Autobiografija" },
                    { 12, 5, "9780012", 110, new DateTime(1936, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Dr" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorAwardBridge_AwardId",
                table: "AuthorAwardBridge",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorAwardBridge");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
