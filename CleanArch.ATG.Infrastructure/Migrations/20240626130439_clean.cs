using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class clean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Ck_Price",
                table: "Books");

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
                defaultValue: 6.5m);

            migrationBuilder.AddCheckConstraint(
                name: "Ck_Price",
                table: "Books",
                sql: "\"Price\" >= 6.5");
        }
    }
}
