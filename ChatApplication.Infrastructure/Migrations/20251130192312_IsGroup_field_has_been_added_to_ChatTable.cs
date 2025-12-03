using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IsGroup_field_has_been_added_to_ChatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isGroup",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isGroup",
                table: "Chats");
        }
    }
}
