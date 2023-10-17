using AES.Domain;
using AES.Infrastructure;
using AES.Infrastructure.EntityFrameworkCore;
using AES.Infrastructure.EntityFrameworkCore.PostgreSql;
using AES.Infrastructure.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>()
    .Build();

var psql = new AESEntityFrameworkCorePostgreSqlContext(configuration.GetConnectionString("psql"));
var sqlt = new AESEntityFrameworkCoreSqliteContext(configuration.GetConnectionString("sqlt"));

/*
var entityTypes = sqlt.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
foreach (var entityType in entityTypes)
{
    Console.WriteLine(entityType.FullName);
}
*/

IUnitOfWork unitOfWorkSqlite = new UnitOfWork(sqlt);
//IUnitOfWork unitOfWorkPsql = new UnitOfWork(psql);


/*
Console.WriteLine("DirectionRepository");
foreach (var entity in unitOfWorkSqlite.DirectionRepository.GetQuery().ToList())
{
    unitOfWorkPsql.DirectionRepository.Save(entity);
}

Console.WriteLine("DurationRepository");
foreach (var entity in unitOfWorkSqlite.DurationRepository.GetQuery().ToList())
{
    unitOfWorkPsql.DurationRepository.Save(entity);
}

Console.WriteLine("LanguageRepository");
foreach (var entity in unitOfWorkSqlite.LanguageRepository.GetQuery().ToList())
{
    unitOfWorkPsql.LanguageRepository.Save(entity);
}

Console.WriteLine("QualificationRepository");
foreach (var entity in unitOfWorkSqlite.QualificationRepository.GetQuery().ToList())
{
    unitOfWorkPsql.QualificationRepository.Save(entity);
}

Console.WriteLine("SpecializationRepository");
foreach (var entity in unitOfWorkSqlite.SpecializationRepository.GetQuery().ToList())
{
    unitOfWorkPsql.SpecializationRepository.Save(entity);
}

Console.WriteLine("SubjectRepository");
foreach (var entity in unitOfWorkSqlite.SubjectRepository.GetQuery().ToList())
{
    unitOfWorkPsql.SubjectRepository.Save(entity);
}

foreach (var entity in unitOfWorkSqlite.TerritoryRepository.GetQuery().AsNoTracking().ToList())
{
    unitOfWorkPsql.TerritoryRepository.Save(entity);
}
*/



Console.WriteLine("BinaryDataRepository");
foreach (var entity in unitOfWorkSqlite.BinaryDataRepository.GetQuery().ToList())
{
    Console.WriteLine("Entity " + entity.Id);
    IUnitOfWork unitOfWorkPsql = new UnitOfWork(psql);
    unitOfWorkPsql.BinaryDataRepository.Save(entity);
    unitOfWorkPsql.Commit();
}

/*
Console.WriteLine("FormEducationRepository");
foreach (var entity in unitOfWorkSqlite.FormEducationRepository.GetQuery().ToList())
{
    unitOfWorkPsql.FormEducationRepository.Save(entity);
}

Console.WriteLine("RateEducationRepository");
foreach (var entity in unitOfWorkSqlite.RateEducationRepository.GetQuery().ToList())
{
    unitOfWorkPsql.RateEducationRepository.Save(entity);
}

Console.WriteLine("StoryTemplateRepository");
foreach (var entity in unitOfWorkSqlite.StoryTemplateRepository.GetQuery().ToList())
{
    unitOfWorkPsql.StoryTemplateRepository.Save(entity);
}

Console.WriteLine("TypeTestingRepository");
foreach (var entity in unitOfWorkSqlite.TypeTestingRepository.GetQuery().ToList())
{
    unitOfWorkPsql.TypeTestingRepository.Save(entity);
}

Console.WriteLine("RoleRepository");
foreach (var entity in unitOfWorkSqlite.RoleRepository.GetQuery().ToList())
{
    unitOfWorkPsql.RoleRepository.Save(entity);
}

Console.WriteLine("OrganizationRepository");
foreach (var entity in unitOfWorkSqlite.OrganizationRepository.GetQuery().ToList())
{
    unitOfWorkPsql.OrganizationRepository.Save(entity);
}

/*

Console.WriteLine("StudentRepository");
foreach (var entity in unitOfWorkSqlite.StudentRepository.GetQuery().ToList())
{
    unitOfWorkPsql.StudentRepository.Save(entity);
}

Console.WriteLine("PersonRepository");
foreach (var entity in unitOfWorkSqlite.PersonRepository.GetQuery().ToList())
{
    unitOfWorkPsql.PersonRepository.Save(entity);
}
*/

/*
Console.WriteLine("StoryTemplateRepository");
foreach (var entity in unitOfWorkSqlite.StoryTemplateRepository.GetQuery().ToList())
{
    unitOfWorkPsql.StoryTemplateRepository.Save(entity);
}
*/

unitOfWorkSqlite.Commit();
//unitOfWorkPsql.Commit();