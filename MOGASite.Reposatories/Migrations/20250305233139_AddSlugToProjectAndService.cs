using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGASite.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugToProjectAndService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Projects");
        }
    }
}
