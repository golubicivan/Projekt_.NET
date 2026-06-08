using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SquareEmblemLogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6,
                column: "LogoUrl",
                value: "/img/logos/tvornica-emblem.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                column: "LogoUrl",
                value: "/img/logos/mocvara-emblem.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8,
                column: "LogoUrl",
                value: "/img/logos/vintage-emblem.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 16,
                column: "LogoUrl",
                value: "/img/logos/lauba-emblem.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19,
                column: "LogoUrl",
                value: "/img/logos/lisinski.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 21,
                column: "LogoUrl",
                value: "/img/logos/maksimir-emblem.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6,
                column: "LogoUrl",
                value: "/img/logos/tvornica-logo.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                column: "LogoUrl",
                value: "/img/logos/mocvara-logo.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8,
                column: "LogoUrl",
                value: "/img/logos/vintage.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 16,
                column: "LogoUrl",
                value: "/img/logos/lauba.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19,
                column: "LogoUrl",
                value: "/img/logos/lisinski-logo.png");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 21,
                column: "LogoUrl",
                value: "/img/logos/maksimir.svg");
        }
    }
}
