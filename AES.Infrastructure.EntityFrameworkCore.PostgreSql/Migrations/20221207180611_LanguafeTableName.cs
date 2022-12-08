using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class LanguafeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_Subjects_SubjectId",
                table: "ModuleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_TypeTesting_TypeTestingId",
                table: "ModuleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Direction_DirectionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Duration_DurationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_FormEducation_FormEducationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Language_StudiedLanguageId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Qualification_QualificationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Specialization_SpecializationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Subdivision_SubdivisionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Language_LanguageId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorDescription_Subjects_SubjectId",
                table: "TutorDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorDescription_TypeTesting_TypeTestingId",
                table: "TutorDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "LanguagesList");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeTestingId",
                table: "TutorDescription",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
                table: "TutorDescription",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenSemesterBegin",
                table: "Student",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubdivisionId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StudiedLanguageId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecializationId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QualificationId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FormEducationId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DurationId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectionId",
                table: "Student",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AgreementDate",
                table: "Student",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenSetPassWord",
                table: "Persons",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActivityDateTime",
                table: "Persons",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Persons",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeTestingId",
                table: "ModuleItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
                table: "ModuleItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "GradeDateTime",
                table: "GradeRecords",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAppointment",
                table: "Curriculum",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguagesList",
                table: "LanguagesList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesList_Name",
                table: "LanguagesList",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_Subjects_SubjectId",
                table: "ModuleItems",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_TypeTesting_TypeTestingId",
                table: "ModuleItems",
                column: "TypeTestingId",
                principalTable: "TypeTesting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Direction_DirectionId",
                table: "Student",
                column: "DirectionId",
                principalTable: "Direction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Duration_DurationId",
                table: "Student",
                column: "DurationId",
                principalTable: "Duration",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_FormEducation_FormEducationId",
                table: "Student",
                column: "FormEducationId",
                principalTable: "FormEducation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_LanguagesList_StudiedLanguageId",
                table: "Student",
                column: "StudiedLanguageId",
                principalTable: "LanguagesList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Qualification_QualificationId",
                table: "Student",
                column: "QualificationId",
                principalTable: "Qualification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Specialization_SpecializationId",
                table: "Student",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Subdivision_SubdivisionId",
                table: "Student",
                column: "SubdivisionId",
                principalTable: "Subdivision",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_LanguagesList_LanguageId",
                table: "Subjects",
                column: "LanguageId",
                principalTable: "LanguagesList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorDescription_Subjects_SubjectId",
                table: "TutorDescription",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TutorDescription_TypeTesting_TypeTestingId",
                table: "TutorDescription",
                column: "TypeTestingId",
                principalTable: "TypeTesting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_Subjects_SubjectId",
                table: "ModuleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleItems_TypeTesting_TypeTestingId",
                table: "ModuleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Direction_DirectionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Duration_DurationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_FormEducation_FormEducationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_LanguagesList_StudiedLanguageId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Qualification_QualificationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Specialization_SpecializationId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Subdivision_SubdivisionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_LanguagesList_LanguageId",
                table: "Subjects");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorDescription_Subjects_SubjectId",
                table: "TutorDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorDescription_TypeTesting_TypeTestingId",
                table: "TutorDescription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguagesList",
                table: "LanguagesList");

            migrationBuilder.DropIndex(
                name: "IX_LanguagesList_Name",
                table: "LanguagesList");

            migrationBuilder.RenameTable(
                name: "LanguagesList",
                newName: "Language");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeTestingId",
                table: "TutorDescription",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
                table: "TutorDescription",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenSemesterBegin",
                table: "Student",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubdivisionId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudiedLanguageId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecializationId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "QualificationId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FormEducationId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "DurationId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "DirectionId",
                table: "Student",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AgreementDate",
                table: "Student",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "WhenSetPassWord",
                table: "Persons",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastActivityDateTime",
                table: "Persons",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Persons",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeTestingId",
                table: "ModuleItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectId",
                table: "ModuleItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GradeDateTime",
                table: "GradeRecords",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAppointment",
                table: "Curriculum",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_Subjects_SubjectId",
                table: "ModuleItems",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleItems_TypeTesting_TypeTestingId",
                table: "ModuleItems",
                column: "TypeTestingId",
                principalTable: "TypeTesting",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Direction_DirectionId",
                table: "Student",
                column: "DirectionId",
                principalTable: "Direction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Duration_DurationId",
                table: "Student",
                column: "DurationId",
                principalTable: "Duration",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_FormEducation_FormEducationId",
                table: "Student",
                column: "FormEducationId",
                principalTable: "FormEducation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Language_StudiedLanguageId",
                table: "Student",
                column: "StudiedLanguageId",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Qualification_QualificationId",
                table: "Student",
                column: "QualificationId",
                principalTable: "Qualification",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Specialization_SpecializationId",
                table: "Student",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Subdivision_SubdivisionId",
                table: "Student",
                column: "SubdivisionId",
                principalTable: "Subdivision",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Language_LanguageId",
                table: "Subjects",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorDescription_Subjects_SubjectId",
                table: "TutorDescription",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorDescription_TypeTesting_TypeTestingId",
                table: "TutorDescription",
                column: "TypeTestingId",
                principalTable: "TypeTesting",
                principalColumn: "Id");
        }
    }
}
