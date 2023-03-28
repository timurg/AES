using System.Linq;
using AES.Domain;
using AES.Infrastructure;

namespace AES.BusinessLogic.Implementation;

public class StudentFinderBusinessObject : BusinessObject, IStudentFinder
{
    public Student FindByAgreementNumber(string agreementNumber)
    {
        return (from student in UnitOfWork.StudentRepository.GetQuery()
            where student.AgreementNumber == agreementNumber
            select student).FirstOrDefault();
    }

    public StudentFinderBusinessObject(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}