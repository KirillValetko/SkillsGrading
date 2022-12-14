using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GraderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_GraderId",
                        column: x => x.GraderId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GradeLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    GradeRevisionInMonths = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelValue = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewGradeLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_GradeLevels_NewGradeLevelId",
                        column: x => x.NewGradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_GradeTemplates_GradeTemplateId",
                        column: x => x.GradeTemplateId,
                        principalTable: "GradeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelValue = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillLevels_SkillGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "SkillGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_SkillGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "SkillGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradedSkillSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeLevelPosition = table.Column<int>(type: "int", nullable: false),
                    SkillPosition = table.Column<int>(type: "int", nullable: false),
                    GradeTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GradeLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradedSkillSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradedSkillSets_GradeLevels_GradeLevelId",
                        column: x => x.GradeLevelId,
                        principalTable: "GradeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradedSkillSets_GradeTemplates_GradeTemplateId",
                        column: x => x.GradeTemplateId,
                        principalTable: "GradeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradedSkillSets_SkillLevels_GradeTemplateId",
                        column: x => x.GradeTemplateId,
                        principalTable: "SkillLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GradedSkillSets_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GraderId",
                table: "Employees",
                column: "GraderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GradedSkillSets_GradeLevelId",
                table: "GradedSkillSets",
                column: "GradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_GradedSkillSets_GradeTemplateId",
                table: "GradedSkillSets",
                column: "GradeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GradedSkillSets_SkillId",
                table: "GradedSkillSets",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EmployeeId",
                table: "Grades",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_GradeTemplateId",
                table: "Grades",
                column: "GradeTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_NewGradeLevelId",
                table: "Grades",
                column: "NewGradeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillLevels_GroupId",
                table: "SkillLevels",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_GroupId",
                table: "Skills",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradedSkillSets");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "SkillLevels");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "GradeLevels");

            migrationBuilder.DropTable(
                name: "GradeTemplates");

            migrationBuilder.DropTable(
                name: "SkillGroups");
        }
    }
}
