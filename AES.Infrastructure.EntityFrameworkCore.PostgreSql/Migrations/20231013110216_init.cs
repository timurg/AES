using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AES.Infrastructure.EntityFrameworkCore.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinaryData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Bits = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormEducation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormEducation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguagesList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagesList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LastActivityDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    WhenSetPassWord = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    Birthday = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    PhotoID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RateEducation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateEducation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeTesting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTesting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectType = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: true),
                    BaseSubjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    Abbreviation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_LanguagesList_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "LanguagesList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjects_Subjects_BaseSubjectId",
                        column: x => x.BaseSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subdivision",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subdivision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subdivision_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curators_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tutor_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonRole",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRole", x => new { x.PersonId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PersonRole_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyStoryTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeTestingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyStoryTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyStoryTemplates_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyStoryTemplates_TypeTesting_TypeTestingId",
                        column: x => x.TypeTestingId,
                        principalTable: "TypeTesting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AgreementNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AgreementDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ActiveAgreement = table.Column<bool>(type: "boolean", nullable: false),
                    Semester = table.Column<byte>(type: "smallint", nullable: false),
                    WhenSemesterBegin = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MaybeAlternateRule = table.Column<bool>(type: "boolean", nullable: false),
                    AgreementComment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SubdivisionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QualificationId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormEducationId = table.Column<Guid>(type: "uuid", nullable: false),
                    DurationId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudiedLanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseRateEducationId = table.Column<Guid>(type: "uuid", nullable: true),
                    EndRateEducationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Direction_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Direction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Duration_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Duration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_FormEducation_FormEducationId",
                        column: x => x.FormEducationId,
                        principalTable: "FormEducation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_LanguagesList_StudiedLanguageId",
                        column: x => x.StudiedLanguageId,
                        principalTable: "LanguagesList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Qualification_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_RateEducation_BaseRateEducationId",
                        column: x => x.BaseRateEducationId,
                        principalTable: "RateEducation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_RateEducation_EndRateEducationId",
                        column: x => x.EndRateEducationId,
                        principalTable: "RateEducation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Student_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Subdivision_SubdivisionId",
                        column: x => x.SubdivisionId,
                        principalTable: "Subdivision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuratorDescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubdivisionId = table.Column<Guid>(type: "uuid", nullable: true),
                    DirectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    CuratorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuratorDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuratorDescription_Curators_CuratorId",
                        column: x => x.CuratorId,
                        principalTable: "Curators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CuratorDescription_Direction_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Direction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CuratorDescription_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CuratorDescription_Subdivision_SubdivisionId",
                        column: x => x.SubdivisionId,
                        principalTable: "Subdivision",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TutorDescription",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubdivisionId = table.Column<Guid>(type: "uuid", nullable: true),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectionId = table.Column<Guid>(type: "uuid", nullable: true),
                    SpecializationId = table.Column<Guid>(type: "uuid", nullable: true),
                    TypeTestingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: true),
                    TutorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorDescription_Direction_DirectionId",
                        column: x => x.DirectionId,
                        principalTable: "Direction",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TutorDescription_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TutorDescription_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TutorDescription_Subdivision_SubdivisionId",
                        column: x => x.SubdivisionId,
                        principalTable: "Subdivision",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TutorDescription_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorDescription_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TutorDescription_TypeTesting_TypeTestingId",
                        column: x => x.TypeTestingId,
                        principalTable: "TypeTesting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LearningProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LearningProcessesType = table.Column<int>(type: "integer", nullable: false),
                    StoryStep = table.Column<int>(type: "integer", nullable: true),
                    StoryGeneration = table.Column<long>(type: "bigint", nullable: true),
                    StoryTemplateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningProcesses_MyStoryTemplates_StoryTemplateId",
                        column: x => x.StoryTemplateId,
                        principalTable: "MyStoryTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyStoryTemplateItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemIndex = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MyStoryTemplateId = table.Column<Guid>(type: "uuid", nullable: true),
                    MyStoryTemplateItemType = table.Column<int>(type: "integer", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    TelegramFileId = table.Column<string>(type: "text", nullable: true),
                    Longitude = table.Column<float>(type: "real", nullable: true),
                    Latitude = table.Column<float>(type: "real", nullable: true),
                    Adress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyStoryTemplateItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyStoryTemplateItems_MyStoryTemplates_MyStoryTemplateId",
                        column: x => x.MyStoryTemplateId,
                        principalTable: "MyStoryTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfAppointment = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    tag = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curriculum_Student_Id",
                        column: x => x.Id,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyStoryTemplateQuizItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: true),
                    MyStoryTemplateQuizId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyStoryTemplateQuizItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyStoryTemplateQuizItem_MyStoryTemplateItems_MyStoryTemplat~",
                        column: x => x.MyStoryTemplateQuizId,
                        principalTable: "MyStoryTemplateItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ItemIndex = table.Column<int>(type: "integer", nullable: false),
                    TelegramId = table.Column<int>(type: "integer", nullable: true),
                    ChatId = table.Column<long>(type: "bigint", nullable: true),
                    ObjectId = table.Column<string>(type: "text", nullable: true),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPassed = table.Column<bool>(type: "boolean", nullable: true),
                    Generation = table.Column<long>(type: "bigint", nullable: false),
                    MyStoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    StoryItemType = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    SelectedItem = table.Column<long>(type: "bigint", nullable: true),
                    Longitude = table.Column<float>(type: "real", nullable: true),
                    Latitude = table.Column<float>(type: "real", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Adress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryItems_LearningProcesses_MyStoryId",
                        column: x => x.MyStoryId,
                        principalTable: "LearningProcesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoryItems_MyStoryTemplateItems_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "MyStoryTemplateItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryPollItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: true),
                    StoryPollId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryPollItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryPollItem_StoryItems_StoryPollId",
                        column: x => x.StoryPollId,
                        principalTable: "StoryItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GradeRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GradeDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsPassed = table.Column<bool>(type: "boolean", nullable: false),
                    GradeRecordType = table.Column<int>(type: "integer", nullable: false),
                    ModuleItemId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IsRequared = table.Column<bool>(type: "boolean", nullable: false),
                    GradeId = table.Column<Guid>(type: "uuid", nullable: true),
                    CurriculumId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Curriculum_CurriculumId",
                        column: x => x.CurriculumId,
                        principalTable: "Curriculum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modules_GradeRecords_GradeId",
                        column: x => x.GradeId,
                        principalTable: "GradeRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ModuleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    GradeId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeTestingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    LearningProcessId = table.Column<Guid>(type: "uuid", nullable: true),
                    LearningProcessesType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleItems_GradeRecords_GradeId",
                        column: x => x.GradeId,
                        principalTable: "GradeRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuleItems_LearningProcesses_LearningProcessId",
                        column: x => x.LearningProcessId,
                        principalTable: "LearningProcesses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModuleItems_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleItems_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleItems_TypeTesting_TypeTestingId",
                        column: x => x.TypeTestingId,
                        principalTable: "TypeTesting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuratorDescription_CuratorId",
                table: "CuratorDescription",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CuratorDescription_DirectionId",
                table: "CuratorDescription",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CuratorDescription_OrganizationId",
                table: "CuratorDescription",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CuratorDescription_SubdivisionId",
                table: "CuratorDescription",
                column: "SubdivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeRecords_ModuleItemId",
                table: "GradeRecords",
                column: "ModuleItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesList_Name",
                table: "LanguagesList",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LearningProcesses_StoryTemplateId",
                table: "LearningProcesses",
                column: "StoryTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleItems_GradeId",
                table: "ModuleItems",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleItems_LearningProcessId",
                table: "ModuleItems",
                column: "LearningProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleItems_ModuleId",
                table: "ModuleItems",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleItems_SubjectId",
                table: "ModuleItems",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleItems_TypeTestingId",
                table: "ModuleItems",
                column: "TypeTestingId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_CurriculumId",
                table: "Modules",
                column: "CurriculumId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_GradeId",
                table: "Modules",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_MyStoryTemplateItems_MyStoryTemplateId",
                table: "MyStoryTemplateItems",
                column: "MyStoryTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_MyStoryTemplateQuizItem_MyStoryTemplateQuizId",
                table: "MyStoryTemplateQuizItem",
                column: "MyStoryTemplateQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_MyStoryTemplates_SubjectId",
                table: "MyStoryTemplates",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MyStoryTemplates_TypeTestingId",
                table: "MyStoryTemplates",
                column: "TypeTestingId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRole_RolesId",
                table: "PersonRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Email",
                table: "Persons",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Login",
                table: "Persons",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoryItems_MyStoryId",
                table: "StoryItems",
                column: "MyStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryItems_TemplateId",
                table: "StoryItems",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryPollItem_StoryPollId",
                table: "StoryPollItem",
                column: "StoryPollId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_BaseRateEducationId",
                table: "Student",
                column: "BaseRateEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DirectionId",
                table: "Student",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DurationId",
                table: "Student",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_EndRateEducationId",
                table: "Student",
                column: "EndRateEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FormEducationId",
                table: "Student",
                column: "FormEducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_QualificationId",
                table: "Student",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SpecializationId",
                table: "Student",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudiedLanguageId",
                table: "Student",
                column: "StudiedLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SubdivisionId",
                table: "Student",
                column: "SubdivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subdivision_OrganizationId",
                table: "Subdivision",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_BaseSubjectId",
                table: "Subjects",
                column: "BaseSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LanguageId",
                table: "Subjects",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Name",
                table: "Subjects",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_DirectionId",
                table: "TutorDescription",
                column: "DirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_OrganizationId",
                table: "TutorDescription",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_SpecializationId",
                table: "TutorDescription",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_SubdivisionId",
                table: "TutorDescription",
                column: "SubdivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_SubjectId",
                table: "TutorDescription",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_TutorId",
                table: "TutorDescription",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorDescription_TypeTestingId",
                table: "TutorDescription",
                column: "TypeTestingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeRecords_ModuleItems_ModuleItemId",
                table: "GradeRecords",
                column: "ModuleItemId",
                principalTable: "ModuleItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Direction_DirectionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Subdivision_Organizations_OrganizationId",
                table: "Subdivision");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Subdivision_SubdivisionId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Persons_Id",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Curriculum_Student_Id",
                table: "Curriculum");

            migrationBuilder.DropForeignKey(
                name: "FK_GradeRecords_ModuleItems_ModuleItemId",
                table: "GradeRecords");

            migrationBuilder.DropTable(
                name: "BinaryData");

            migrationBuilder.DropTable(
                name: "CuratorDescription");

            migrationBuilder.DropTable(
                name: "MyStoryTemplateQuizItem");

            migrationBuilder.DropTable(
                name: "PersonRole");

            migrationBuilder.DropTable(
                name: "StoryPollItem");

            migrationBuilder.DropTable(
                name: "TutorDescription");

            migrationBuilder.DropTable(
                name: "Curators");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "StoryItems");

            migrationBuilder.DropTable(
                name: "Tutor");

            migrationBuilder.DropTable(
                name: "MyStoryTemplateItems");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Subdivision");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Duration");

            migrationBuilder.DropTable(
                name: "FormEducation");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "RateEducation");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "ModuleItems");

            migrationBuilder.DropTable(
                name: "LearningProcesses");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "MyStoryTemplates");

            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "GradeRecords");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "TypeTesting");

            migrationBuilder.DropTable(
                name: "LanguagesList");
        }
    }
}
