using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oblig2.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Discussion",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Discussion",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Discussion",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Discussion",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Discussion");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Discussion",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Discussion",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Discussion",
                newName: "id");
        }
    }
}
