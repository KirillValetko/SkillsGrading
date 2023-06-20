using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GradeLevelGroupLogicChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupValue",
                table: "GradeLevelGroups");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "GradeLevelGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupValue",
                table: "GradeLevelGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "GradeLevelGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
