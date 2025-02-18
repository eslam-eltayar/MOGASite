using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGASite.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DescriptionAR = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionEN = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BioAR = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BioEN = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleAR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TitleEN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DescriptionAR = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DescriptionEN = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BioAR = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    BioEN = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceSteps_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSteps_ServiceId",
                table: "ServiceSteps",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceSteps");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
