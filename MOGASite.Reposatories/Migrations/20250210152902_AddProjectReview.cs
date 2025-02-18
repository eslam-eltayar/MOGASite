using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGASite.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Projects_ProjectId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Reviews");

            migrationBuilder.CreateTable(
                name: "ProjectReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstNameEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstNameAR = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastNameEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastNameAR = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PositionEN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PositionAR = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReviewTextEN = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    ReviewTextAR = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectReviews_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReviews_ProjectId",
                table: "ProjectReviews",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectReviews");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Reviews",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProjectId",
                table: "Reviews",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Projects_ProjectId",
                table: "Reviews",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
