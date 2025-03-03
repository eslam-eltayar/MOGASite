using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGASite.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlToHosting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Hostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Hostings");
        }
    }
}
