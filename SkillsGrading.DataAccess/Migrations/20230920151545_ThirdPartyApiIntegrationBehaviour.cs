using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ThirdPartyApiIntegrationBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Specialties_SpecialtyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeLevels_GradeLevelGroups_GroupId",
                table: "GradeLevels");

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
                name: "IX_Employees_AccountName",
                table: "Employees");

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
                name: "Salary",
                table: "GradeLevels");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "GradeLevels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    SpecialtyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    SpecialtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "IX_Employees_AccountName",
                table: "Employees",
                column: "AccountName",
                unique: true);

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
                name: "FK_GradeLevels_GradeLevelGroups_GroupId",
                table: "GradeLevels",
                column: "GroupId",
                principalTable: "GradeLevelGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeTemplates_Specialties_SpecialtyId",
                table: "GradeTemplates",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id");
        }
    }
}
