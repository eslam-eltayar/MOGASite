using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGASite.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddIsBestToHosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBest",
                table: "Hostings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBest",
                table: "Hostings");
        }
    }
}
