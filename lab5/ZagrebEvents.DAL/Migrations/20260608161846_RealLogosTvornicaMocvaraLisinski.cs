using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RealLogosTvornicaMocvaraLisinski : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: 19,
                column: "LogoUrl",
                value: "/img/logos/lisinski-logo.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6,
                column: "LogoUrl",
                value: "/img/logos/tvornica.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                column: "LogoUrl",
                value: "/img/logos/mocvara.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19,
                column: "LogoUrl",
                value: "/img/logos/lisinski.svg");
        }
    }
}
