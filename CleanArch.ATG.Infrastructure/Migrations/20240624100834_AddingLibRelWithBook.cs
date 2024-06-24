using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingLibRelWithBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryId",
                table: "Books",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    LibraryId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    LibraryName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Location = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.LibraryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibraryId",
                table: "Books",
                column: "LibraryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books",
                column: "LibraryId",
                principalTable: "Libraries",
                principalColumn: "LibraryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Libraries_LibraryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibraryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibraryId",
                table: "Books");
        }
    }
}
