using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AlkatrazNameAndLogo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 114,
                column: "Name",
                value: "Alkatraz Rock Night");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "LogoUrl", "Name" },
                values: new object[] { "/img/logos/alkatraz-emblem.jpg", "Alkatraz Rock Bar" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 114,
                column: "Name",
                value: "Alcatraz Rock Night");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "LogoUrl", "Name" },
                values: new object[] { "/img/logos/alcatraz.svg", "Alcatraz Rock Bar" });
        }
    }
}
