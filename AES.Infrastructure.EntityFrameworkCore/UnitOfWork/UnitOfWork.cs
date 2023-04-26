using System;
using AES.Domain;
using AES.Story;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AES.Infrastructure.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        public AESEntityFrameworkCoreContext Context { get; private set; }
        private IDbContextTransaction transaction;
        private bool savedAutoDetectChangesEnabled;

        public UnitOfWork(AESEntityFrameworkCoreContext context)
        {
            Context = context;
            transaction = context.Database.BeginTransaction();
            savedAutoDetectChangesEnabled = Context.ChangeTracker.AutoDetectChangesEnabled;
            Context.ChangeTracker.AutoDetectChangesEnabled = false;

            PersonRepository = new PersonRepository(Context);
            DirectionRepository = new DirectionRepository(Context);
            DurationRepository = new DurationRepository(Context);
            FormEducationRepository = new FormEducationRepository(Context);
            LanguageRepository = new LanguageRepository(Context);
            OrganizationRepository = new OrganizationRepository(Context);
            QualificationRepository = new QualificationRepository(Context);
            RateEducationRepository = new RateEducationRepository(Context);
            RoleRepository = new RoleRepository(Context);
            SpecializationRepository = new SpecializationRepository(Context);
            StudentRepository = new StudentRepository(Context);
            SubjectRepository = new SubjectRepository(Context);
            TerritoryRepository = new TerritoryRepository(Context);
            TypeTestingRepository = new TypeTestingRepository(context);
            StoryTemplateRepository = new StoryTemplateRepository(context);
            BinaryDataRepository = new BinaryDataRepository(context);
        }

        public T getRepository<T>() where T : DomainObject
        {
            throw new NotImplementedException();
        }

        public IPersonRepository PersonRepository { get; private set; }

        public IDirectionRepository DirectionRepository { get; private set; }

        public IDurationRepository DurationRepository { get; private set; }

        public IFormEducationRepository FormEducationRepository { get; private set; }

        public ILanguageRepository LanguageRepository { get; private set; }

        public IOrganizationRepository OrganizationRepository { get; private set; }

        public IQualificationRepository QualificationRepository { get; private set; }

        public IRateEducationRepository RateEducationRepository { get; private set; }

        public IRoleRepository RoleRepository { get; private set; }

        public ISpecializationRepository SpecializationRepository { get; private set; }

        public IStudentRepository StudentRepository { get; private set; }

        public ISubjectRepository SubjectRepository { get; private set; }

        public ITerritoryRepository TerritoryRepository { get; private set; }

        public ITypeTestingRepository TypeTestingRepository { get; private set; }
        
        public IStoryTemplateRepository StoryTemplateRepository { get; private set; }
        public IBinaryDataRepository BinaryDataRepository { get; }

        public void Commit()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction.Dispose();
                transaction = null;
            }
            Context.ChangeTracker.DetectChanges();
            
            
            var saved = false;
            while (!saved)
            {
                try
                {
                    Context.SaveChanges();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is StoryPollItem)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                //var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                 proposedValues[property] = proposedValue;
                                //proposedValue = proposedValues[property];
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
            
            //Context.SaveChanges();
            Context.ChangeTracker.AutoDetectChangesEnabled = savedAutoDetectChangesEnabled;
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;
                throw new UnitOfWorkNotCommitedException();
            }
        }
    }
}
