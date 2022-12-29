using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BothWayRecursion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_GraderId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GraderId",
                table: "Employees",
                column: "GraderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_GraderId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GraderId",
                table: "Employees",
                column: "GraderId",
                unique: true);
        }
    }
}
