using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SkillLevelGradedSkillSetConstraintFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradedSkillSets_SkillLevels_GradeTemplateId",
                table: "GradedSkillSets");

            migrationBuilder.CreateIndex(
                name: "IX_GradedSkillSets_SkillLevelId",
                table: "GradedSkillSets",
                column: "SkillLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradedSkillSets_SkillLevels_SkillLevelId",
                table: "GradedSkillSets",
                column: "SkillLevelId",
                principalTable: "SkillLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradedSkillSets_SkillLevels_SkillLevelId",
                table: "GradedSkillSets");

            migrationBuilder.DropIndex(
                name: "IX_GradedSkillSets_SkillLevelId",
                table: "GradedSkillSets");

            migrationBuilder.AddForeignKey(
                name: "FK_GradedSkillSets_SkillLevels_GradeTemplateId",
                table: "GradedSkillSets",
                column: "GradeTemplateId",
                principalTable: "SkillLevels",
                principalColumn: "Id");
        }
    }
}
