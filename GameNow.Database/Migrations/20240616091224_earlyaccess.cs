using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNow.Database.Migrations
{
    /// <inheritdoc />
    public partial class earlyaccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EarlyAcess",
                table: "Game",
                newName: "EarlyAccess");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EarlyAccess",
                table: "Game",
                newName: "EarlyAcess");
        }
    }
}
