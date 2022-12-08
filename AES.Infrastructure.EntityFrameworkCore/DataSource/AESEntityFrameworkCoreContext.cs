using AES.Domain;
using Microsoft.EntityFrameworkCore;
namespace AES.Infrastructure.EntityFrameworkCore
{
    public class AESEntityFrameworkCoreContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>( ob =>
                ob.ToTable("LanguagesList"));
            modelBuilder.Entity<Language>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Subject>(
                ob =>
            {
                ob.ToTable("Subjects");
                ob.HasDiscriminator<int>("SubjectType")
                .HasValue<SimpleSubject>(0)
                .HasValue<Practice>(1)
                .HasValue<LangugetSubject>(4)
                .HasValue<BaseForeignLanguageSubject>(5);
            });

            modelBuilder.Entity<Subject>().HasIndex(p => p.Name).IsUnique();

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Student)
                .WithOne(i => i.Person)
                .HasForeignKey<Student>(b => b.Id);
            modelBuilder.Entity<Person>().HasIndex(p => p.Login).IsUnique();
            modelBuilder.Entity<Person>().HasIndex(p => p.Email).IsUnique();
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Curator)
                .WithOne(i => i.Person)
                .HasForeignKey<Curator>(b => b.Id);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Tutor)
                .WithOne(i => i.Person)
                .HasForeignKey<Tutor>(b => b.Id);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Curriculum)
                .WithOne(c => c.Student)
                .HasForeignKey<Curriculum>(c => c.Id);

            modelBuilder.Entity<GradeRecord>().ToTable("GradeRecords")
                .HasDiscriminator<int>("GradeRecordType")
            .HasValue<BalledGradeRecord>(0);

            modelBuilder.Entity<Module>().ToTable("Modules")
                .HasDiscriminator<int>("ModuleType")
            .HasValue<SubjectCycle>(0);

            modelBuilder.Entity<Curriculum>().HasMany(c => c.Modules).WithOne(m => m.Curriculum);
            modelBuilder.Entity<Module>().HasMany(m => m.Items).WithOne(mi => mi.Module);

            modelBuilder.Entity<ModuleItem>().ToTable("ModuleItems")
                .HasDiscriminator<int>("ModuleItemType")
            .HasValue<CurriculumItem>(0);

            modelBuilder.Entity<Curator>().ToTable("Curators");
        }

    }
}
