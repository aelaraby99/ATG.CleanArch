﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArch.ATG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BooksAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEMP_BOOKS",
                columns: table => new
                {
                    Title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    AuthorName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEMP_BOOKS");
        }
    }
}
