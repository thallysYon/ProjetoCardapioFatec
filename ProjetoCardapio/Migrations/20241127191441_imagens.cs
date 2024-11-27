﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoCardapio.Migrations
{
    /// <inheritdoc />
    public partial class imagens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Prato",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Prato");
        }
    }
}
