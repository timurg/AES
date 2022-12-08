﻿// <auto-generated />
using System;
using AES.Infrastructure.EntityFrameworkCore.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.PostgreSql.Migrations
{
    [DbContext(typeof(AESEntityFrameworkCorePostgreSqlContext))]
    [Migration("20221207180611_LanguafeTableName")]
    partial class LanguafeTableName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AES.Domain.Curator", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Curators", (string)null);
                });

            modelBuilder.Entity("AES.Domain.CuratorDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CuratorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DirectionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SubdivisionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("DirectionId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("SubdivisionId");

                    b.ToTable("CuratorDescription");
                });

            modelBuilder.Entity("AES.Domain.Curriculum", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfAppointment")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("tag")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("AES.Domain.Direction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Direction");
                });

            modelBuilder.Entity("AES.Domain.Duration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Duration");
                });

            modelBuilder.Entity("AES.Domain.FormEducation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("FormEducation");
                });

            modelBuilder.Entity("AES.Domain.GradeRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("GradeDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GradeRecordType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPassed")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("GradeRecords", (string)null);

                    b.HasDiscriminator<int>("GradeRecordType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AES.Domain.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("LanguagesList", (string)null);
                });

            modelBuilder.Entity("AES.Domain.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurriculumId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GradeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRequared")
                        .HasColumnType("boolean");

                    b.Property<int>("ModuleType")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurriculumId");

                    b.HasIndex("GradeId");

                    b.ToTable("Modules", (string)null);

                    b.HasDiscriminator<int>("ModuleType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AES.Domain.ModuleItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GradeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRequared")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("uuid");

                    b.Property<int>("ModuleItemType")
                        .HasColumnType("integer");

                    b.Property<int>("Semester")
                        .HasColumnType("integer");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeTestingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TypeTestingId");

                    b.ToTable("ModuleItems", (string)null);

                    b.HasDiscriminator<int>("ModuleItemType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AES.Domain.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("AES.Domain.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("LastActivityDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("PhotoID")
                        .HasColumnType("uuid");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("WhenSetPassWord")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("AES.Domain.Qualification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Qualification");
                });

            modelBuilder.Entity("AES.Domain.RateEducation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("RateEducation");
                });

            modelBuilder.Entity("AES.Domain.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("AES.Domain.Specialization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Specialization");
                });

            modelBuilder.Entity("AES.Domain.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("ActiveAgreement")
                        .HasColumnType("boolean");

                    b.Property<string>("AgreementComment")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("AgreementDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AgreementNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid?>("BaseRateEducationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DirectionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DurationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EndRateEducationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FormEducationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("MaybeAlternateRule")
                        .HasColumnType("boolean");

                    b.Property<Guid>("QualificationId")
                        .HasColumnType("uuid");

                    b.Property<byte>("Semester")
                        .HasColumnType("smallint");

                    b.Property<Guid>("SpecializationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StudiedLanguageId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubdivisionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("WhenSemesterBegin")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BaseRateEducationId");

                    b.HasIndex("DirectionId");

                    b.HasIndex("DurationId");

                    b.HasIndex("EndRateEducationId");

                    b.HasIndex("FormEducationId");

                    b.HasIndex("QualificationId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("StudiedLanguageId");

                    b.HasIndex("SubdivisionId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("AES.Domain.Subdivision", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Subdivision");
                });

            modelBuilder.Entity("AES.Domain.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("SubjectType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Subjects", (string)null);

                    b.HasDiscriminator<int>("SubjectType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AES.Domain.Tutor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Tutor");
                });

            modelBuilder.Entity("AES.Domain.TutorDescription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DirectionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Semester")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SpecializationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SubdivisionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TutorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeTestingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DirectionId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("SubdivisionId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TutorId");

                    b.HasIndex("TypeTestingId");

                    b.ToTable("TutorDescription");
                });

            modelBuilder.Entity("AES.Domain.TypeTesting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("TypeTesting");
                });

            modelBuilder.Entity("AES.Domain.BalledGradeRecord", b =>
                {
                    b.HasBaseType("AES.Domain.GradeRecord");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("AES.Domain.SubjectCycle", b =>
                {
                    b.HasBaseType("AES.Domain.Module");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("AES.Domain.CurriculumItem", b =>
                {
                    b.HasBaseType("AES.Domain.ModuleItem");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("AES.Domain.BaseForeignLanguageSubject", b =>
                {
                    b.HasBaseType("AES.Domain.Subject");

                    b.HasDiscriminator().HasValue(5);
                });

            modelBuilder.Entity("AES.Domain.LangugetSubject", b =>
                {
                    b.HasBaseType("AES.Domain.Subject");

                    b.Property<Guid>("BaseSubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.HasIndex("BaseSubjectId");

                    b.HasIndex("LanguageId");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("AES.Domain.Practice", b =>
                {
                    b.HasBaseType("AES.Domain.Subject");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("AES.Domain.SimpleSubject", b =>
                {
                    b.HasBaseType("AES.Domain.Subject");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("AES.Domain.Curator", b =>
                {
                    b.HasOne("AES.Domain.Person", "Person")
                        .WithOne("Curator")
                        .HasForeignKey("AES.Domain.Curator", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("AES.Domain.CuratorDescription", b =>
                {
                    b.HasOne("AES.Domain.Curator", null)
                        .WithMany("Descriptions")
                        .HasForeignKey("CuratorId");

                    b.HasOne("AES.Domain.Direction", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId");

                    b.HasOne("AES.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("AES.Domain.Subdivision", "Subdivision")
                        .WithMany()
                        .HasForeignKey("SubdivisionId");

                    b.Navigation("Direction");

                    b.Navigation("Organization");

                    b.Navigation("Subdivision");
                });

            modelBuilder.Entity("AES.Domain.Curriculum", b =>
                {
                    b.HasOne("AES.Domain.Student", "Student")
                        .WithOne("Curriculum")
                        .HasForeignKey("AES.Domain.Curriculum", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("AES.Domain.Module", b =>
                {
                    b.HasOne("AES.Domain.Curriculum", "Curriculum")
                        .WithMany("Modules")
                        .HasForeignKey("CurriculumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.GradeRecord", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.Navigation("Curriculum");

                    b.Navigation("Grade");
                });

            modelBuilder.Entity("AES.Domain.ModuleItem", b =>
                {
                    b.HasOne("AES.Domain.GradeRecord", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("AES.Domain.Module", "Module")
                        .WithMany("Items")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.TypeTesting", "TypeTesting")
                        .WithMany()
                        .HasForeignKey("TypeTestingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Module");

                    b.Navigation("Subject");

                    b.Navigation("TypeTesting");
                });

            modelBuilder.Entity("AES.Domain.Role", b =>
                {
                    b.HasOne("AES.Domain.Person", null)
                        .WithMany("Roles")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("AES.Domain.Student", b =>
                {
                    b.HasOne("AES.Domain.RateEducation", "BaseRateEducation")
                        .WithMany()
                        .HasForeignKey("BaseRateEducationId");

                    b.HasOne("AES.Domain.Direction", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Duration", "Duration")
                        .WithMany()
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.RateEducation", "EndRateEducation")
                        .WithMany()
                        .HasForeignKey("EndRateEducationId");

                    b.HasOne("AES.Domain.FormEducation", "FormEducation")
                        .WithMany()
                        .HasForeignKey("FormEducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Person", "Person")
                        .WithOne("Student")
                        .HasForeignKey("AES.Domain.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Qualification", "Qualification")
                        .WithMany()
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Language", "StudiedLanguage")
                        .WithMany()
                        .HasForeignKey("StudiedLanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Subdivision", "Subdivision")
                        .WithMany()
                        .HasForeignKey("SubdivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseRateEducation");

                    b.Navigation("Direction");

                    b.Navigation("Duration");

                    b.Navigation("EndRateEducation");

                    b.Navigation("FormEducation");

                    b.Navigation("Person");

                    b.Navigation("Qualification");

                    b.Navigation("Specialization");

                    b.Navigation("StudiedLanguage");

                    b.Navigation("Subdivision");
                });

            modelBuilder.Entity("AES.Domain.Subdivision", b =>
                {
                    b.HasOne("AES.Domain.Organization", "Organization")
                        .WithMany("Subdivisions")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("AES.Domain.Tutor", b =>
                {
                    b.HasOne("AES.Domain.Person", "Person")
                        .WithOne("Tutor")
                        .HasForeignKey("AES.Domain.Tutor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("AES.Domain.TutorDescription", b =>
                {
                    b.HasOne("AES.Domain.Direction", "Direction")
                        .WithMany()
                        .HasForeignKey("DirectionId");

                    b.HasOne("AES.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId");

                    b.HasOne("AES.Domain.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId");

                    b.HasOne("AES.Domain.Subdivision", "Subdivision")
                        .WithMany()
                        .HasForeignKey("SubdivisionId");

                    b.HasOne("AES.Domain.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Tutor", null)
                        .WithMany("Descriptions")
                        .HasForeignKey("TutorId");

                    b.HasOne("AES.Domain.TypeTesting", "TypeTesting")
                        .WithMany()
                        .HasForeignKey("TypeTestingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Direction");

                    b.Navigation("Organization");

                    b.Navigation("Specialization");

                    b.Navigation("Subdivision");

                    b.Navigation("Subject");

                    b.Navigation("TypeTesting");
                });

            modelBuilder.Entity("AES.Domain.LangugetSubject", b =>
                {
                    b.HasOne("AES.Domain.BaseForeignLanguageSubject", "BaseSubject")
                        .WithMany("LangugetSubjects")
                        .HasForeignKey("BaseSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AES.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BaseSubject");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("AES.Domain.Curator", b =>
                {
                    b.Navigation("Descriptions");
                });

            modelBuilder.Entity("AES.Domain.Curriculum", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("AES.Domain.Module", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("AES.Domain.Organization", b =>
                {
                    b.Navigation("Subdivisions");
                });

            modelBuilder.Entity("AES.Domain.Person", b =>
                {
                    b.Navigation("Curator");

                    b.Navigation("Roles");

                    b.Navigation("Student");

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("AES.Domain.Student", b =>
                {
                    b.Navigation("Curriculum");
                });

            modelBuilder.Entity("AES.Domain.Tutor", b =>
                {
                    b.Navigation("Descriptions");
                });

            modelBuilder.Entity("AES.Domain.BaseForeignLanguageSubject", b =>
                {
                    b.Navigation("LangugetSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
