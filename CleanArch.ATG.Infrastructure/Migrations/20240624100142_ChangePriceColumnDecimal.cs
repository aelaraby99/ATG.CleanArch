using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePriceColumnDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "DECIMAL(18, 2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Books",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18, 2)");
        }
    }
}
