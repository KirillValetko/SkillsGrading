using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GradeLevelGroupAndSpecialtyBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradedSkillSets_GradeLevels_GradeLevelId",
                table: "GradedSkillSets");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Employees_EmployeeId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeLevels_NewGradeLevelId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecialtyId",
                table: "GradeTemplates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "GradeLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SpecialtyId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialtyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeLevelGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupValue = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLevelGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeLevelGroups_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeTemplates_SpecialtyId",
                table: "GradeTemplates",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeLevels_GroupId",
                table: "GradeLevels",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SpecialtyId",
                table: "Employees",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeLevelGroups_SpecialtyId",
                table: "GradeLevelGroups",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Specialties_SpecialtyId",
                table: "Employees",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradedSkillSets_GradeLevels_GradeLevelId",
                table: "GradedSkillSets",
                column: "GradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeLevels_GradeLevelGroups_GroupId",
                table: "GradeLevels",
                column: "GroupId",
                principalTable: "GradeLevelGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Employees_EmployeeId",
                table: "Grades",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeLevels_NewGradeLevelId",
                table: "Grades",
                column: "NewGradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeTemplates_Specialties_SpecialtyId",
                table: "GradeTemplates",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Specialties_SpecialtyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_GradedSkillSets_GradeLevels_GradeLevelId",
                table: "GradedSkillSets");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeLevels_GradeLevelGroups_GroupId",
                table: "GradeLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Employees_EmployeeId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeLevels_NewGradeLevelId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeTemplates_Specialties_SpecialtyId",
                table: "GradeTemplates");

            migrationBuilder.DropTable(
                name: "GradeLevelGroups");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropIndex(
                name: "IX_GradeTemplates_SpecialtyId",
                table: "GradeTemplates");

            migrationBuilder.DropIndex(
                name: "IX_GradeLevels_GroupId",
                table: "GradeLevels");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SpecialtyId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "GradeTemplates");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GradeLevels");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_GradedSkillSets_GradeLevels_GradeLevelId",
                table: "GradedSkillSets",
                column: "GradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Employees_EmployeeId",
                table: "Grades",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeLevels_NewGradeLevelId",
                table: "Grades",
                column: "NewGradeLevelId",
                principalTable: "GradeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
