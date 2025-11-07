using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "IdentityRole<Guid>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole<Guid>", x => x.Id);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                table: "IdentityRole<Guid>",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2301d884-221a-4e7d-b509-0113dcc043e1"), null, "Editor", "EDITOR" },
                    { new Guid("5b00155d-77a2-438c-b18f-dc1cc8af5a43"), null, "Librarian", "LIBRARIAN" }
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
                    { 12, 5, "9780012", 110, new DateTime(1936, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Dr" },
                    { 13, 1, "9780013", 280, new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Senke nad Balkanom" },
                    { 14, 2, "9780014", 450, new DateTime(1986, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Zlatno runo" },
                    { 15, 3, "9780015", 220, new DateTime(1962, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Noć u Lisabonu" },
                    { 16, 1, "9780016", 310, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Tvrđava" },
                    { 17, 2, "9780017", 260, new DateTime(1953, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Koreni" },
                    { 18, 3, "9780018", 500, new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Vreme smrti" },
                    { 19, 4, "9780019", 340, new DateTime(1966, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Derviš i smrt" },
                    { 20, 5, "9780020", 180, new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Čarobna frula" },
                    { 21, 1, "9780021", 96, new DateTime(1943, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Mali princ" },
                    { 22, 2, "9780022", 240, new DateTime(1976, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Znakovi pored puta" },
                    { 23, 3, "9780023", 360, new DateTime(1983, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Besnilo" },
                    { 24, 4, "9780024", 290, new DateTime(2009, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Konstantinovo raskršće" },
                    { 25, 5, "9780025", 270, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Knjiga o Milutinu" },
                    { 26, 1, "9780026", 230, new DateTime(1933, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Legenda o Ali Paši" },
                    { 27, 2, "9780027", 190, new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Velika tajna" },
                    { 28, 3, "9780028", 210, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Putnik" },
                    { 29, 4, "9780029", 170, new DateTime(1995, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Zemlja snova" },
                    { 30, 5, "9780030", 150, new DateTime(1988, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Tišina" },
                    { 31, 1, "9780031", 220, new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Oluja" },
                    { 32, 2, "9780032", 260, new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Zaboravljeni grad" },
                    { 33, 3, "9780033", 240, new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Dolina senki" },
                    { 34, 4, "9780034", 180, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Pogled u beskraj" },
                    { 35, 5, "9780035", 300, new DateTime(2003, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Kamen mudrosti" },
                    { 36, 1, "9780036", 210, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Plavi horizonti" },
                    { 37, 2, "9780037", 190, new DateTime(2007, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Tajna šifre" },
                    { 38, 3, "9780038", 160, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Zvezdana prašina" },
                    { 39, 4, "9780039", 280, new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Okean tišine" },
                    { 40, 5, "9780040", 250, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Grad bez imena" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuthorAwardBridge");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "IdentityRole<Guid>");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
