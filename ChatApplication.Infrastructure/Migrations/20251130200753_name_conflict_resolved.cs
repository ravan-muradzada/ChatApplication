using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class name_conflict_resolved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isGroup",
                table: "Chats",
                newName: "IsGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsGroup",
                table: "Chats",
                newName: "isGroup");
        }
    }
}
