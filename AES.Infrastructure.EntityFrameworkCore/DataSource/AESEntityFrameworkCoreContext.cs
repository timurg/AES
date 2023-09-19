using AES.Domain;
using AES.Domain.Course;
using AES.Story;
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
            
            modelBuilder.Entity<BinaryData>().ToTable("BinaryData");

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

            modelBuilder.Entity<Person>().HasMany(p => p.Roles).WithMany();
            
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

            modelBuilder.Entity<ModuleItem>().ToTable("ModuleItems").HasDiscriminator<int>("LearningProcessesType")
                .HasValue<CurriculumItem>(1);
            modelBuilder.Entity<LearningProcess>().ToTable("LearningProcesses")
                .HasDiscriminator<int>("LearningProcessesType")
            .HasValue<MyStory>(1);

            modelBuilder.Entity<StoryItem>().ToTable("StoryItems")
                .HasDiscriminator<int>("StoryItemType")
                .HasValue<StoryImage>(0)
                .HasValue<StoryPoll>(1)
                .HasValue<StoryVideo>(2)
                .HasValue<StoryVenue>(3)
                .HasValue<StoryHtml>(4);
            
            modelBuilder.Entity<Curator>().ToTable("Curators");
            
            modelBuilder.Entity<MyStoryTemplate>(p => p.ToTable("MyStoryTemplates"));
            
            modelBuilder.Entity<MyStoryTemplateItem>().ToTable("MyStoryTemplateItems")
                .HasDiscriminator<int>("MyStoryTemplateItemType")
                .HasValue<MyStoryTemplateImage>(0)
                .HasValue<MyStoryTemplateQuiz>(1)
                .HasValue<MyStoryTemplateVideo>(2)
                .HasValue<MyStoryTemplateVenue>(3)
                .HasValue<MyStoryTemplateHtml>(4);
        }

    }
}
