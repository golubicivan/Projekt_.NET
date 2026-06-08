using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MoreRealLogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                column: "LogoUrl",
                value: "/img/logos/aquarius-emblem.jpg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                column: "LogoUrl",
                value: "/img/logos/katran-emblem.jpg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 17,
                column: "LogoUrl",
                value: "/img/logos/domsportova-emblem.jpg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 18,
                column: "LogoUrl",
                value: "/img/logos/arena-emblem.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                column: "LogoUrl",
                value: "/img/logos/aquarius.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                column: "LogoUrl",
                value: "/img/logos/katran.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 17,
                column: "LogoUrl",
                value: "/img/logos/domsportova.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 18,
                column: "LogoUrl",
                value: "/img/logos/arena.svg");
        }
    }
}
