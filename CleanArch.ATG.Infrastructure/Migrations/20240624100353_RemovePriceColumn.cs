using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "DECIMAL(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
