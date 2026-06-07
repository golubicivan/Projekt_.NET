using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AllVenueLogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2,
                column: "LogoUrl",
                value: "/img/logos/kavanalav.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 3,
                column: "LogoUrl",
                value: "/img/logos/bundek.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                column: "LogoUrl",
                value: "/img/logos/masters.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                column: "LogoUrl",
                value: "/img/logos/hangar.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 15,
                column: "LogoUrl",
                value: "/img/logos/pogon.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 16,
                column: "LogoUrl",
                value: "/img/logos/lauba.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 20,
                column: "LogoUrl",
                value: "/img/logos/jarun.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 21,
                column: "LogoUrl",
                value: "/img/logos/maksimir.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 22,
                column: "LogoUrl",
                value: "/img/logos/salata.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 23,
                column: "LogoUrl",
                value: "/img/logos/ribnjak.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 24,
                column: "LogoUrl",
                value: "/img/logos/eliscaffe.svg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 2,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 3,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 15,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 16,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 20,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 21,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 22,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 23,
                column: "LogoUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 24,
                column: "LogoUrl",
                value: "");
        }
    }
}
