using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RealLogosSovaOutRooftopAlkatraz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "https://www.instagram.com/sova.night.club/", "/img/logos/sova-emblem.jpg" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 104,
                column: "LogoUrl",
                value: "/img/logos/alkatraz.svg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "https://www.instagram.com/outrooftop/", "/img/logos/outrooftop-emblem.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "", "/img/logos/sova.svg" });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 104,
                column: "LogoUrl",
                value: "/img/logos/alkatraz-emblem.jpg");

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "InstagramUrl", "LogoUrl" },
                values: new object[] { "", "/img/logos/outrooftop.svg" });
        }
    }
}
