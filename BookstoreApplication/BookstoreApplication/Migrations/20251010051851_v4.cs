using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookstoreApplication.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "ISBN", "PageCount", "PublishedDate", "PublisherId", "Title" },
                values: new object[,]
                {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 40);
        }
    }
}
