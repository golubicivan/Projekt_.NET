using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixVenueCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.779400000000003, 15.9217 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.799599999999998, 15.9718 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.808500000000002, 15.992100000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.783000000000001, 15.9885 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.792200000000001, 15.961399999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.803899999999999, 15.9998 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.791800000000002, 15.961499999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.805700000000002, 15.9521 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.792999999999999, 15.98 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.8078, 15.9518 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.771500000000003, 15.9445 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.800400000000003, 15.974500000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.817700000000002, 15.9838 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.805999999999997, 15.9648 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.802199999999999, 15.960100000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.801600000000001, 15.960800000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.786999999999999, 15.951700000000001 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.783299999999997, 15.918100000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.801299999999998, 15.9869 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.814599999999999, 16.000800000000002 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.795000000000002, 15.976000000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.789999999999999, 15.962999999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.802999999999997, 16.015000000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.787999999999997, 15.965 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.798999999999999, 15.970000000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.789999999999999, 15.99 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.805999999999997, 15.954000000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.770000000000003, 15.943 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.802, 15.967000000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.823, 15.984999999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.805199999999999, 15.966799999999999 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.809199999999997, 15.956200000000001 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.810000000000002, 15.9556 });

            migrationBuilder.UpdateData(
                table: "Venues",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Latitude", "Longitude" },
                values: new object[] { 45.795999999999999, 15.945 });
        }
    }
}
