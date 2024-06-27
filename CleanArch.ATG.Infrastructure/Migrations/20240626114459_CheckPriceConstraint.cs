using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckPriceConstraint : Migration
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

            migrationBuilder.AddCheckConstraint(
                name: "Ck_Price",
                table: "Books",
                sql: "\"Price\" >= 6.5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "Ck_Price",
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
        }
    }
}
