﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SkillsGrading.DataAccess.Infrastructure;

#nullable disable

namespace SkillsGrading.DataAccess.Migrations
{
    [DbContext(typeof(GradingContext))]
    [Migration("20230415163233_RemovedEmployeeDepartment")]
    partial class RemovedEmployeeDepartment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GraderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("SpecialtyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccountName")
                        .IsUnique();

                    b.HasIndex("GraderId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Grade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("GradeDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("GradeTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("NewGradeLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GradeTemplateId");

                    b.HasIndex("NewGradeLevelId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GradeRevisionInMonths")
                        .HasColumnType("int");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelValue")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("GradeLevels");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeLevelGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupValue")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<Guid>("SpecialtyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("GradeLevelGroups");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<Guid>("SpecialtyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TemplateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("GradeTemplates");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradedSkillSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GradeLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GradeLevelPosition")
                        .HasColumnType("int");

                    b.Property<Guid>("GradeTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SkillLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SkillPosition")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GradeLevelId");

                    b.HasIndex("GradeTemplateId");

                    b.HasIndex("SkillId");

                    b.HasIndex("SkillLevelId");

                    b.ToTable("GradedSkillSets");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.SkillGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("SkillGroups");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.SkillLevel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LevelValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("SkillLevels");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Specialty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<string>("SpecialtyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Employee", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.Employee", "Grader")
                        .WithMany("Gradees")
                        .HasForeignKey("GraderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SkillsGrading.DataAccess.Models.Specialty", "Specialty")
                        .WithMany("Employees")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grader");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Grade", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.Employee", "Employee")
                        .WithMany("Grades")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SkillsGrading.DataAccess.Models.GradeTemplate", "GradeTemplate")
                        .WithMany("Grades")
                        .HasForeignKey("GradeTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillsGrading.DataAccess.Models.GradeLevel", "GradeLevel")
                        .WithMany("Grades")
                        .HasForeignKey("NewGradeLevelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("GradeLevel");

                    b.Navigation("GradeTemplate");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeLevel", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.GradeLevelGroup", "GradeLevelGroup")
                        .WithMany("GradeLevels")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GradeLevelGroup");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeLevelGroup", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.Specialty", "Specialty")
                        .WithMany("GradeLevelGroups")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeTemplate", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.Specialty", "Specialty")
                        .WithMany("GradeTemplates")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradedSkillSet", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.GradeLevel", "GradeLevel")
                        .WithMany("GradedSkillSets")
                        .HasForeignKey("GradeLevelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SkillsGrading.DataAccess.Models.GradeTemplate", "GradeTemplate")
                        .WithMany("GradedSkillSets")
                        .HasForeignKey("GradeTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkillsGrading.DataAccess.Models.Skill", "Skill")
                        .WithMany("GradedSkillSets")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SkillsGrading.DataAccess.Models.SkillLevel", "SkillLevel")
                        .WithMany("GradedSkillSets")
                        .HasForeignKey("SkillLevelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GradeLevel");

                    b.Navigation("GradeTemplate");

                    b.Navigation("Skill");

                    b.Navigation("SkillLevel");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Skill", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.SkillGroup", "SkillGroup")
                        .WithMany("Skills")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkillGroup");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.SkillLevel", b =>
                {
                    b.HasOne("SkillsGrading.DataAccess.Models.SkillGroup", "SkillGroup")
                        .WithMany("SkillLevels")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SkillGroup");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Employee", b =>
                {
                    b.Navigation("Gradees");

                    b.Navigation("Grades");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeLevel", b =>
                {
                    b.Navigation("GradedSkillSets");

                    b.Navigation("Grades");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeLevelGroup", b =>
                {
                    b.Navigation("GradeLevels");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.GradeTemplate", b =>
                {
                    b.Navigation("GradedSkillSets");

                    b.Navigation("Grades");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Skill", b =>
                {
                    b.Navigation("GradedSkillSets");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.SkillGroup", b =>
                {
                    b.Navigation("SkillLevels");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.SkillLevel", b =>
                {
                    b.Navigation("GradedSkillSets");
                });

            modelBuilder.Entity("SkillsGrading.DataAccess.Models.Specialty", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("GradeLevelGroups");

                    b.Navigation("GradeTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}
