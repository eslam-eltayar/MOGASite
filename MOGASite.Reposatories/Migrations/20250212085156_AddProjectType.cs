using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOGASite.Reposatories.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSteps_Projects_ProjectId",
                table: "ProjectSteps");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectSteps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeadImageUrl",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSteps_Projects_ProjectId",
                table: "ProjectSteps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectSteps_Projects_ProjectId",
                table: "ProjectSteps");

            migrationBuilder.DropColumn(
                name: "HeadImageUrl",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectSteps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectSteps_Projects_ProjectId",
                table: "ProjectSteps",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
