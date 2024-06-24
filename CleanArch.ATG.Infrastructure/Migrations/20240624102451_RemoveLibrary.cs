using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libraries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
