using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZagrebEvents.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityDocumentBackPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityDocumentBackPath",
                table: "AspNetUsers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityDocumentBackPath",
                table: "AspNetUsers");
        }
    }
}
