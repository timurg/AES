using System;
using AES.Domain;

namespace AES.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        ///<summary>
        /// Необходимо вызывать для фиксации
        ///</summary>
        void Commit();

        T getRepository<T>()  where T:DomainObject; 
        
        IPersonRepository PersonRepository { get; }
        IDirectionRepository DirectionRepository { get; }
        IDurationRepository DurationRepository { get; }
        IFormEducationRepository FormEducationRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IOrganizationRepository OrganizationRepository { get; }
        IQualificationRepository QualificationRepository { get; }
        IRateEducationRepository RateEducationRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }
        IStudentRepository StudentRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ITerritoryRepository TerritoryRepository { get; }
        ITypeTestingRepository TypeTestingRepository { get; }
    }
}
