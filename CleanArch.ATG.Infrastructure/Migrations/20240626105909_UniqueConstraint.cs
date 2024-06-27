using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "DECIMAL(18, 2)",
                nullable: false,
                defaultValue: 6.5m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,2)",
                oldDefaultValue: 6.5m);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Books",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Books",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Code_AuthorName",
                table: "Books",
                columns: new[] { "Code", "AuthorName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Code_AuthorName",
                table: "Books");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 6.5m,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)",
                oldDefaultValue: 6.5m);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Books",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorName",
                table: "Books",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");
        }
    }
}
